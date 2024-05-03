﻿using TKR.Shared;
using System;
using TKR.WorldServer.core.objects;
using TKR.WorldServer.core.worlds;

namespace TKR.WorldServer.core.commands
{
    public abstract partial class Command
    {
        internal class ColorChat : Command
        {
            public override RankingType RankRequirement => RankingType.Supporter1;
            public override string CommandName => "chatcolor";

            protected override bool Process(Player player, TickTime time, string color)
            {
                if (string.IsNullOrWhiteSpace(color))
                {
                    player.SendInfo("Usage: /chatcolor <color> \n (use hexcode format: 0xFFFFFF)");
                    return true;
                }

                player.ColorChat = Utils.FromString(color);

                var acc = player.Client.Account;
                acc.ColorChat = player.ColorChat;
                acc.FlushAsync();

                return true;
            }
        }
    }
}
