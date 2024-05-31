﻿using Shared;
using Shared.resources;
using System;
using System.Collections.Generic;
using WorldServer.core.net.datas;
using WorldServer.core.structures;
using WorldServer.core.worlds;
using WorldServer.networking.packets.outgoing;
using WorldServer.utils;

namespace WorldServer.core.objects
{
    public partial class Player
    {
        private readonly int[] _slotEffectCooldowns = new int[4];

        private void TryApplySpecialEffects(ref TickTime time)
        {
            /*(for (var slot = 0; slot < 4; slot++)
            {
                if (CantApplySlotEffect(slot))
                    continue;

                var item = Inventory[slot];
                if (item == null)
                    continue;
            }*/
        }

        private void TryAddOnEnemyHitEffect(ref TickTime tickTime, Enemy entity, ProjectileDesc projectileDesc)
        {
            /*for (var slot = 0; slot < 4; slot++)
            {
                if (CantApplySlotEffect(slot))
                    continue;

                var item = Inventory[slot];
                if (item == null)
                    continue;
            }*/
        }

        private void TryAddOnPlayerEffects(string type, int dmg = 0)
        {
            for (var slot = 0; slot < 4; slot++) {
                if (CantApplySlotEffect(slot))
                    continue;

                var item = Inventory[slot];
                if (item == null)
                    continue;

                ActivateEffect[] effs;
                switch (type) {
                    case "hit": effs = item.OnPlayerHitActivateEffects; break;
                    case "shoot": effs = item.OnPlayerShootActivateEffects; break;
                    case "ability": effs = item.OnPlayerAbilityActivateEffects; break;
                    case "passive": effs = item.OnPlayerPassiveActivateEffects; break;
                    case "enemyHit": effs = item.OnEnemyHitActivateEffects; break;
                    default: continue;
                }

                foreach (ActivateEffect eff in effs) {
                    if (eff.Proc != 0) {
                        var rand = new Random();
                        var doub = rand.NextDouble();
                        if (eff.Proc < doub)
                            continue;
                    }
                    if (eff.RequiredConditions != null)
                        if (!HasConditionEffect(StringToConditionEffect(eff.RequiredConditions)))
                            continue;
                    if (eff.DamageThreshold != 0)
                        if (dmg < eff.DamageThreshold)
                            continue;
                    if (eff.HealthThreshold != 0)
                        if (Health > eff.HealthThreshold)
                            continue;
                    if (eff.HealthRequired != 0)
                        if (Health < eff.HealthRequired)
                            continue;
                    if (eff.HealthRequiredRelative != 0)
                        if (Health / MaxHealth < eff.HealthRequiredRelative)
                            continue;
                    if (eff.ManaCost != 0)
                        if (Mana < eff.ManaCost)
                            continue;
                    if (eff.ManaRequired != 0)
                        if (Mana < eff.ManaRequired)
                            continue;
                    OnOtherActivate(type, item, Position);
                    SetSlotEffectCooldown(eff.Cooldown, slot);
                }
            }
        }
    }
}
