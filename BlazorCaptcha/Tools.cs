namespace BlazorCaptcha;

using System;
using System.Linq;

public static class Tools
{
    public static string GetCaptchaWord(int length)
    {
        var random = new Random(DateTime.Now.Millisecond);

        const string chars = "ABCDEFGHJKLMNPQRSTUWYZabcdefghijkmnpqrstuwz23456789*#!$%=";
        string cw = new(Enumerable.Repeat(chars, length)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());

        return cw;
    }
}
