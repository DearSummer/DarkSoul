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

    private bool _curState = false;
    private bool _lastState = false;

    public void Tick(bool input)
    {

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
            }
        }

        _lastState = _curState;
    }
}
