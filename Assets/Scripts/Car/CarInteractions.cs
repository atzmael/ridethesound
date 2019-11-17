using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInteractions : MonoBehaviour
{
    public float translateSpeed = 7f;
    public float jumpForce = 2f;
    
    private Rigidbody _car;
    private Vector3 _jumpVector;
    private bool _isGrounded = true;
    private AudioSource soundCamper;
    private float _carSpeed;
    private float decelerate = 0.0f;

    private bool isColliding = false;

    void Start()
    {
        _car = GetComponent<Rigidbody>();
        _jumpVector = new Vector3(0.0f, jumpForce, 0.0f);
        soundCamper = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(!isColliding)
        {
            isColliding = true;
        }
        if (other.collider.tag == "segment")
        {
            _isGrounded = true;
        } else
        {
            if (other.collider.tag.Contains("obs")) {
                GameManager.inst.timeRemaining -= other.gameObject.GetComponent<ObstacleBehavior>().ScoreModifier;
                soundCamper.Play();
            }
        }
    }


    void FixedUpdate()
    {
        
        if (GameManager.inst.startGame)
        {
            // Left
            if (Input.GetKey("left"))
            {
                if (!GameManager.inst.interacted) GameManager.inst.interacted = true;
                if (GameManager.inst.GameSpeed > 0)
                {
                    _car.transform.Translate(Vector3.left * translateSpeed * Time.deltaTime);
                }
            }

            // Right
            if (Input.GetKey("right"))
            {
                if (!GameManager.inst.interacted) GameManager.inst.interacted = true;
                if (GameManager.inst.GameSpeed > 0)
                {
                    _car.transform.Translate(Vector3.right * translateSpeed * Time.deltaTime);
                }
            }

            // Jump
            if (Input.GetButton("Jump") && _isGrounded)
            {
                if (GameManager.inst.GameSpeed > 0)
                {
                    _car.AddForce(_jumpVector, ForceMode.Impulse);
                    _isGrounded = false;
                }
            }

            // Acceleration
            if (Input.GetKey("up"))
            {
                if (!GameManager.inst.interacted) GameManager.inst.interacted = true;
                GameManager.inst.GameSpeed += 2;
                decelerate = 0.0f;
            }
            else
            {
                GameManager.inst.GameSpeed -= GameManager.inst.GameSpeed > 0 ? Mathf.Pow(GameManager.inst.GameSpeed, GameManager.inst.GameSpeed * decelerate) : 0;
            }

            if (Input.GetKey("down"))
            {
                if (GameManager.inst.GameSpeed > 0)
                {
                    decelerate += 0.0002f;
                }
                else if (GameManager.inst.GameSpeed <= 0)
                {
                    GameManager.inst.GameSpeed -= 4;
                }
            }
        }
    }
}
