using UnityEngine;


//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

namespace DS.Role
{
    public class AnimatorEventController : MonoBehaviour
    {
        private Animator animator;

        // Use this for initialization
        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void ResetAttackTrigger()
        {
            animator.ResetTrigger(ProjectConstant.AnimatorParameter.ATTACK);
        }

    }
}

