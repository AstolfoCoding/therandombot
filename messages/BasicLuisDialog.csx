#r "System.Web"
#load "RandomNumGenerator.csx"

using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using static System.Uri;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

// For more information about this template visit http://aka.ms/azurebots-csharp-luis
[Serializable]
public class BasicLuisDialog : LuisDialog<object>
{
    public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(Utils.GetAppSetting("LuisAppId"), Utils.GetAppSetting("LuisAPIKey"))))
    {
    }

    /**If the bot cannot find an intent
    then the bot will let the user know.
    */
    [LuisIntent("None")]
    public async Task NoneIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"I don't know what you mean, but if you want to see a list of my commands, just type command or command list!"); //
        context.Wait(MessageReceived);
    }

    // Intent shows the user what commands are currently available
    [LuisIntent("RandomCommandList")]
    public async Task RandomCommandList(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"I don't have much right now, but you can ask for a random number or random wikipedia article!"); //
        context.Wait(MessageReceived);
    }
    
    /**Intent greets the user after the user greets
    them because who doesn't like a friendly bot?*/
    [LuisIntent("Greeting")]
    public async Task Greeting(IDialogContext context, LuisResult result)
    {
        //int i = 0;
        EntityRecommendation title;
        await context.PostAsync($"Hi! \n");
        if(result.TryFindEntity("PersonalInquiry", out title))
            await context.PostAsync($"Not much!");
        else if(result.TryFindEntity("InquireFeelings", out title))
            await context.PostAsync($"Pretty good!");
        
        context.Wait(MessageReceived);
    }
    /**If wikipedia article or wiki is found in text
    bot will post a random wikipedia article
    Still need to find a way to embed the URL into
    a block of text*/
    [LuisIntent("RandomWiki")]
    public async Task RandomWiki(IDialogContext context, LuisResult result)
    {
        await context.PostAsync("https://en.wikipedia.org/wiki/Special:Random");

        context.Wait(MessageReceived);
    }
    
    /**Intent gives the user a random number
    when asked for it.*/
    [LuisIntent("RandomNumber")]
    public async Task RandomNumber(IDialogContext context, LuisResult result)
    {
        RandomNumGenerator rn = new RandomNumGenerator();
        await context.PostAsync(rn.randNum());

        context.Wait(MessageReceived);
    }
}