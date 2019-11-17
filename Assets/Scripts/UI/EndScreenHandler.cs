using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenHandler : MonoBehaviour
{

    private bool shown = false;
    private bool show = false;
    private float alpha = 0;
    private CanvasGroup canvasGroup;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Transform child = transform.Find("Panel");
        Transform grandChild = child.Find("StatusEndGame");
        text = grandChild.gameObject.GetComponent<Text>();
        text.text = "You've " + GameManager.inst.endGameStatus;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.inst.ended && !shown)
        {
            text.text = "You've " + GameManager.inst.endGameStatus;
            shown = true;
            StartCoroutine(displayEndScreen());
        }
        if(show)
        {
            if (alpha < 1)
            {
                alpha += 0.005f;
            } else if(alpha >= 1)
            {
                canvasGroup.blocksRaycasts = true;
            }
            canvasGroup.alpha = alpha;
        }
    }

    IEnumerator displayEndScreen()
    {
        yield return new WaitForSeconds(3);
        show = true;
    }
}
