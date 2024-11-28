using MIR;
using Walgelijk;

namespace Inputsoldatcc.Zedifier.Components;

public class EnmeshmentComponent : Component
{
    /// <summary>
    /// The character.
    /// </summary>
    public CharacterComponent? Character
    {
        get
        {
            if (Game.Main.Scene.TryGetComponentFrom(Entity, out CharacterComponent? character))
            {
                return Character;
            }
            return null;
        }
    }

    /// <summary>
    /// Watches for the character's death, and turns them into a zed.
    /// </summary>
    public EnmeshmentComponent()
    {
        Character?.OnDeath.AddListener(_ =>
        {
            CharacterLook characterLook = new(Character.Look);
            Zombify(ref characterLook);

            MadnessUtils.Delay(1.4f, () =>
            {
                if (Character != null && Registries.Stats.TryGet("zed", out var zedStats) && EligibleForEnmeshment())
                {
                    Prefabs.CreateEnemy(
                        Game.Main.Scene, Character.Positioning.Body.GlobalPosition, zedStats, characterLook, Registries.Factions["player"]
                    );

                    Character.Delete(Game.Main.Scene);
                }
            });
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
    public static void Zombify(ref CharacterLook characterLook)
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
