using SkiaSharp;

namespace BlazorCaptcha;

/// <summary />
public class Letter
{
    public int Angle { get; set; }
    public required string Value { get; set; }
    public SKColor ForeColor { get; set; }
    public required string Family { get; set; }
}
