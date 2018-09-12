using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBOXJoystickInput : PlayerInput
{

    [Header("-------key----------")]
    public string keyX = "joystick button 2";
    public string keyY = "joystick button 3";
    public string keyB = "joystick button 1";
    public string keyA = "joystick button 0";

    [Header("----------ex key---------")]
    public string keyR1 = "joystick button 5";
    public string keyR2 = "RT Axis";
    public string keyR3 = "joystick button 8";
    public string keyL1 = "joystick button 4";
    public string keyL2 = "LT Axis";
    public string keyL3 = "joystick button 9";

    private readonly ButtonSignal _btnX = new ButtonSignal();
    private readonly ButtonSignal _btnY = new ButtonSignal();
    private readonly ButtonSignal _btnA = new ButtonSignal();
    private readonly ButtonSignal _btnB = new ButtonSignal();
    private readonly ButtonSignal _btnR1 = new ButtonSignal();
    private readonly ButtonSignal _btnR3 = new ButtonSignal();
    private readonly ButtonSignal _btnL1 = new ButtonSignal();
    private readonly ButtonSignal _btnL3 = new ButtonSignal();


    private float _targetUpValue;
    private float _upValueVelocity;

    private float _targetRightValue;
    private float _rightValueVelocity;

    private float _targetCUpValue;
    private float _cUpValueVelocity;

    private float _targetCRightValue;
    private float _cRightValueVelocity;

    void Update()
    {

        InputHandler();

        if (!inputEnable)
        {
            _targetRightValue = 0f;
            _targetUpValue = 0f;
        }

        HandleActionSignal();
        CalculationAxisSignal();
        
    }

    private void CalculationAxisSignal()
    {
        UpValue = Mathf.SmoothDamp(UpValue, _targetUpValue, ref _upValueVelocity, 0.1f);
        RightValue = Mathf.SmoothDamp(RightValue, _targetRightValue, ref _rightValueVelocity, 0.1f);

        CUpValue = Mathf.SmoothDamp(CUpValue, _targetCUpValue, ref _cUpValueVelocity, 0.3f);
        CRightValue = Mathf.SmoothDamp(CRightValue, _targetCRightValue, ref _cRightValueVelocity, 0.3f);

        Vector2 temp = SquareToCircle(new Vector2(UpValue, RightValue));

        SignalValueMagic = Mathf.Sqrt(temp.x * temp.x + temp.y * temp.y);
        SignalVec = temp.x * this.transform.forward + temp.y * this.transform.right;

    }

    private void HandleActionSignal()
    {
        Roll = _btnA.IsDelaying && _btnA.OnRelease;
        Jump = _btnA.OnPressed && _btnA.IsExtending;
        Attack = _btnX.OnPressed;
        Run = (_btnA.IsPressing && !_btnA.IsDelaying) || _btnA.IsExtending;

    }

    private void InputHandler()
    {
        _targetUpValue = Input.GetAxis("Left Y Axis");
        _targetRightValue = Input.GetAxis("Left X Axis");

        _targetCRightValue = Input.GetAxis("Right X Axis");
        _targetCUpValue = Input.GetAxis("Right Y Axis");        

        _btnA.Tick(Input.GetKey(keyA));
        _btnB.Tick(Input.GetKey(keyB));
        _btnX.Tick(Input.GetKey(keyX));
        _btnY.Tick(Input.GetKey(keyY));

        _btnL1.Tick(Input.GetKey(keyL1));
        _btnL3.Tick(Input.GetKey(keyL3));
        _btnR1.Tick(Input.GetKey(keyR1));
        _btnR3.Tick(Input.GetKey(keyR3));

        
    }



}
