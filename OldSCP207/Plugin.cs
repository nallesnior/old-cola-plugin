using Exiled.API.Features;
using System;
using MEC;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

namespace OldCola
{
    public class OldCola : Plugin<Config>
    {
        public override Version Version => new Version(1, 0, 0);
        public override string Author => "Naleśnior";
        public override string Name => "OldCola";

        public override void OnEnabled()
        {
            base.OnEnabled();
            Exiled.Events.Handlers.Player.Hurting += onHurting;
            Exiled.Events.Handlers.Player.UsedItem += onUsedItem;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            Exiled.Events.Handlers.Player.Hurting -= onHurting;
            Exiled.Events.Handlers.Player.UsedItem -= onUsedItem;
        }

        private void onHurting(HurtingEventArgs hurtingEvent)
        {
            if (hurtingEvent.DamageHandler.Type == DamageType.Scp207)
            {
                float oldDamage = hurtingEvent.Amount;
                hurtingEvent.Amount *= Config.DamageMultiplier;
            }
        }

        private void onUsedItem(UsedItemEventArgs usedItemEvent)
        {
            if (usedItemEvent.Item.Type == ItemType.SCP207)
            {
                Timing.CallDelayed(0.1f, () =>
                {
                    if (usedItemEvent.Player == null) return;
                    var effect = usedItemEvent.Player.GetEffect<CustomPlayerEffects.Scp207>();

                    if (effect != null && effect.IsEnabled)
                    {
                        byte intensity = effect.Intensity;

                        if (intensity > 0)
                        {
                            byte speedBoost = (byte)(intensity * Config.SpeedBoostPerCola);
                            usedItemEvent.Player.ChangeEffectIntensity(EffectType.MovementBoost, speedBoost);
                        }
                    }
                });
            }
            else if (usedItemEvent.Item.Type == ItemType.SCP500)
            {
                usedItemEvent.Player.DisableEffect(EffectType.MovementBoost);
            }
        }
    }
}
