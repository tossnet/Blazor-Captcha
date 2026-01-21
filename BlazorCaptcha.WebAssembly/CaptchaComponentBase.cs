using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorCaptcha;

/// <summary>
///     
/// </summary>
public abstract class CaptchaComponentBase : ComponentBase
{
    /// <summary>
    ///     
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 170;

    /// <summary>
    ///     
    /// </summary>
    [Parameter]
    public int Height { get; set; } = 40;

    /// <summary>
    ///     
    /// </summary>
    [Parameter]
    public int CharNumber { get; set; } = 5;

    /// <summary>
    ///     
    /// </summary>
    [Parameter]
    public EventCallback<string> CaptchaWordChanged { get; set; }

    /// <summary>
    ///     
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnRefresh { get; set; }

}
