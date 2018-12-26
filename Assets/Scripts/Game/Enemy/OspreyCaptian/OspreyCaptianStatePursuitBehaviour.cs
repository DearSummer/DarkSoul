using DS.Game.Core;
using UnityEngine;
using UnityEngine.AI;

namespace DS.Game.Enemy.OspreyCaptian
{
    public class OspreyCaptianStatePursuitBehaviour :
        MonoLinkedStateMachineBehviour<OspreyCaptianBehaviour>
    {
        protected override void OnLinkedStateEnter(Animator animator, AnimatorStateInfo info, int layerIndex)
        {
            if (monoBehaviour.Target != null)
            {
                monoBehaviour.OrientTowardTarget();
            }
        }


        protected override void OnStateUpdateWithoutTrasnlation(Animator animator, AnimatorStateInfo info,
            int layerIndex)
        {
            monoBehaviour.ScanTarget();
            animator.SetFloat(OspreyCaptianBehaviour.hashSpeed,
                Mathf.Lerp(animator.GetFloat(OspreyCaptianBehaviour.hashSpeed), monoBehaviour.Controller.Agent.speed,
                    0.05f));

            if (monoBehaviour.Controller.Agent.pathStatus == NavMeshPathStatus.PathInvalid ||
                monoBehaviour.Controller.Agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                monoBehaviour.StopPursuit();
                return;
            }

            if (monoBehaviour.Target != null)
            {
                Vector3 targetPos = monoBehaviour.Target.transform.position;
                Vector3 toTarget = targetPos - monoBehaviour.transform.position;

                monoBehaviour.Controller.SetTarget(targetPos);

                if (toTarget.sqrMagnitude < monoBehaviour.meleeRange * monoBehaviour.meleeRange)
                {
                    monoBehaviour.OrientTowardTarget();
                    animator.SetTrigger(OspreyCaptianBehaviour.hashAttack);
                    animator.SetFloat(OspreyCaptianBehaviour.hashAttackValue, Random.value);
                    animator.SetBool(OspreyCaptianBehaviour.hashPursuit, false);
                }

            }
            else
            {
                monoBehaviour.StopPursuit();
            }
        }
    }
}
