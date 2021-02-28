using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingEnding : MonoBehaviour
{
    public List<AudioSource> audios;

    private int index;
    private float playTime;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        audios[index].Play();
        playTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        playTime -= Time.deltaTime;
        if (playTime <= 0)
        {
            if (index == 0)
                playTime = 4f;
            else if (index == 1)
            {
                playTime = 7f;
            }
            else if (index == 2)
            {
                playTime = 4f;
            }
            else if (index == 3)
            {
                playTime = 15f;
            }
            else if (index == 4)
            {
                playTime = 7f;
            }
            else if (index == 5)
            {
                playTime = 6f;
            }
            else if (index == 6)
            {
                playTime = 30f;
            }
            else if (index == 7)
            {
                playTime = 30f;
            }
            else if (index == 8)
            {
                playTime = 30f;
            }

            index++;
            audios[index].Play();
        }
    }
}
