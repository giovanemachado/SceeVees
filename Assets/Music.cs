using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject canvasMan;
    AudioSource music;

    bool dMusic = false;

    private void Update()
    {
        if (dMusic) return;

        var con = canvasMan.GetComponent<CanvasManager>();
        music = GetComponent<AudioSource>();
        
        if (con.musicOn == 2)
        {
            music.volume = 0;
        } else
        {
            music.volume = 0.2f;
        }
        dMusic = true;
    }
}
