using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayUI : MonoBehaviour
{

    private Text textEditor;
    // Start is called before the first frame update
    void Start()
    {
        textEditor = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.inst.displayUI)
        {
            textEditor.text = "Temps restants : " + GameManager.inst.timeRemaining + "\nDistance restante : " + (int)GameManager.inst.distanceRemaining;
        }
    }
}
