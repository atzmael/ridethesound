using UnityEngine;
using System.Collections;

public class ElementBehavior : MonoBehaviour
{

    private GameObject newElement;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!newElement) return;
        this.transform.Translate(0, 0, -GameManager.inst.GameSpeed * Time.deltaTime);

        if (this.transform.position.z < -GameManager.inst.DistanceToDestroy)
        {
            Object.Destroy(this.gameObject);
        }
    }

    public void create(BasicElement element, bool isHospital)
    {
        newElement = Instantiate<GameObject>(element.mesh, transform);

        float xPos = element.side > 0 ? 9f : -8.5f;
        float zPos = GameManager.inst.DistanceToDestroy * 2;
        if(isHospital)
        {
            xPos = 12.5f;
        }

        this.transform.position = new Vector3(xPos, -0.5f, zPos);
    }
}