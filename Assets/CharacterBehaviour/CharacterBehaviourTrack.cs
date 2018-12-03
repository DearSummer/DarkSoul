using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DS.Role;
using DS.Role.Interface;

[TrackColor(0f, 0.8023882f, 1f)]
[TrackClipType(typeof(CharacterBehaviourClip))]
[TrackBindingType(typeof(IActorManager))]
public class CharacterBehaviourTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<CharacterBehaviourMixerBehaviour>.Create (graph, inputCount);
    }
}
