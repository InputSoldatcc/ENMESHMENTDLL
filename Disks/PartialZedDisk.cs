using MIR;
using Walgelijk;
using Walgelijk.AssetManager;

namespace Inputsoldatcc.Zedifier.Disks;

/// <summary>
/// Zed disk, but doesnt change your body.
/// </summary>
public class ZedDiskNoBody : ImprobabilityDisk
{
    /// <summary>
    /// Creates an instance of <see cref="ZedDisk"/>
    /// </summary>
    public ZedDiskNoBody() : base(
        "ZED.incomp",
        Assets.Load<Texture>("textures/Zed.png").Value,
        "Same with the other disk, except it doesnt change your body.")
    {
    }

    /// <summary>
    /// Its pretty cool!
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="character"></param>
    public override void Apply(Scene scene, CharacterComponent character)
    {
        var ent = character.Entity;
        
        if (Registries.Stats.TryGet("zed", out var originalZed))
        {
            character.Stats = new()
            {
                HeadHealth = originalZed.HeadHealth,
                BodyHealth = originalZed.BodyHealth,

                AimingRandomness = originalZed.AimingRandomness,
                RecoilHandlingAbility = originalZed.RecoilHandlingAbility,
                DodgeAbility = originalZed.DodgeAbility,
                PanicIntensity = originalZed.PanicIntensity,
                MeleeSkill = originalZed.MeleeSkill,
                WalkHopDuration = originalZed.WalkHopDuration,
                UnarmedSeq = originalZed.UnarmedSeq,
                WalkAnimation = originalZed.WalkAnimation,
            };

            var body = scene.GetComponentFrom<BodyPartComponent>(character.Positioning.Body.Entity);
            var head = scene.GetComponentFrom<BodyPartComponent>(character.Positioning.Head.Entity);
            body.MaxHealth = body.Health = originalZed.BodyHealth;
            head.MaxHealth = head.Health = originalZed.HeadHealth;
        }

        if (Registries.Armour.Head.TryGet("classic_zed_head", out ArmourPiece? zedHead) && 
            Registries.Armour.HandArmour.TryGet("classic_zed", out HandArmourPiece? zedHand))
        {
            character.Look.Head = zedHead;
            character.Look.Hands = zedHand;
        }
    }
}
