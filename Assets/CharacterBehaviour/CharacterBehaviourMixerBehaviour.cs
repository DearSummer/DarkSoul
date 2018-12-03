using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DS.Role;
using DS.Role.Interface;

public class CharacterBehaviourMixerBehaviour : PlayableBehaviour
{
    // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        IActorManager trackBinding = playerData as IActorManager;

        if (!trackBinding)
            return;

        int inputCount = playable.GetInputCount ();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<CharacterBehaviourBehaviour> inputPlayable = (ScriptPlayable<CharacterBehaviourBehaviour>)playable.GetInput(i);
            CharacterBehaviourBehaviour input = inputPlayable.GetBehaviour ();
            
            // Use the above variables to process each frame of this playable.
            
        }
    }
}
