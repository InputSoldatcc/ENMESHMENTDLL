using MIR;
using HarmonyLib;
using Inputsoldatcc.Zedifier.Disks;

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
    }

    /// <summary>
    /// Called when the game closes and this mod is unloaded.
    /// </summary>
    public void OnUnload()
    {
    }
}
