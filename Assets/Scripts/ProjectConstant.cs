


//-------------------------------------------
//  author: 
//  description:  
//-------------------------------------------

using UnityEngine;

namespace DS
{
    public static class ProjectConstant
    {

        public class Tag
        {
            public static readonly string WEAPON = "Weapon";
            public static readonly string DAMAGE_CANVAS = "DamageCanvas";
            public static readonly string PLAYER = "Player";
        }

        public class Layer
        {       
            public static readonly string GROUND = "Ground";
            public static readonly string ENEMY = "Enemy";
            public static readonly string HIT_SENOR = "HitSenor";
            public static readonly string WEAPON = "Weapon";
        }

        public class AnimatorParameter
        {
            public static readonly int FORWARD = Animator.StringToHash( "forward");
            public static readonly int JUMP = Animator.StringToHash("jump");
            public static readonly int ON_GROUND = Animator.StringToHash("onGround");
            public static readonly int AIR = Animator.StringToHash("air");
            public static readonly int ROLL = Animator.StringToHash("roll");
            public static readonly int ATTACK = Animator.StringToHash("attack");
            public static readonly int DIE = Animator.StringToHash("die");
            public static readonly int RIGHT = Animator.StringToHash("right");
            public static readonly int HIT = Animator.StringToHash("hit");
            public static readonly int DEFENSE = Animator.StringToHash("defense");
            public static readonly int BLOACKED = Animator.StringToHash("blocked");
            public static readonly int COUNTER_BACK = Animator.StringToHash("counterBack");
            public static readonly int STUNED = Animator.StringToHash("stuned");
            public static readonly int DRAW_WEAPON = Animator.StringToHash("draw");
            public static readonly int BACK_STAB = Animator.StringToHash("backStab");
            public static readonly int STAFF_STUNED = Animator.StringToHash("StaffStuned");
        }


        public enum PlayerState : byte
        {
            DEFENSE = 1,
            DEATH,
            HIT,
            ATTACK,
            GROUND,
            JUMP,
            BLOCKED,
            DRAW_WEAPON,
            BACK_STAB,
            STUNED
        }

        public class AnimatorLayer
        {
            public static readonly string BASE = "Base";

        }



    }
}

