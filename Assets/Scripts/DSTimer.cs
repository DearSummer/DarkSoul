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

    private float duration = 1f;
    private float elapsedTime = 0f;

    public void Tick()
    {
        switch (State)
        {
            case TimerState.FINISH:
                break;
            case TimerState.IDEL:
                break;
            case TimerState.RUN:
                elapsedTime += Time.deltaTime;
                if (elapsedTime > duration)
                {
                    State = TimerState.FINISH;
                }
                break;
        }
    }

    public void StartTimer(float duration)
    {
        this.duration = duration;
        Run();      
    }

    public void Run()
    {
        elapsedTime = 0f;
        State = TimerState.RUN;
    }
}