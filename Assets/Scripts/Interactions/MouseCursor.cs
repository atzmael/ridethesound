using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{

    public Sprite handCursor;
    public Sprite normalCursor;
    public float rayLength = 100;
    public LayerMask interactionsMask;

    private Camera _mainCamera;
    private SpriteRenderer _rend;
    private Radio _radioScript;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start cursor");
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _rend = GetComponent<SpriteRenderer>();
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _radioScript = GameObject.Find("Body").GetComponent<Radio>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update sprite position to cursor position
        var mousePos = Input.mousePosition;
        mousePos.z = 2;

        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(mousePos);
        cursorPos.y -= 0.02f;
        transform.position = cursorPos;
        Debug.Log(cursorPos);

        // Raycast
        RaycastHit hit;
        Ray mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * rayLength);

        if (Physics.Raycast(mouseRay, out hit, rayLength, interactionsMask.value))
        {

            OnHover(hit.collider.gameObject);
        }
        else
        {
            OnHoverExit();
        }
    }

    public void OnHover(GameObject button)
    {
        _rend.sprite = handCursor;

        if (Input.GetMouseButtonDown(0))
        {
            _radioScript.OnButtonClick(button);
        }
    }

    public void OnHoverExit()
    {
        _rend.sprite = normalCursor;
    }
}