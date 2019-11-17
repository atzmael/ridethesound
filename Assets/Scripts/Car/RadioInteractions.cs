using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioInteractions : MonoBehaviour
{
    private Radio radio;
    private GameObject mouse;
    private MouseCursor mouseCursor;
    
    void Start()
    {
        radio = GetComponentInParent<Radio>();
        mouse = GameObject.Find("Cursor");
        mouseCursor = mouse.GetComponent<MouseCursor>();
    }

    // BEST WORKING FOR 2D COLLIDERS
    //void OnMouseDown()
    //{
    //    Debug.Log("CLICK");
    //    radio.OnButtonClick(this.gameObject);
    //}

    //void OnMouseEnter()
    //{
    //    mouseCursor.OnHover();
    //}

    //void OnMouseExit()
    //{
    //    mouseCursor.OnHoverExit();
    //}


    // BEST WORKING FOR 3D COLLIDERS
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.collider.name == "NextSong" || hit.collider.name == "PreviousSong")
    //            {
    //                radio.OnMusicChangeClick(hit.collider.gameObject);
    //            }

    //            Debug.Log("Name = " + hit.collider.name);
    //            //Debug.Log("Tag = " + hit.collider.tag);
    //            //Debug.Log("Hit Point = " + hit.point);
    //            //Debug.Log("Object position = " + hit.collider.gameObject.transform.position);
    //            //Debug.Log("--------------");
    //        }
    //    }
    //}
}
