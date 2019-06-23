using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace MCBot.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "world"), Summary("Hello World command")]
        public async Task Hello()
        {
            await Context.Channel.SendMessageAsync("Hello World");
        }

        [Command("embed"), Summary("Embed test command")]
        public async Task Embed([Remainder]string Input = "None")
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Test embed", Context.User.GetAvatarUrl());
            Embed.WithColor(Color.Blue);
            Embed.WithFooter("The footer of the embed", Context.Guild.Owner.GetAvatarUrl());
            Embed.WithDescription("This is a dummy description, with a cool link.\n" +
                                  "[This is my favorite website](https://api.mcsrvstat.us/2/147.135.38.232:25597)");
            Embed.AddField("User input: ", Input);

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
