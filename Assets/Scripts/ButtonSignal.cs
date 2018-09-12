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

    private bool _curState = false;
    private bool _lastState = false;

    private readonly DSTimer _timer = new DSTimer();

    public void Tick(bool input)
    {

        _timer.Tick();

        _curState = input;

        IsPressing = input;

        OnRelease = false;
        OnPressed = false;
        if (_curState != _lastState)
        {
            if (_curState == true)
            {
                OnPressed = true;
            }
            else
            {
                OnRelease = false;
                _timer.StartTimer(0.5f);
            }
        }

        IsExtending = _timer.State == DSTimer.TimerState.FINISH;

        _lastState = _curState;
    }
}
