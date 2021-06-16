﻿<!-- PROJECT SHIELDS -->

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

[contributors-shield]: https://img.shields.io/github/contributors/blazorhero/CleanArchitecture.svg?style=flat-square
[contributors-url]: https://github.com/blazorhero/CleanArchitecture/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/blazorhero/CleanArchitecture?style=flat-square
[forks-url]: https://github.com/blazorhero/CleanArchitecture/network/members
[stars-shield]: https://img.shields.io/github/stars/blazorhero/CleanArchitecture.svg?style=flat-square
[stars-url]: https://img.shields.io/github/stars/blazorhero/CleanArchitecture?style=flat-square
[issues-shield]: https://img.shields.io/github/issues/blazorhero/CleanArchitecture?style=flat-square
[issues-url]: https://github.com/blazorhero/CleanArchitecture/issues
[license-shield]: https://img.shields.io/github/license/blazorhero/CleanArchitecture?style=flat-square
[license-url]: https://github.com/blazorhero/CleanArchitecture/blob/master/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/iammukeshm/

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/blazorhero/CleanArchitecture">
    <img src="https://codewithmukesh.com/wp-content/uploads/2021/04/BlazorHeroBannerv2.jpg" alt="Blazor Hero">
  </a>
  <h3 align="center">BlazorHero - Clean Architecture Template</h3>
  <p align="center">
    Open Sourced Solution Template For Blazor Web-Assembly 5.0 built with MudBlazor Components
    <br />
    <a href="https://codewithmukesh.com/blog/blazor-hero-quick-start-guide/"><strong>Read the Documentation »</strong></a>
    <br />
    <br />
    <a href="https://github.com/blazorhero/CleanArchitecture/issues">Report Bug</a>
    ·
    <a href="https://github.com/blazorhero/CleanArchitecture/issues">Request Feature</a>
  </p>
</p>

## About The Project :zap:

BlazorHero is a Clean Architecture Solution Template for Blazor Webassembly 5.0 built with MudBlazor Components.

## Complete Overview - Youtube Video 🆕 📈

So, here is an in-depth video that takes you through the BlazorHero Project! Do Like & Subscribe to my Youtube channel! It would be great if you could leave behind your valuable feedback in the comments section of the Video. This helps me reach a much wider audience with time :)

<a href="https://www.youtube.com/watch?v=j7BxKN7HYjk&lc=Ugy1SjDcmP0pboRxJe94AaABAg">Watch it here!</a>

