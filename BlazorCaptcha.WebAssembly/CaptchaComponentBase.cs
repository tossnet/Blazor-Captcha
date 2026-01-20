using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorCaptcha;

/// </summary> 
public abstract class CaptchaComponentBase : ComponentBase
{
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 170;

    /// </summary>
    [Parameter]
    public int Height { get; set; } = 40;

    /// </summary>
    [Parameter]
    public int CharNumber { get; set; } = 5;

    /// </summary>
    [Parameter]
    public EventCallback<string> CaptchaWordChanged { get; set; }

    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnRefresh { get; set; }

}
