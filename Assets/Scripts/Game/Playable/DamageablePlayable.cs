using DS.Game.DamageSystem;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
namespace DS.Game.Playable
{
    public class DamageablePlayable : PlayableBehaviour
    {

        public Damageable damageable;
        public DamageData data;

        // Called when the owning graph starts playing
        public override void OnGraphStart(UnityEngine.Playables.Playable playable) {
		
        }

        // Called when the owning graph stops playing
        public override void OnGraphStop(UnityEngine.Playables.Playable playable) {
		
        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(UnityEngine.Playables.Playable playable, FrameData info)
        {
            damageable.TryGetDamage(data);
        }

        // Called when the state of the playable is set to Paused
        public override void OnBehaviourPause(UnityEngine.Playables.Playable playable, FrameData info) {
		
        }

        // Called each frame while the state is set to Play
        public override void PrepareFrame(UnityEngine.Playables.Playable playable, FrameData info) {
		
        }
    }
}
