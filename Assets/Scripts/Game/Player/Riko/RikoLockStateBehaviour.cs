using DS.Game.Core;
using UnityEngine;

namespace DS.Game.Player.Riko
{
    public class RikoLockStateBehaviour : MonoLinkedStateMachineBehviour<PlayerController>
    {
        protected override void OnLinkedStateExit(Animator animator, AnimatorStateInfo info, int layerIndex)
        {
            monoBehaviour.weapon.EndAttack();
        }
    }
}
