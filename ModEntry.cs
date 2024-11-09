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
        ImprobabilityDisks.All.Add("PartialEnmeshment", new ZedDiskNoBody());

        // Attach plz
        Game.Main.OnSceneChange.AddListener(Scene => {
            if (Scene.New != null && (ImprobabilityDisks.IsEnabled("Enmeshment") || ImprobabilityDisks.IsEnabled("PartialEnmeshment")))
                Scene.New.OnCreateEntity += TryAttachZedWatch;
        });
    }

    /// <summary>
    /// Called when the game closes and this mod is unloaded.
    /// </summary>
    public void OnUnload()
    {
    }

    /// <summary>
    /// Tries to add ZedWatch to a character component.
    /// </summary>
    public static void TryAttachZedWatch(Entity entity)
    {
        if (Game.Main.Scene.TryGetComponentFrom<CharacterComponent>(entity, out CharacterComponent? character))
        {
            // We can attach the zedwatcher to the charactercomponent entity here, and give it the character parameter.
            Game.Main.Scene.AttachComponent<ZedWatchComponent>(entity, new ZedWatchComponent(character));
        }
    }
}
