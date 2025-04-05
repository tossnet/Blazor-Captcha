namespace BlazorCaptcha;

/// <summary />
public static class Tools
{
    /// <summary />
    public static string GetCaptchaWord(int length)
    {
        var random = new Random(DateTime.Now.Millisecond);

        const string chars = "ABCDEFGHJKLMNPQRTUWYZabcdefghijkmnpqrtuwz23456789*#!$%=@?";
        string cw = new(Enumerable.Repeat(chars, length)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());

        return cw;
    }
}
