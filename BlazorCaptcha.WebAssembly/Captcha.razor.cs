using Microsoft.AspNetCore.Components;
using SkiaSharp.Views.Blazor;
using System.Threading.Tasks;

namespace BlazorCaptcha.WebAssembly;

/// </summary>
public partial class Captcha : CaptchaComponentBase
{
    /// </summary>
    [Parameter]
    public string CaptchaWord { get; set; }

    private SKCanvasView skiaView = null!;

    /// </summary>
    private async Task OnRefreshInternal()
    {
        CaptchaWord = Tools.GetCaptchaWord(CharNumber);

        skiaView?.Invalidate();

        await CaptchaWordChanged.InvokeAsync(CaptchaWord);
    }

    /// </summary>
    private void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        if (string.IsNullOrEmpty(CaptchaWord))
        {
            return;
        }

        var canvas = e.Surface.Canvas;

        CaptchaRenderer.Draw(canvas, Height, Width, CaptchaWord);
    }
}
