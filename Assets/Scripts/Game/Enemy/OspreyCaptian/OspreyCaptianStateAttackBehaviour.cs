using DS.Game.Core;
using UnityEngine;

namespace DS.Game.Enemy.OspreyCaptian
{
    public class OspreyCaptianStateAttackBehaviour : MonoLinkedStateMachineBehviour<OspreyCaptianBehaviour>
    {
        protected override void OnLinkedStateEnter(Animator animator, AnimatorStateInfo info, int layerIndex)
        {
            monoBehaviour.Controller.SetFollowNavMesh(false);
        }
    }
}
