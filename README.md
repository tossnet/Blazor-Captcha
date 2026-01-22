# Blazor-Captcha
[![NuGet](https://img.shields.io/nuget/v/BlazorCaptcha.svg)](https://www.nuget.org/packages/BlazorCaptcha/)  ![BlazorCaptcha Nuget Package](https://img.shields.io/nuget/dt/BlazorCaptcha)

Generates a captcha image for a Blazor Server or Webassembly application.

## Live demo
 Blazor webassembly :  <a href="https://tossnet.github.io/Blazor-Captcha/" target="_blank">[https://tossnet.github.io/Blazor-Captcha/](https://tossnet.github.io/Blazor-Captcha/)</a> 

> [!WARNING]
> **⚠️ WebAssembly Demo Currently Broken ⚠️**
> 
> The live demo on GitHub Pages is currently not working due to a SkiaSharp initialization error:
> ```
> Uncaught ManagedError: TypeInitialization_Type, SkiaSharp.SKImageInfo
> ```
> 
> **The component works correctly locally** (both debug and release), but fails when deployed to GitHub Pages. This issue appeared after version 2.0.2 and persists despite rollback attempts.
> 
> **🙏 Help Wanted:** If you have experience with SkiaSharp + Blazor WebAssembly deployment issues and find a solution, please [open an issue using our template](https://github.com/tossnet/Blazor-Captcha/issues/new?template=skiasharp-wasm-fix.yml) or submit a PR - it would be greatly appreciated!
> 
> Related discussion: https://github.com/mono/SkiaSharp/discussions/3185
 
![Blazor Captcha](https://github.com/tossnet/Blazor-Captcha/blob/master/blazor-captcha.png)


# Installation

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
        Captcha = BlazorCaptcha.Commun.Tools.GetCaptchaWord(CaptchaLetters);

        return base.OnInitializedAsync();
    }
}
```

## <a name="ReleaseNotes"></a>Release Notes

<details open="open"><summary>Version 2.1.0</summary>
    
>- Add .NET 10 support
>- Code optimization
>- Captcha improvement
>- Change license to MIT
</details>

<details><summary>Version 2.0.0</summary>
    
>- Due to the Skiasharp problem : https://github.com/mono/SkiaSharp/discussions/3185#discussioncomment-12410708,a special component for Blazor WebAssembly has been created. Currently only compatible with .NET 8 for the WASM part

>- .NET 7 compatibility removed
</details>

<details><summary>Version 1.7.1</summary>
    
>- https://github.com/tossnet/Blazor-Captcha/issues/20#issuecomment-2549029344
</details>


<details><summary>Version 1.6.0</summary>
    
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
