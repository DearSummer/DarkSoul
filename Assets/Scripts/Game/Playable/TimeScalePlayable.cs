using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// A behaviour that is attached to a playable
namespace DS.Game.Playable
{
    public class TimeScalePlayable : PlayableBehaviour
    {
        public AnimationCurve curve;

        private float curTime;
        private float maxTime;

        // Called when the owning graph starts playing
        public override void OnGraphStart(UnityEngine.Playables.Playable playable) {
		
        }

        // Called when the owning graph stops playing
        public override void OnGraphStop(UnityEngine.Playables.Playable playable)
        {
            Time.timeScale = 1;
        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(UnityEngine.Playables.Playable playable, FrameData info)
        {
            maxTime = (float) playable.GetDuration();
        }

        // Called when the state of the playable is set to Paused
        public override void OnBehaviourPause(UnityEngine.Playables.Playable playable, FrameData info) {
		
        }

        // Called each frame while the state is set to Play
        public override void PrepareFrame(UnityEngine.Playables.Playable playable, FrameData info)
        {
            curTime += Time.deltaTime;
            Time.timeScale = curve.Evaluate(curTime / maxTime);
        }
    }
}
