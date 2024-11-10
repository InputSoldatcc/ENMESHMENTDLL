using MIR;
using Walgelijk;

namespace Inputsoldatcc.Zedifier.Components;

public class ZedWatchComponent : Component
{
    /// <summary>
    /// Hooks to the character's death event,
    /// and if the requirements are met, turns them into a zed.
    /// </summary>
    /// <param name="character">The character to watch for.</param>
    public ZedWatchComponent(CharacterComponent character)
    {
        character.OnDeath.AddListener(_ => {
            if (EligibleForEnmeshment() && Registries.Stats.TryGet("zed", out var zedStats))
            {   
                CharacterLook characterLook = new(character.Look);
                TryZombify(ref characterLook);
                
                MadnessUtils.Delay(1.4f, () => {
                    if (character != null)
                    {
                        Prefabs.CreateEnemy(
                        Game.Main.Scene, character.Positioning.Body.GlobalPosition, zedStats, characterLook, Registries.Factions["player"]);
                        character.Delete(Game.Main.Scene);
                    }
                });
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

    /// <summary>
    /// Turns the character look into a zombified version.
    /// </summary>
    /// <param name="characterLook">The character look to zombify.</param>
    public static void TryZombify(ref CharacterLook characterLook)
    {
        if (Registries.Armour.Head.TryGet("classic_zed_head", out ArmourPiece? zedHead) && 
            Registries.Armour.HandArmour.TryGet("classic_zed", out HandArmourPiece? zedHand) &&
            Registries.Armour.Body.TryGet("classic_zed_body", out ArmourPiece? zedBody))
        {
            characterLook.Head = zedHead;
            characterLook.Hands = zedHand;
            characterLook.Body = zedBody;
        }
    }
}
