using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------


public class KeyBoardInput : PlayerInput
{


    [Header("-----key setting----")] public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    [Header("--------camera key setting----")]
    public string cKeyUp;

    public string cKeyDown;
    public string cKeyLeft;
    public string cKeyRight;

    [Header("-----run key------")] public string keyA;
    [Header("------jump key-----")] public string keyB;

    [Header("-----attack key setting----")]
    public string keyC;

    public string keyD;

    [Header("-------mouse enable------")]
    public bool mouseEnable = true;

    private float targetUpValue;
    private float targetRightValue;

    private float targetCUpvalue;
    private float targetCRightValue;

    private float velocityUpValue;
    private float velocityRightValue;

    private float velocityCUpValue;
    private float velocityCRightValue;



    // Update is called once per frame
    void Update()
    {
        InputHandler();

        if (!inputEnable)
        {
            targetRightValue = 0f;
            targetUpValue = 0f;
        }

        UpValue = Mathf.SmoothDamp(UpValue, targetUpValue, ref velocityUpValue, 0.1f);
        RightValue = Mathf.SmoothDamp(RightValue, targetRightValue, ref velocityRightValue, 0.1f);

        CUpValue = Mathf.SmoothDamp(CUpValue, targetCUpvalue, ref velocityCUpValue, 0.3f);
        CRightValue = Mathf.SmoothDamp(CRightValue, targetCRightValue, ref velocityCRightValue, 0.3f);


        Vector2 temp = SquareToCircle(new Vector2(UpValue, RightValue));

        SignalValueMagic = Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y);
        SignalVec = temp.x * this.transform.forward + temp.y * this.transform.right;
    }

    /// <summary>
    /// 控制输入的信息
    /// </summary>
    private void InputHandler()
    {
        targetUpValue = (Input.GetKey(keyUp) ? 1.0f : 0f) - (Input.GetKey(keyDown) ? 1.0f : 0f);
        targetRightValue = (Input.GetKey(keyRight) ? 1.0f : 0f) - (Input.GetKey(keyLeft) ? 1.0f : 0f);

        if (mouseEnable)
        {
            targetCUpvalue = Input.GetAxis("Mouse Y");
            targetCRightValue = Input.GetAxis("Mouse X");
        }
        else
        {
            targetCUpvalue = (Input.GetKey(cKeyUp) ? 1.0f : 0f) - (Input.GetKey(cKeyDown) ? 1.0f : 0f);
            targetCRightValue = (Input.GetKey(cKeyRight) ? 1.0f : 0f) - (Input.GetKey(cKeyLeft) ? 1.0f : 0f);
        }

        Run = Input.GetKey(keyA);
        Jump = Input.GetKey(keyB);
        Attack = Input.GetKeyDown(keyC);
        Magic = Input.GetKeyDown(keyD);
    }

}

