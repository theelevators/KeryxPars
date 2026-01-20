// Polyfill for init-only setters in netstandard2.0
// This allows us to use C# 9+ init properties in older frameworks

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}
