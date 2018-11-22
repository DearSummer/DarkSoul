using DS.Effect;
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
        private WeaponManager _weaponManger;

        // Use this for initialization
        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _weaponManger = GetComponent<WeaponManager>();
            _attackCollider = _weaponManger.GetWeaponCollider();
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

        private void ShowSword()
        {
            _weaponManger.DisplayWeapon();
        }

        private void BackStab()
        {
            ResetAttackCollider();
            
            _weaponManger.IsCrit = true;
        }

    }
}

