using MIR;
using Walgelijk;
using Walgelijk.AssetManager;

namespace Inputsoldatcc.Zedifier.Disks;

/// <summary>
/// Zed disk
/// </summary>
public class ZedDisk : ImprobabilityDisk
{
    /// <summary>
    /// Creates an instance of <see cref="ZedDisk"/>
    /// </summary>
    public ZedDisk() : base(
        "ENMESHMENT",
        Assets.Load<Texture>("textures/Zed.png").Value,
        "What is left of your soul is locked to your body.")
    {
        AbilityDescriptors = [
            new(){
                Name = "Infection",
                Description = "Those slain by your hands will wake up as a Zed."
            },
            new(){
                Name = "Rotted Resilience",
                Description = "You are blinded by your unconditional love for eating brains, pain does not affect you."
            }
        ];
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
    }
}
