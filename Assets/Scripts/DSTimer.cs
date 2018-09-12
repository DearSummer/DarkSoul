using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSTimer
{

    public enum TimerState
    {
        IDEL,
        RUN,
        FINISH
    }

    public TimerState State
    {
        get;
        set;
    }

    private float _duration = 1f;
    private float _elapsedTime = 0f;

    public void Tick()
    {
        switch (State)
        {
            case TimerState.FINISH:
                State = TimerState.IDEL;
                break;
            case TimerState.IDEL:
                break;
            case TimerState.RUN:
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime > _duration)
                {
                    State = TimerState.FINISH;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void StartTimer(float duration)
    {
        this._duration = duration;
        Run();      
    }

    public void Run()
    {
        _elapsedTime = 0f;
        State = TimerState.RUN;
    }
}