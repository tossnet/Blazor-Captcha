using Microsoft.AspNetCore.Components;
using SkiaSharp.Views.Blazor;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace BlazorCaptcha.WebAssembly;

/// <summary>
/// Captcha component for generating and displaying CAPTCHA images.
/// </summary>
public partial class Captcha : CaptchaComponentBase
{
    /// <summary>
    /// Gets or sets the CAPTCHA word.
    /// </summary>
    [Parameter]
    public string CaptchaWord { get; set; }

    private SKCanvasView skiaView = null!;

    /// <summary>
    /// Refreshes the CAPTCHA word and updates the view.
    /// </summary>
    [SupportedOSPlatform("browser")]
    private async Task OnRefreshInternal()
    {
        CaptchaWord = Tools.GetCaptchaWord(CharNumber);

        skiaView?.Invalidate();

        await CaptchaWordChanged.InvokeAsync(CaptchaWord);
    }

    /// <summary />
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
