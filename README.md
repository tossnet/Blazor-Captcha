# Blazor-Captcha
[![NuGet](https://img.shields.io/nuget/v/BlazorCaptcha.svg)](https://www.nuget.org/packages/BlazorCaptcha/)  ![BlazorCaptcha Nuget Package](https://img.shields.io/nuget/dt/BlazorCaptcha)

Generates a captcha image for a Blazor Server or Webassembly application.

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


<details  open="open"><summary>Version 1.6.0</summary>
    
>- issue #12
</details>

<details><summary>Version 1.5.0</summary>
    
>- Add DotNet 8.0 framework target
</details>

<details ><summary>Version 1.4.2</summary>
    
>- Update nuget packages
</details>

<details><summary>Version 1.4.1</summary>
    
>- To avoid any confusion, remove 'x', 'V', 'v' chars
</details>

<details><summary>Version 1.4.0</summary>
    
>- issue #10 Characters did not always fit inside the div
>- remove 'X' and '+' chars
</details>

<details><summary>Version 1.3.0</summary>
    
>- migrate to .NET 7
</details>

<details><summary>Version 1.2.4</summary>
    
>- minor improvement
</details>


<details><summary>Version 1.2.3</summary>
    
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
