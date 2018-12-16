using DS.Game.Core;
using UnityEngine;

namespace DS.Game.Enemy.OspreyCaptian
{
    public class OspreyCaptianStateStunedBehaviour : MonoLinkedStateMachineBehviour<OspreyCaptianBehaviour> {

        protected override void OnLinkedStateEnter(Animator animator, AnimatorStateInfo info, int layerIndex)
        {
            monoBehaviour.frontStubCommand.enabled = true;
        }

        protected override void OnLinkedStateExit(Animator animator, AnimatorStateInfo info, int layerIndex)
        {
            monoBehaviour.frontStubCommand.enabled = false;
        }
    }
}
