﻿using wServer.core.objects;

namespace wServer.core.commands
{
    public abstract partial class Command
    {
        internal class WorldID : Command
        {
            public WorldID() : base("worldId", permLevel: 110, alias: "wi")
            {
            }

            protected override bool Process(Player player, TickTime time, string color)
            {
                player.SendInfo(player.World.Id.ToString());
                return true;
            }
        }
    }
}
