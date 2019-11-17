using UnityEngine;
using System.Collections;

public class ElementController : MonoBehaviour
{

    public BasicElement[] refElement;
    public GameObject elementBehavior;

    private bool hospitalGenerated = false;
    private float speedCreate = 0.1f;

    // Use this for initialization
    void Start()
    {
        GameManager.inst.OnGameStart += Inst_OnGameStart;
    }

    private void Inst_OnGameStart()
    {
        InvokeRepeating("CreateElements", 0.0f, speedCreate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateElements()
    {
        if(GameManager.inst.GameSpeed > 0 && GameManager.inst.distanceRemaining > 30)
        {
            // Get random number in all the object we have stored
            int presetNumber = Random.Range(1, refElement.Length);
            if (refElement[presetNumber] == null) return;
            // Create an instance of elementBehavior and store it into element
            GameObject element = Instantiate<GameObject>(elementBehavior, transform);
            // Run the create method of elementBehavior
            element.GetComponent<ElementBehavior>().create(refElement[presetNumber], false);
        }
        if (GameManager.inst.distanceRemaining < 20 && !hospitalGenerated)
        {
            hospitalGenerated = true;
            GameObject element = Instantiate<GameObject>(elementBehavior, transform);
            // Run the create method of elementBehavior
            element.GetComponent<ElementBehavior>().create(refElement[0], true);
        }
    }
}
