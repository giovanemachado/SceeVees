using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject MenuCanvas;
    public GameObject IngameCanvas;
    public GameObject EndgameCanvas;
    public TMPro.TextMeshProUGUI endText;
    public TMPro.TextMeshProUGUI buttonTextTutorial;
    public TMPro.TextMeshProUGUI buttonTextMusic;

    public GameObject music;
    AudioSource musicS;

    public int musicOn = 0;
    public int tutorialOn = 0;

    // collectionsDelivered

    bool playing = false;

    void Awake()
    {
        FlowManager.OnGameStateChange += FlowManagerOnGameStateChanged;
    }

    void OnDestroy()
    {
        FlowManager.OnGameStateChange -= FlowManagerOnGameStateChanged;
    }

    private void Start()
    {
        musicS = music.GetComponent<AudioSource>();
        int m = PlayerPrefs.GetInt("music");
        int t = PlayerPrefs.GetInt("tutorial");
      
        musicOn = m != 0 ? m : 1;
        tutorialOn = t != 0 ? t : 1; 
       
        if (tutorialOn == 2)
        {
            buttonTextTutorial.text = "turn on tutorial";
        }
        else
        {
            buttonTextTutorial.text = "turn off tutorial";
        }

        if (musicOn == 2)
        {
            buttonTextMusic.text = "turn on music";
        }
        else
        {
            buttonTextMusic.text = "turn off music";
        }
    }

    private void Update()
    {
        if (!playing) return; 

        endText.text = "You did " + CollectionService.collectionsDelivered.ToString() + " points";
    }

    void FlowManagerOnGameStateChanged(BaseGameState state)
    {
        MenuCanvas.SetActive(state == FlowManager.Instance.MenuState);
        IngameCanvas.SetActive(state == FlowManager.Instance.PlayState);
        EndgameCanvas.SetActive(state == FlowManager.Instance.EndgameState);

        playing = state == FlowManager.Instance.PlayState;
    }

    
    public void PlayButtonPressed()
    {
        FlowManager.Instance.SwitchState(FlowManager.Instance.PlayState);
    }

    public void TurnOffOnTutorial()
    {
        if (tutorialOn == 2)
        {
            tutorialOn = 1;
            buttonTextTutorial.text = "turn off tutorial";
            PlayerPrefs.SetInt("tutorial", 1);
        } else
        {
            tutorialOn = 2;
            buttonTextTutorial.text = "turn on tutorial";
            PlayerPrefs.SetInt("tutorial", 2);
        }
        
    }
    
    public void TurnOffOnMusic()
    {
        if (musicOn == 2)
        {
            musicOn = 1;
            buttonTextMusic.text = "turn off music";
            PlayerPrefs.SetInt("music", 1);
            musicS.volume = 0.2f;
        }
        else
        {
            musicOn = 2;
            buttonTextMusic.text = "turn on music";
            PlayerPrefs.SetInt("music", 2);
            musicS.volume = 0;
        }
    }
}
