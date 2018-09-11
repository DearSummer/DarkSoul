using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
//-------------------------------------------
//  author: 
//  description:  
//-------------------------------------------

public static class ProjectConstant
{


    public class Tag
    {
        public static readonly string GROUND = "Ground";
        public static readonly string PLAYER = "Player";
        public static readonly string ENEMY = "Enemy";
        public static readonly string ACTIVE_ENEMY = "ActiveEnemy";
        public static readonly string SKILL_CANVAS = "SkillCanvas";
    }

    public class Layout
    {
        public static readonly string RENDER_OUTLINE = "RenderOutline";
        public static readonly string GROUND = "Ground";

    }

    public class AnimatorParameter
    {
        public static readonly string BASE_LAYER_ANIMTION = "animation";

        public static readonly string FORWARD = "forward";
        public static readonly string JUMP = "jump";
        public static readonly string ON_GROUND = "onGround";
        public static readonly string AIR = "air";
        public static readonly string ROLL = "roll";
        public static readonly string ATTACK = "attack";
        public static readonly string DIE = "die";
        public static readonly string FIRE = "fire";
        public static readonly string ICE = "ice";
    }

    public class AnimatorLayer
    {
        public static readonly string BASE = "Base";
        public static readonly string ATTACK = "Attack";
    }



}

