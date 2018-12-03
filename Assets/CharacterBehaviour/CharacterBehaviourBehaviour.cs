using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DS.Role;
using DS.Role.Interface;

[Serializable]
public class CharacterBehaviourBehaviour : PlayableBehaviour
{
    public ActorController actorController;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }
}
