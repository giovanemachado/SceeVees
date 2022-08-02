using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    bool playing = false;
    public float timeLeft = 10f;

    public TMPro.TextMeshProUGUI timingUi;

    void Awake()
    {
        FlowManager.OnGameStateChange += TimerManagerOnGameStateChanged;
    }


    void OnDestroy()
    {
        FlowManager.OnGameStateChange -= TimerManagerOnGameStateChanged;
    }

    void Update()
    {

        if (!playing) return;


        timeLeft -= Time.deltaTime;
        var timeLeftInt = (int)timeLeft;
        timingUi.text = timeLeftInt.ToString() + " seconds left";

        if (timeLeft < 0)
        {
            FlowManager.Instance.SwitchState(FlowManager.Instance.EndgameState);
        }
    }

    private void TimerManagerOnGameStateChanged(BaseGameState state)
    {
        playing = state == FlowManager.Instance.PlayState;
    }
}
