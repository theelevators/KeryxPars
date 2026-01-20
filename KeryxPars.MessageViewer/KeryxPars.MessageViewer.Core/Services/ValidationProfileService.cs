using System.Text.Json;
using KeryxPars.MessageViewer.Core.Models;

namespace KeryxPars.MessageViewer.Core.Services;

/// <summary>
/// Service for managing validation profiles.
/// </summary>
public class ValidationProfileService
{
    private readonly string _profilesDirectory;
    private List<ValidationProfile> _profiles = [];

    public ValidationProfileService()
    {
        _profilesDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "KeryxPars",
            "ValidationProfiles"
        );
        
        Directory.CreateDirectory(_profilesDirectory);
        LoadProfiles();
    }

    public ValidationProfile? ActiveProfile { get; private set; }

    /// <summary>
    /// Gets all available profiles (built-in + user-created).
    /// </summary>
    public List<ValidationProfile> GetProfiles() => _profiles;

    /// <summary>
    /// Gets only user-created profiles.
    /// </summary>
    public List<ValidationProfile> GetUserProfiles() => 
        _profiles.Where(p => !p.IsBuiltIn).ToList();

    /// <summary>
    /// Creates a new profile.
    /// </summary>
    public async Task<ValidationProfile> CreateProfileAsync(string name, string? description = null)
    {
        var profile = new ValidationProfile
        {
            Name = name,
            Description = description
        };

        _profiles.Add(profile);
        await SaveProfileAsync(profile);
        return profile;
    }

    /// <summary>
    /// Updates an existing profile.
    /// </summary>
    public async Task UpdateProfileAsync(ValidationProfile profile)
    {
        profile.UpdatedAt = DateTime.UtcNow;
        
        var existing = _profiles.FirstOrDefault(p => p.Id == profile.Id);
        if (existing != null)
        {
            var index = _profiles.IndexOf(existing);
            _profiles[index] = profile;
        }

        await SaveProfileAsync(profile);
    }

    /// <summary>
    /// Deletes a profile.
    /// </summary>
    public async Task DeleteProfileAsync(Guid profileId)
    {
        var profile = _profiles.FirstOrDefault(p => p.Id == profileId);
        if (profile != null && !profile.IsBuiltIn)
        {
            _profiles.Remove(profile);
            var filePath = GetProfileFilePath(profileId);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        
        await Task.CompletedTask;
    }

    /// <summary>
    /// Sets the active profile.
    /// </summary>
    public void SetActiveProfile(ValidationProfile profile)
    {
        ActiveProfile = profile;
    }

    /// <summary>
    /// Adds a rule to the active profile.
    /// </summary>
    public async Task AddRuleAsync(RuleDefinition rule)
    {
        if (ActiveProfile == null)
        {
            ActiveProfile = await CreateProfileAsync("New Profile");
        }

        ActiveProfile.Rules.Add(rule);
        await UpdateProfileAsync(ActiveProfile);
    }

    /// <summary>
    /// Updates a rule in the active profile.
    /// </summary>
    public async Task UpdateRuleAsync(RuleDefinition rule)
    {
        if (ActiveProfile == null)
            return;

        var existing = ActiveProfile.Rules.FirstOrDefault(r => r.Id == rule.Id);
        if (existing != null)
        {
            var index = ActiveProfile.Rules.IndexOf(existing);
            ActiveProfile.Rules[index] = rule;
            await UpdateProfileAsync(ActiveProfile);
        }
    }

    /// <summary>
    /// Removes a rule from the active profile.
    /// </summary>
    public async Task RemoveRuleAsync(Guid ruleId)
    {
        if (ActiveProfile == null)
            return;

        var rule = ActiveProfile.Rules.FirstOrDefault(r => r.Id == ruleId);
        if (rule != null)
        {
            ActiveProfile.Rules.Remove(rule);
            await UpdateProfileAsync(ActiveProfile);
        }
    }

    /// <summary>
    /// Toggles a rule's enabled state.
    /// </summary>
    public async Task ToggleRuleAsync(RuleDefinition rule)
    {
        rule.IsEnabled = !rule.IsEnabled;
        await UpdateRuleAsync(rule);
    }

    /// <summary>
    /// Exports a profile to JSON.
    /// </summary>
    public string ExportProfile(ValidationProfile profile)
    {
        return JsonSerializer.Serialize(profile, new JsonSerializerOptions 
        { 
            WriteIndented = true 
        });
    }

    /// <summary>
    /// Imports a profile from JSON.
    /// </summary>
    public async Task<ValidationProfile> ImportProfileAsync(string json)
    {
        var profile = JsonSerializer.Deserialize<ValidationProfile>(json);
        if (profile == null)
            throw new InvalidOperationException("Invalid profile JSON");

        profile.Id = Guid.NewGuid(); // Generate new ID for imported profile
        profile.IsBuiltIn = false;
        
        _profiles.Add(profile);
        await SaveProfileAsync(profile);
        
        return profile;
    }

    private void LoadProfiles()
    {
        _profiles.Clear();

        // Load built-in profiles
        _profiles.AddRange(RuleTemplateService.GetBuiltInProfiles());

        // Load user profiles
        if (Directory.Exists(_profilesDirectory))
        {
            foreach (var file in Directory.GetFiles(_profilesDirectory, "*.json"))
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var profile = JsonSerializer.Deserialize<ValidationProfile>(json);
                    if (profile != null)
                    {
                        _profiles.Add(profile);
                    }
                }
                catch
                {
                    // Skip invalid profiles
                }
            }
        }
    }

    private async Task SaveProfileAsync(ValidationProfile profile)
    {
        if (profile.IsBuiltIn)
            return; // Don't save built-in profiles

        var filePath = GetProfileFilePath(profile.Id);
        var json = JsonSerializer.Serialize(profile, new JsonSerializerOptions 
        { 
            WriteIndented = true 
        });
        await File.WriteAllTextAsync(filePath, json);
    }

    private string GetProfileFilePath(Guid profileId)
    {
        return Path.Combine(_profilesDirectory, $"{profileId}.json");
    }
}
