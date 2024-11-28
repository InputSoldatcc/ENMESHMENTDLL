using MIR;
using HarmonyLib;
using Inputsoldatcc.Zedifier.Disks;
using Walgelijk;
using Inputsoldatcc.Zedifier.Components;

namespace Inputsoldatcc.Zedifier;

public class ModEntry : IModEntry
{
    /// <summary>
    /// Called immediately after this mod is loaded. Beware that some resources might not yet be present.
    /// </summary>
    /// <param name="mod">Your mod instance</param>
    /// <param name="harmony">Your Harmony instance</param>
    public void OnLoad(Mod mod, Harmony harmony)
    {
    }

    /// <summary>
    /// Called when everything is ready.
    /// </summary>
    public void OnReady()
    {
        ImprobabilityDisks.All.Add("Enmeshment", new ZedDisk());
        ImprobabilityDisks.SetIncompatible("Enmeshment", "tricky", "grunt", "agent", "engineer", "soldat");
        
        // Attach plz
        Game.Main.OnSceneChange.AddListener(Scenes =>
        {
            if (Scenes.New != null && (ImprobabilityDisks.IsEnabled("Enmeshment") || ImprobabilityDisks.IsEnabled("PartialEnmeshment")))
            {
                MadnessUtils.Delay(1.5f, () => {
                    foreach (var character in Scenes.New.GetAllComponentsOfType<CharacterComponent>())
                        AttachEnmesher(character.Entity, character);
                });

                Scenes.New.OnCreateEntity += DetectCharacter;
            }
        });
    }

    /// <summary>
    /// Called when the game closes and this mod is unloaded.
    /// </summary>
    public void OnUnload()
    {
    }

    /// <summary>
    /// Tries to attach the <see cref="EnmeshmentComponent"/> to the <see cref="CharacterComponent"/>'s <see cref="Entity"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="character"></param>
    public static void AttachEnmesher(Entity entity, CharacterComponent character)
    {
        if (MadnessUtils.FindPlayer(Game.Main.Scene, out var _, out var playerCharacter))
        {
            if (playerCharacter != character)
            {
                Game.Main.Scene.AttachComponent(character.Entity, new EnmeshmentComponent());
            }
        }
    }

    /// <summary>
    /// Tries to find a character from the <paramref name="entity"/>
    /// </summary>
    public static void DetectCharacter(Entity entity)
    {
        _ = MadnessUtils.Delay(1.5f, () =>
        {
            if (Game.Main.Scene.TryGetComponentFrom(entity, out CharacterComponent? character))
            {
                AttachEnmesher(entity, character);
            }
        });
    }
}
