using System.Drawing;

namespace BlazorCaptcha
{
    public class Letter
    {
        public int Angle { get; set; }
        public string Value { get; set; }
        public Font Font { get; set; }
        public Color ForeColor { get; set; }
        public string Family { get; set; }
    }
}
