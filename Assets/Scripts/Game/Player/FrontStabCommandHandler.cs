using System.Collections.Generic;
using Cinemachine;
using DS.Game.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace DS.Game.Player
{
    [RequireComponent(typeof(PlayableDirector))]
    public class FrontStabCommandHandler : GameCommandHandler
    {
        public CinemachineTargetGroup cinemachineGroup;
        public PlayableAsset playableAsset;

        private PlayableDirector director;
        private Animator anim;


        private readonly Dictionary<int, PlayableBinding> playableBindingDir = new Dictionary<int, PlayableBinding>();

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
            anim = GetComponent<Animator>();


            foreach (var outpus in playableAsset.outputs)
            {
                if (!playableBindingDir.ContainsKey(outpus.streamName.GetHashCode()))
                {
                    playableBindingDir.Add(outpus.streamName.GetHashCode(), outpus);
                }
            }

        }

        public override void PerformInteractor(object sender)
        {
            GameObject target = (GameObject) sender;
            Play(target.GetComponent<Animator>());
        }



        private void Play(Animator targetAnimator)
        {
            director.playableAsset = Instantiate(playableAsset);

            director.SetGenericBinding(playableBindingDir["attackerAnim".GetHashCode()].sourceObject, anim);
            director.SetGenericBinding(playableBindingDir["receiverAnim".GetHashCode()].sourceObject, targetAnimator);

            director.Play();
        }
        
    }
}
