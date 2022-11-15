using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundButtons : MonoBehaviour
{

    private bool playing;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void playMusic()
    {
        AudioListener.pause = false;
    }

    public void pauseMusic()
    {
        AudioListener.pause = true;
    }

}
