# Blazor-Captcha
[![NuGet](https://img.shields.io/nuget/v/BlazorCaptcha.svg)](https://www.nuget.org/packages/BlazorCaptcha/)
Generates a captcha image for a Blazor Server application.

![Blazor Captcha](https://github.com/tossnet/Blazor-Captcha/blob/master/blazor-captcha.png)



# Installation
Latest version in here: [![NuGet](https://img.shields.io/nuget/v/BlazorCaptcha.svg)](https://www.nuget.org/packages/BlazorCaptcha/)

To Install

```
Install-Package BlazorCaptcha
```
or
```
dotnet add package BlazorCaptcha
```
For client-side and server-side Blazor - add script section to _Host.cshtml (head section)

```html
<link href="_content/BlazorCaptcha/captcha.css" rel="stylesheet" /
```

## Usage

```html
@page "/"
@using BlazorCaptcha

<h3>Hello, world!</h3>


<Captcha CaptchaWord="@Captcha" OnRefresh="NewCaptchaEvent" />

<p>@Captcha</p>

@code{
    private string Captcha = "";

    private void NewCaptchaEvent()
    {
        Captcha = BlazorCaptcha.Tools.GetCaptchaWord(5);
    }


    protected override Task OnInitializedAsync()
    {
        NewCaptchaEvent();

        return base.OnInitializedAsync();
    }
}
```

