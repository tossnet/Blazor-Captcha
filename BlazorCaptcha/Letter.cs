using SkiaSharp;

namespace BlazorCaptcha
{
    public class Letter
    {
        public int Angle { get; set; }
        public string Value { get; set; }
        public SKColor ForeColor { get; set; }
        public string Family { get; set; }
    }
}
