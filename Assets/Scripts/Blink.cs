using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public GameObject topEye;
    public GameObject bottomEye;

    private Animation animTop;
    private Animation animBottom;
    private float randomBlink;
    public int localTime =  0;

    // Start is called before the first frame update
    void Start()
    {
        topEye = GameObject.Find("EyeTop");
        bottomEye = GameObject.Find("EyeBottom");

        animTop = topEye.GetComponent<Animation>();
        animBottom = bottomEye.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        // Blink chance increase by time
        randomBlink = Random.Range(0.0f, (float)GameManager.inst.timeSinceStart / (float)GameManager.inst.gameDuration);

        if (GameManager.inst.timeSinceStart > localTime)
        {
            if (randomBlink >= 0.5 && !animTop.isPlaying && !animBottom.isPlaying)
            {
                animTop.Play("BlinkTop");
                animBottom.Play("BlinkBottom");
            }

            localTime++;
        }
    }
}
