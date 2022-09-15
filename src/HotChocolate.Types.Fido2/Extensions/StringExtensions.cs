namespace HotChocolate.Types.Fido2.Extensions;

internal static class StringExtensions
{
    public static string Dedent(this string @this)
    {
        var lines = @this
            .Split(Environment.NewLine)
            .Select(x => x.TrimEnd()).ToList();
        var trimLen = lines
            .FindAll(x => x != string.Empty)
            .Min(x => x.Length - x.TrimStart().Length);
        return string.Join(Environment.NewLine,
            lines.Select(x => x[Math.Min(x.Length, trimLen)..]));
    }

    public static string TrimNewLines(this string @this) =>
        @this.Trim(Environment.NewLine.ToCharArray());
}
