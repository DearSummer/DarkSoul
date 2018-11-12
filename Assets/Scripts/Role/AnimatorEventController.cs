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
        private CapsuleCollider _attackCollider;
        // Use this for initialization
        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _attackCollider = GetComponent<WeaponManager>().GetWeaponCollider();
        }

        private void ResetAttackTrigger()
        {
            animator.ResetTrigger(ProjectConstant.AnimatorParameter.ATTACK);
        }

        private void ResetAttackCollider()
        {
            _attackCollider.enabled = true;
        }

        private void CloseAttackCollider()
        {
            _attackCollider.enabled = false;
        }

    }
}

