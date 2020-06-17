I never made peace with JavaScript. This left me in a really awkward position when JS took over web development a while back. .NET was still relevant on the back-end, but had limitations on the front-end. While Razor is really productive to write, as server-rendered markup, it's hard to deliver the rich client experiences users expect today from web apps. I went the jQuery route, adding as much "richness" as I could on the client side by starting with Razor, then adding a generous helping of jQuery-oriented JS. But this was never very productive, and was the reason JS kept such a bad taste in my mouth. In the meantime, a new generation of developers had embraced JS. I knew there was a rich ecosystem of front-end development frameworks out there. But I resented the apparent obsolescence of my .NET skills. And I couldn't swallow what I felt was a huge opportunity cost of essentially *starting over* in my learning curve. I also couldn't abide JS itself as a dynamic language. I don't have a problem with dynamic languages in principle. But as a practical matter, dynamic languages don't handle complexity very well. That, of course, is why there's TypeScript. But here again, I didn't want to *add more things* to the stack. What I yearned for deep down was to be able to focus on application development using a single language to deliver compelling experiences to today's web users. For the first time in the .NET world, Blazor is making this possible. This repo represents my baby steps in this direction.

It turns out I am, in effect, rebuilding my [Ginseng](https://github.com/adamfoneil/Ginseng8) app. I don't expect to get my team to use this new Blazor version, but I *am* going to see how far I can get. I'm incorporating some lessons and feedback from the last iteration, and I'm giving serious thought to all aspects of it. I'm treating this as a real application, in other words.

## Cloning and running it
If you clone the repo, uncomment [this line](https://github.com/adamfoneil/BlazorServerDemo/blob/master/BlazorServerDemo/Startup.cs#L38), and run the app to create the database:

```csharp
LocalDb.CreateFromResourceAsync(Assembly.GetExecutingAssembly(), "BlazorServerDemo.Resources.BlazorServerDemo.zip", "BlazorServerDemo").Wait();
```

## Solution architecture
- [BlazorServerDemo](https://github.com/adamfoneil/BlazorServerDemo/tree/master/BlazorServerDemo) is the app itself
- [Models](https://github.com/adamfoneil/BlazorServerDemo/tree/master/Models) has model classes
- [CmdConsole](https://github.com/adamfoneil/BlazorServerDemo/tree/master/CmdConsole) was just a little thing to create the database zip file
- [Testing](https://github.com/adamfoneil/BlazorServerDemo/tree/master/Testing) is what it sounds like, but as of now just has [QueryTests](https://github.com/adamfoneil/BlazorServerDemo/blob/master/Testing/QueryTests.cs).

## Walkthroughs
As I get around to making demo videos, I'll link them below:
- 6/17/20: a few simple Crud pages: https://1drv.ms/u/s!AvguHRnyJtWMlvsmlGydpZqevIAyoQ?e=0LteG5
