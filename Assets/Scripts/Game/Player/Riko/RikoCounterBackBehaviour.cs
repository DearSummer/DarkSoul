using System.Collections.Generic;
using DS.Game.Core;
using DS.Game.DamageSystem;
using DS.Game.Effect;
using DS.Game.Playable;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DS.Game.Player.Riko
{
    public class RikoCounterBackBehaviour : MonoLinkedStateMachineBehviour<PlayerController>
    {

        public PlayableAsset playableAsset;

        private PlayableDirector director;
        private readonly Dictionary<int, PlayableBinding> playableBindingDir = new Dictionary<int, PlayableBinding>();


        protected override void OnStart(Animator animator)
        {
            director = monoBehaviour.GetComponent<PlayableDirector>();
            foreach (var outputs in playableAsset.outputs)
            {
                if (!playableBindingDir.ContainsKey(outputs.streamName.GetHashCode()))
                {
                    playableBindingDir.Add(outputs.streamName.GetHashCode(), outputs);
                }
            }

        }



        protected override void OnStateUpdateWithoutTrasnlation(Animator animator, AnimatorStateInfo info, int layerIndex)
        {
            if (monoBehaviour.playerInput.Attack && monoBehaviour.targetEnemy != null)
            {
                Play(animator, monoBehaviour.targetEnemy.GetComponent<Animator>());
                monoBehaviour.weapon.StartAttack();
                monoBehaviour.targetEnemy = null;
            }
        }



        private void Play(Animator playerAnimator,Animator targetAnimator)
        {
            director.playableAsset = Instantiate(playableAsset);

            director.SetGenericBinding(playableBindingDir["attackerAnim".GetHashCode()].sourceObject, playerAnimator);
            director.SetGenericBinding(playableBindingDir["receiverAnim".GetHashCode()].sourceObject, targetAnimator);

            BindData(playerAnimator, targetAnimator);

            director.Play();
        }

        private void BindData(Animator attacker,Animator target)
        {
            PlayableTrack playableTrack  =
                (PlayableTrack) playableBindingDir["Event Track".GetHashCode()].sourceObject;


            foreach (var clips in playableTrack.GetClips())
            {
                object asset = clips.asset;
                if(!(asset is ShowEffectAsset))
                    continue;

                ShowEffectAsset showEffectAsset = (ShowEffectAsset) asset;
                showEffectAsset.effectEventReference = new ExposedReference<EffectEvent>
                {
                    defaultValue = monoBehaviour.GetComponent<EffectEvent>()
                };
                showEffectAsset.eventName = "FrontStabEffect";
            }

            playableTrack = (PlayableTrack) playableBindingDir["Character State Lock".GetHashCode()].sourceObject;

            foreach (var clips in playableTrack.GetClips())
            {
                if (!(clips.asset is CharacterStateLockAsset))
                    continue;


                CharacterStateLockAsset characterStateLockAsset = (CharacterStateLockAsset) clips.asset;

                characterStateLockAsset.attackerAnimator = new ExposedReference<Animator> {defaultValue = attacker};
                characterStateLockAsset.receiverAnimator = new ExposedReference<Animator> {defaultValue = target};
            }

            playableTrack = (PlayableTrack) playableBindingDir["Damageable Track".GetHashCode()].sourceObject;
            foreach (var clips in playableTrack.GetClips())
            {
                if (!(clips.asset is DamageableAsset))
                    continue;


                DamageableAsset damageableAsset = (DamageableAsset) clips.asset;

                damageableAsset.damageable =
                    new ExposedReference<Damageable> {defaultValue = target.GetComponent<Damageable>()};

                DamageData data = new DamageData()
                {
                    attacker = attacker.gameObject,
                    damage = monoBehaviour.weapon.damage * 2,
                    damagePos = monoBehaviour.weapon.transform.position,
                    direction = target.transform.position - attacker.transform.position
                };

                damageableAsset.data = data;

            }

        }
    }

}
