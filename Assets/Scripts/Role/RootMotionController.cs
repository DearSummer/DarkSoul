using UnityEngine;


//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

namespace DS.Role
{
    public class RootMotionController : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            SendMessageUpwards("AnimatorMove", _animator.deltaPosition);
        }

    }
}

