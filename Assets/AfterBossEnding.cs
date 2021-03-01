using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterBossEnding : MonoBehaviour
{
    public AudioSource goodEnding;
    public AudioSource badEnding;

    private bool bad;
    private bool end;
    private float endingTime;
    private float waitBefore;

    private void Start()
    {
        end = false;
    }

    private void Update()
    {
        if (end)
        {
            endingTime -= Time.deltaTime;
            if (endingTime <= 0)
            {
                if (bad)
                {
                    SceneManager.LoadScene(3);
                }
                else
                {
                    SceneManager.LoadScene(4);
                }
            }
        }
    }

    public void Ending(int index)
    {
        end = true;
        if (index == 1)
        {
            badEnding.Play();
            bad = true;
            endingTime = 10f;
        }
        else if (index == 2)
        {
            goodEnding.Play();
            bad = false;
            endingTime = 10f;
        }
    }
}
