using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedNeedle : MonoBehaviour
{

    private float _rotationZ = -90f;
    private GameObject _aiguille;
    private GameObject _volant;
    private GameObject _car;

    
    // Start is called before the first frame update
    void Start()
    {
        _aiguille = transform.GetChild(0).Find("SPEED").gameObject;
        _volant = transform.GetChild(0).Find("WHEEL_TURN").gameObject;
        _car = GameObject.Find("Car");
    }

    // Update is called once per frame
    void Update()
    {
        if (_aiguille != null)
        {
            float speed = GameManager.inst.GameSpeed * 1f / GameManager.inst.MaxSpeed * 1f;
            speed = (speed - .5f) * 2.0f;
            _rotationZ = speed * 90f + 90f;

            Quaternion rot = _aiguille.transform.rotation;
            Vector3 eAngles = rot.eulerAngles;
            eAngles.z = _rotationZ;
            rot.eulerAngles = eAngles;
            _aiguille.transform.rotation = rot;
        }

        if(_volant != null)
        {
            Quaternion target = Quaternion.Euler(0, _car.transform.position.x * 10 - 180, 0);
            Quaternion rotation = Quaternion.Slerp(_volant.transform.rotation, target, Time.deltaTime * 5);
            rotation = Quaternion.Euler(new Vector3(0f, rotation.eulerAngles.y, 0f));

            _volant.transform.rotation = rotation;
        }
    }
}
