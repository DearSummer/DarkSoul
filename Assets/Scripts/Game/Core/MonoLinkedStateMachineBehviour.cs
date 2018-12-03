using UnityEditor;
using UnityEngine;

namespace DS.Game.Core
{
    public class MonoLinkedStateMachineBehviour<T> : SealedStateMachineBehaviour
        where T : MonoBehaviour
    {
        protected T monoBehaviour;

        private bool isFristFrame = false;

        public static void Initialise(Animator animator, T behaviour)
        {
            MonoLinkedStateMachineBehviour<T>[] behviours = animator.GetBehaviours<MonoLinkedStateMachineBehviour<T>>();

            for (int i = 0; i < behviours.Length; i++)
            {
                behviours[i].InternalInitialise(animator,behaviour);
            }
        }

        private void InternalInitialise(Animator animator, T behaviour)
        {
            monoBehaviour = behaviour;
            OnStart(animator);          
        }

        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            isFristFrame = false;

            OnLinkedStateEnter(animator, animatorStateInfo, layerIndex);
        }

        public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if(!animator.IsInTransition(layerIndex) && !isFristFrame)
                OnStateUpdateWithoutTrasnlation(animator,animatorStateInfo,layerIndex);

            if (!animator.IsInTransition(layerIndex) && isFristFrame)
                isFristFrame = true;
        }

        protected virtual void OnStart(Animator animator) { }
        protected virtual void OnLinkedStateEnter(Animator animator,AnimatorStateInfo info,int layerIndex) { }
        protected virtual void OnStateUpdateWithoutTrasnlation(Animator animator,AnimatorStateInfo info,int layerIndex) { }
    }
}
