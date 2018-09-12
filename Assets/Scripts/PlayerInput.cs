using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public bool Roll
    {
        protected set;
        get;
    }

    public bool Run
    {
        protected set;
        get;
    }

    public bool Jump
    {
        protected set;
        get;
    }

    public bool Attack
    {
        protected set;
        get;
    }

    public bool Magic
    {
        protected set;
        get;
    }

    //摄像机的上方向信号量
    public float CUpValue
    {
        protected set;
        get;
    }

    //摄像机的右方向信号量
    public float CRightValue
    {
        protected set;
        get;
    }

    public float UpValue
    {
        protected set;
        get;
    }

    public float RightValue
    {
        protected set;
        get;
    }

    public float SignalValueMagic
    {
        protected set;
        get;
    }

    public Vector3 SignalVec
    {
        set;
        get;
    }

    [SerializeField]
    [Header("-------enable-------")]
    protected bool inputEnable = true;

    public bool InputEnable
    {
        get
        {
            return inputEnable;
        }
        set
        {
            inputEnable = value;
        }
    }

    /// <summary>
    /// 将直角坐标系下的坐标映射为相应的圆的坐标
    /// </summary>
    /// <param name="input">直角坐标系下的坐标</param>
    /// <returns>相应的圆的坐标系的坐标</returns>
    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 result = Vector2.zero;
        result.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        result.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return result;
    }
}
