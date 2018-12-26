using UnityEngine;
using UnityEngine.Playables;

namespace DS.Game.Playable
{
    [System.Serializable]
    public class TimeScaleAsset : PlayableAsset
    {

        public AnimationCurve timeScaleCurve;

        // Factory method that generates a playable based on this asset
        public override UnityEngine.Playables.Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var script = ScriptPlayable<TimeScalePlayable>.Create(graph);
            script.GetBehaviour().curve = timeScaleCurve;
            return script;
        }
    }
}
