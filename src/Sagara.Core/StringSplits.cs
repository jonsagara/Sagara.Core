namespace Sagara.Core;

/// <summary>
/// Static array references to that we don't have to allocate an array every time we call string.Split.
/// </summary>
public static class StringSplits
{
    public static readonly char[]
        Colon = { ':' },
        Comma = { ',' },
        Email = { '@', '.' },
        ForwardSlash = { '/' },
        Newline = { '\n' },
        NewlineCarriageReturn = { '\n', '\r' },
        Semicolon = { ';' };
}
