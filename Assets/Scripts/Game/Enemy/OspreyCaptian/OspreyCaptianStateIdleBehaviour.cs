using DS.Game.Core;
using UnityEngine;
using UnityEngine.Animations;

namespace DS.Game.Enemy.OspreyCaptian
{
    public class OspreyCaptianStateIdleBehaviour : MonoLinkedStateMachineBehviour<OspreyCaptianBehaviour>
    {


        protected override void OnLinkedStateEnter(Animator animator, AnimatorStateInfo info, int layerIndex)
        {
            monoBehaviour.Controller.SetFollowNavMesh(true);

            animator.SetBool(OspreyCaptianBehaviour.hashPursuit, false);
            animator.ResetTrigger(OspreyCaptianBehaviour.hashAttack);
        }

        protected override void OnStateUpdateWithoutTrasnlation(Animator animator, AnimatorStateInfo info,
            int layerIndex)
        {
            monoBehaviour.ScanTarget();

            if (monoBehaviour.Target != null)
            {
                Vector3 toTarget = monoBehaviour.Target.transform.position - monoBehaviour.transform.position;

                if (toTarget.sqrMagnitude < monoBehaviour.meleeRange * monoBehaviour.meleeRange)
                {
                    if (monoBehaviour.OrientTowardTarget())
                    {
                        animator.SetTrigger(OspreyCaptianBehaviour.hashAttack);
                        animator.SetFloat(OspreyCaptianBehaviour.hashAttackValue, Random.value);
                    }


                }
                else
                {
                    monoBehaviour.StartPursuit();
                }
                

            }
        }



    }
}
