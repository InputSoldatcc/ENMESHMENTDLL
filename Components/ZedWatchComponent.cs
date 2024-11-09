using System;
using MIR;
using Walgelijk;

namespace Inputsoldatcc.Zedifier.Components;

public class ZedWatchComponent : Walgelijk.Component
{
    /// <summary>
    /// Hooks to the character's death event,
    /// and if the requirements are met, turns them into a zed.
    /// </summary>
    /// <param name="character">The character to watch for.</param>
    public ZedWatchComponent(CharacterComponent character)
    {
        character.OnDeath.AddListener(_ => {
            if (EligibleForEnmeshment())
            {
                // Revive /:
            }
        });
    }

    /// <summary>
    /// Gets if the player is able to turn other characters into zeds.
    /// WARNING: This method is based on assumptions.
    /// </summary>
    /// <returns>If the character will be turned into a zed.</returns>
    public static bool EligibleForEnmeshment()
    {
        if (MadnessUtils.FindPlayer(Game.Main.Scene, out var plr, out var character))
        {
            if (character.EquippedWeapon.TryGet(Game.Main.Scene, out WeaponComponent? weapon))
                return false;
            
            return true;
        }

        return false;
    }
}
