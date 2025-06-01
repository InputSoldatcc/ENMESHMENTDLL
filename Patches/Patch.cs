using HarmonyLib;
using MIR;
using Walgelijk;
using Walgelijk.AssetManager;

namespace Inputsoldatcc.Zedifier.Patches
{
    [HarmonyPatch(typeof(Prefabs), nameof(Prefabs.CreatePlayerDeathSequence))]
    public static class PlayerDeathPatch
    {
        public static bool CreatePlayerDeathSequence(Scene scene)
        {
            if (scene.TryGetEntityWithTag(Tags.PlayerDeathSequence, out _))
                throw new Exception("There is already a player death sequence in progress");

            bool isReviveDisk = ImprobabilityDisks.IsEnabled("tricky") || ImprobabilityDisks.IsEnabled("enmesh");

            if (isReviveDisk)
            {
                MadnessUtils.Flash(Colors.Red, 2);
                var snd = SoundCache.Instance.LoadMusicNonLoop(Assets.Load<FixedAudioData>("sounds/tricky_revive.wav"));
                Game.Main.AudioRenderer.Play(snd, 4);
                MadnessUtils.Delay(2f, static () =>
                {
                    MadnessCommands.Revive();
                });
            }
            else
            {
                if (!scene.FindAnyComponent<GameModeComponent>(out var gm) || gm.Mode != GameMode.Experiment)
                {
                    if (scene.FindAnyComponent<MusicPlaylistComponent>(out var playlist))
                        playlist.Stop();
                    if (PersistentSoundHandles.LevelMusic != null)
                        scene.Game.AudioRenderer.Pause(PersistentSoundHandles.LevelMusic);
                }
                scene.Game.AudioRenderer.Play(Sounds.DeathSound);

                MadnessUtils.Delay(1f, static () =>
                {
                    if (Game.Main.Scene.GetEntitiesWithTag(Tags.PlayerDeathSequence).Any())
                        Game.Main.AudioRenderer.Play(Sounds.DeathMusic);
                });
            }
            var entity = scene.CreateEntity();
            scene.AttachComponent(entity, new PlayerDeathSequenceComponent());
            scene.SetTag(entity, Tags.PlayerDeathSequence);

            MadnessUtils.Shake(20);
            return false;
        }
    }
}
