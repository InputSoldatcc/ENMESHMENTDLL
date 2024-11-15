using MIR;
using HarmonyLib;
using Inputsoldatcc.Zedifier.Disks;
using Walgelijk;
using Inputsoldatcc.Zedifier.Components;
using System.Xml.Linq;

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
                        TryAttachZedWatch(character.Entity, character);
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

    public static void TryAttachZedWatch(Entity entity, CharacterComponent character)
    {
        if (MadnessUtils.FindPlayer(Game.Main.Scene, out var _, out var player) && character.Faction != player.Faction)
        {
            // We can attach the zedwatcher to the charactercomponent entity here, and give it the character parameter.
            Game.Main.Scene.AttachComponent(entity, new ZedWatchComponent(character));
        }
    }

    /// <summary>
    /// Tries to add ZedWatch to a character component.
    /// </summary>
    public static void DetectCharacter(Entity entity)
    {
        MadnessUtils.Delay(0.5f, () =>
        {
            if (Game.Main.Scene.TryGetComponentFrom(entity, out CharacterComponent? character))
            {
                TryAttachZedWatch(entity, character);
            }
        });
    }
}
