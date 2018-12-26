using System;
using DS.Game.Effect;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DS.Game.Playable
{
    [System.Serializable]
    public class ShowEffectAsset : PlayableAsset
    {

        public ExposedReference<EffectEvent> effectEventReference;
        public string eventName;

        // Factory method that generates a playable based on this asset
        public override UnityEngine.Playables.Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var script = ScriptPlayable<ShowEffectPlayable>.Create(graph);
            script.GetBehaviour().effectEvent = effectEventReference.Resolve(graph.GetResolver());
            script.GetBehaviour().eventName = eventName;

            return script;
        }

        public static explicit operator ShowEffectAsset(TimelineClip v)
        {
            throw new NotImplementedException();
        }
    }
}
