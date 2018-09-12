using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSignal {
    public bool OnPressed
    {
        private set;
        get;
    }

    public bool IsPressing
    {
        private set;
        get;
    }

    public bool OnRelease
    {
        private set;
        get;
    }

    public bool IsExtending
    {
        private set;
        get;
    }

    public bool IsDelaying
    {
        private set;
        get;
    }

    private float _extendDuration = 0.15f;
    private float _delayDuration = 0.15f;

    private bool _curState = false;
    private bool _lastState = false;

    private readonly DSTimer _extendTimer = new DSTimer();
    private readonly DSTimer _delayTimer = new DSTimer();

    public void Tick(bool input)
    {

        _extendTimer.Tick();
        _delayTimer.Tick();

        _curState = input;

        IsPressing = input;

        OnRelease = false;
        OnPressed = false;
        if (_curState != _lastState)
        {
            if (_curState == true)
            {
                OnPressed = true;
                _delayTimer.StartTimer(_delayDuration);
               
            }
            else
            {
                OnRelease = true;
                _extendTimer.StartTimer(_extendDuration);
            }
        }

        IsExtending = _extendTimer.State == DSTimer.TimerState.RUN;
        IsDelaying = _delayTimer.State == DSTimer.TimerState.RUN;

        _lastState = _curState;
    }

    public void SetExtendDurationTime(float time)
    {
        _extendDuration = time;
    }

    public void SetDelayDurationTime(float time)
    {
        _delayDuration = time;
    }
}
