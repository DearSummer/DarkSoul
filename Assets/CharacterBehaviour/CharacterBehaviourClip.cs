using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DS.Role;
using DS.Role.Interface;

[Serializable]
public class CharacterBehaviourClip : PlayableAsset, ITimelineClipAsset
{
    public CharacterBehaviourBehaviour template = new CharacterBehaviourBehaviour ();
    public ExposedReference<ActorController> actorController;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.SpeedMultiplier | ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CharacterBehaviourBehaviour>.Create (graph, template);
        CharacterBehaviourBehaviour clone = playable.GetBehaviour ();
        clone.actorController = actorController.Resolve (graph.GetResolver ());
        return playable;
    }
}
