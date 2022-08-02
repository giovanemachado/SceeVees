using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject workerArrow;
    public GameObject[] resourcesArrow;
    public GameObject tutorialText;
    public GameObject canvasMan;

    bool dontShowTuto = false;

    bool playing = false;
    bool tutorialStarted = false;

    void Awake()
    {
        FlowManager.OnGameStateChange += TutorialManagerOnGameStateChanged;
    }

    private void TutorialManagerOnGameStateChanged(BaseGameState obj)
    {
        playing = obj == FlowManager.Instance.PlayState;
    }

    void OnDestroy()
    {
        FlowManager.OnGameStateChange -= TutorialManagerOnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playing) return;

        if (!tutorialStarted)
        {
            var con = canvasMan.GetComponent<CanvasManager>();
            dontShowTuto = con.tutorialOn == 2;

            if (!dontShowTuto)
            {
                StartCoroutine(StartAndFinishTutorial1());
            } else
            {
                workerArrow.SetActive(false);
                foreach (GameObject arr in resourcesArrow)
                {
                    arr.SetActive(false);
                }

                tutorialText.SetActive(false);
                tutorialStarted = true;
            }
        }
    }

    private IEnumerator StartAndFinishTutorial1()
    {
        tutorialStarted = true;
        yield return new WaitForSeconds(10);

        workerArrow.SetActive(false);

        foreach (GameObject arr in resourcesArrow)
        {
            arr.SetActive(false);
        }

        tutorialText.SetActive(false);
    }
}
