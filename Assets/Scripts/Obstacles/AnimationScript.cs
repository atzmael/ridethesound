using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{

    private bool animFinish = false;
    private Transform barriere;
    private float barRotation = 0.7f;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        barriere = transform.Find("barriere");
        StartCoroutine(canOpen());
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen)
        {
            if (barriere.transform.rotation.z < -0.695f) return;
            barriere.Rotate(0, 0, -barRotation);
        } else
        {
            if (barriere.transform.rotation.z > 0) return;
            barriere.Rotate(0, 0, barRotation);
        }
    }

    IEnumerator canOpen()
    {
        yield return new WaitForSeconds(3);
        isOpen = true;
    }
}
