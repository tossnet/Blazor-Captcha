using SkiaSharp;

namespace BlazorCaptcha;

/// <summary />
internal class Letter
{
    public int Angle { get; set; }
    public required string Value { get; set; }
    public SKColor ForeColor { get; set; }
    public required string Family { get; set; }
}
