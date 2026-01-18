namespace KeryxPars.HL7.Serialization;

using System.Buffers;

/// <summary>
/// High-performance segment writer using ArrayPool.
/// </summary>
public ref struct SegmentWriter
{
    private char[] _buffer;
    private int _position;
    private readonly bool _rented;

    public SegmentWriter(int initialCapacity = 1024)
    {
        _buffer = ArrayPool<char>.Shared.Rent(initialCapacity);
        _position = 0;
        _rented = true;
    }

    public ReadOnlySpan<char> WrittenSpan => _buffer.AsSpan(0, _position);

    public void Write(ReadOnlySpan<char> value)
    {
        EnsureCapacity(_position + value.Length);
        value.CopyTo(_buffer.AsSpan(_position));
        _position += value.Length;
    }

    public void Write(char value)
    {
        EnsureCapacity(_position + 1);
        _buffer[_position++] = value;
    }

    public void WriteField(ReadOnlySpan<char> value, char delimiter)
    {
        Write(value);
        Write(delimiter);
    }

    private void EnsureCapacity(int required)
    {
        if (required <= _buffer.Length)
            return;

        int newSize = Math.Max(_buffer.Length * 2, required);
        char[] newBuffer = ArrayPool<char>.Shared.Rent(newSize);
        _buffer.AsSpan(0, _position).CopyTo(newBuffer);

        if (_rented)
            ArrayPool<char>.Shared.Return(_buffer);

        _buffer = newBuffer;
    }

    public void Dispose()
    {
        if (_rented && _buffer != null)
        {
            ArrayPool<char>.Shared.Return(_buffer);
        }
    }

    public override string ToString() => new string(WrittenSpan);
}