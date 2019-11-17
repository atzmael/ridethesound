using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenHandler : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    private float alpha = 1.0f;
    private bool hiding = false;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hiding)
        {
            if (alpha > 0)
            {
                alpha -= 0.02f;
            } else if (alpha <= 0)
            {
                gameObject.SetActive(false);
            }
            canvasGroup.alpha = alpha;
        }

    }

    public void hide()
    {
        hiding = true;
        Debug.Log("Game Start click");
    }
}
