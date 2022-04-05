# Blazor-Captcha
[![NuGet](https://img.shields.io/nuget/v/BlazorCaptcha.svg)](https://www.nuget.org/packages/BlazorCaptcha/)  ![BlazorCaptcha Nuget Package](https://img.shields.io/nuget/dt/BlazorCaptcha)

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
<link href="_content/BlazorCaptcha/captcha.css" rel="stylesheet" />
```

## Usage

```html
@page "/"
@using BlazorCaptcha

<h3>Hello, world!</h3>


<Captcha @bind-CaptchaWord="@Captcha" CharNumber="@CaptchaLetters" />

<p>@Captcha</p>

@code{
    private string Captcha = "";
    private int CaptchaLetters = 5;

    protected override Task OnInitializedAsync()
    {
        Captcha = BlazorCaptcha.Tools.GetCaptchaWord(CaptchaLetters);

        return base.OnInitializedAsync();
    }
}
```

## <a name="ReleaseNotes"></a>Release Notes

<details open="open"><summary>Version 1.2.3</summary>
    
>- Add type="button"
</details>


<details><summary>Version 1.2.2</summary>
    
>- add nuget package SkiaSharp.NativeAssets.Linux
</details>

<details><summary>Version 1.2.0</summary>
    
>- NET6, removal of the bootstrap class
</details>

### ⚠️ Breaking changes ⚠️

<details><summary>Version  1.0.1 to 1.1.0</summary>
    
>- Change the parameter "CaptchaWord" => "@bind-CaptchaWord"
>- Add the parameter with the length of the word ex : "CharNumber="@CaptchaLetters"
>- Remove the "OnRefresh" parameter
</details>
