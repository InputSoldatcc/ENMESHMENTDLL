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
    }

    /// <summary>
    /// Tries to find a character from the <paramref name="entity"/>
    /// </summary>
    public static void DetectCharacter(Entity entity)
    {
    }
}