[![Blazor Hero - Clean Architecture Solution Template for Blazor WebAssembly](http://img.youtube.com/vi/j7BxKN7HYjk/0.jpg)](http://www.youtube.com/watch?v=j7BxKN7HYjk "Blazor Hero - Clean Architecture Solution Template for Blazor WebAssembly")

### Tech Stack :muscle:

- Blazor WebAssembly 5.0 - ASP.NET Core Hosted Model
- [Entity Framework Core 5.0](https://docs.microsoft.com/en-us/ef/core/)

# New Features of BlazorHero v2.0

- [x] Play Notification Tone when a new Chat Message is received.
- [x] Auto Scroll to Last Message when a new Chat Message is received.
- [x] Registration Page for Unauthorized User (Currently only Admins can register new users)
- [x] Realtime Notifications - Dashboard Updates Realtime
- [x] Logout Users / Regenerate Token from Multiple Client Browsers when Permission Changes
- [x] FIX: Token Issue Fixed from v1.0.1
- [x] User Images in Chat Component
- [x] Chat - Integrated with Identity to support Private Chats (Will require re-migrating the DB scehmas)
- [x] Notifications System using SignalR
- [x] Document Management
- [x] Export to Excel
- [x] Audit Trails
- [x] FIX: Image Upload - Shift to File System from Encoded String. (REASON: Can be heavy on the db and bandwidth consumption, API responses may look huge and ugly.)
- [x] FIX: Code Cleanup.
- [x] FIX: Validations on User Registration - Show Snackbar on Exceptions / Validation Errors.
- [x] FIX: Remove AutoMigrations - Causes SQL Exceptions at times.

# Getting Started 🦸

> **Important**
If you are already using Blazor Hero v1.x, make sure that you drop your existing database and re-update your database using the CLI as there are couple of new migrations added that may clash with your existing schema. Also, install the latest version of BlazorHero.

The easiest way to get started with Blazor Hero is to install the [NuGet package](https://www.nuget.org/packages/BlazorHero.CleanArchitecture/) and run `dotnet new BlazorHero.CleanArchitecture`:

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Install the latest DOTNET & EF CLI Tools by using this command `dotnet tool install --global dotnet-ef` 
3. Install the latest version of Visual Studio IDE 2019 (v16.8 and above) 🚀
4. Open up Command Prompt and run `dotnet new --install BlazorHero.CleanArchitecture` to install the project template
5. Create a folder for your solution and cd into it (the template will use it as project name)
6. Run `dotnet new BlazorHero.CleanArchitecture` to create a new Solution with all the Awsomeness 🕶️ of BlazorHero 🦸
7. Set the Envioroment Varible in DOS shell: `set DOTNET_ENVIRONMENT=Test` or `set $env:DOTNET_ENVIRONMENT = 'Development'`
8. Run database initialization: open youre server folder, run: `dotnet ef database update --startup-project {Solution}\Server\{Solution}.Server.csproj -- --environment Development` (or a other spcifice Environment Production or Test) [.EF Commands](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) (*BUG - for some reasons this works only in old Command-Prompt by setting environment variable before as described in 7.)

What to do next? Read the [entire guide on my blog](https://codewithmukesh.com/blog/blazor-hero-quick-start-guide/).


# Complete Documentation :rocket:

Getting started with Blazor Hero – A Clean Architecture Template built for Blazor WebAssembly using MudBlazor Components. This project will make your Blazor Learning Process much easier than you anticipate. Blazor Hero is meant to be an Enterprise Level Boilerplate, which comes free of cost, completely open sourced. 

The provided documentation / guide will get you started with BlazorHero in no-time. It provides a complete walkthrough about the project with to-the-point guides and notes.

<a href="https://codewithmukesh.com/blog/blazor-hero-quick-start-guide/"><strong>Read the Quick Start Guide</strong></a>

# Features

All the completed and the upcoming features are mentioned in the [Features.MD File](https://github.com/blazorhero/CleanArchitecture/blob/master/Features.md)

## Contributing

Contributions are what make the open-source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

Here are the few contributions that I would highly appreciate ;)

- [ ] Need someone to add in the API Documentation for Swagger.
- [ ] Need someone to implement localization throughout every Razor Component of the solution under the WASM(Client) Project. You can take the Pages/Authentication/Login.razor as the point of reference. It is as simple as adding `@inject Microsoft.Extensions.Localization.IStringLocalizer<Login> localizer` to every page, changing the texts to `@localizer["Text Here"]` and finally adding resx files to the Resources Folder as per the folder structure.
- [ ] Need few contributors to add in various language transalations as per the implemented Location. I got time to only add a few transalations for French as of now.
- [ ] Need a UI contributor to look at the UX/UI of the entire project
- [ ] Need someone to buildup a cool Material Logo for BlazorHero (BH):D Do contact me on LinkedIn (https://www.linkedin.com/in/iammukeshm/).
- [ ] And finally, Stars from everyone! :D

## License

Distributed under the MIT License.

## Contact
### Mukesh Murugan

-   Blogs at [codewithmukesh.com](https://www.codewithmukesh.com)
-   Facebook - [codewithmukesh](https://www.facebook.com/codewithmukesh)
-   Twitter - [Mukesh Murugan](https://www.twitter.com/iammukeshm)
-   Twitter - [codewithmukesh](https://www.twitter.com/codewithmukesh)
-   Linkedin - [Mukesh Murugan](https://www.linkedin.com/in/iammukeshm/)

## Support :star:

Has this Project helped you learn something New? or Helped you at work? Do Consider Supporting. 
Here are a few ways by which you can support.

-   Leave a star! :star:
-   Recommend this awesome project to your colleagues. 🥇
-   Leave your feedback / comments regarding this project in the comments section on my blog [Blazor Hero Blog](https://codewithmukesh.com/blog/blazor-hero-quick-start-guide/)
-   Do consider endorsing me on LinkedIn for ASP.NET Core - [Connect via LinkedIn](https://codewithmukesh.com/linkedin) 🦸
-   Or, If you want to support this project on the long run, [consider buying me a coffee](https://www.buymeacoffee.com/codewithmukesh)! ☕

<a href="https://www.buymeacoffee.com/codewithmukesh" target="_blank"><img src="https://codewithmukesh.com/wp-content/uploads/2021/04/bmclogo.jpg" alt="Buy Me A Coffee" width="200"  style="height: 60px !important;width: 200px !important;" ></a>
