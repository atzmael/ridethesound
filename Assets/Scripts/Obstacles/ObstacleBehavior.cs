using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{

    private GameObject newObstacle;
    private int scoreModifier = 0;

    public int ScoreModifier { get => scoreModifier; set => scoreModifier = value; }

    // Start is called before the first frame update

    void Start()
    {

    }

    void Update()
    {
        if (!newObstacle) return;
        if(this.tag == "obs_truck" || this.tag == "obs_convoyer")
        {
            this.transform.Translate(0, 0, -GameManager.inst.GameSpeed * Time.deltaTime - 1f);
        } else
        {
            this.transform.Translate(0, 0, -GameManager.inst.GameSpeed * Time.deltaTime);
        }

        if (this.transform.position.z < -GameManager.inst.DistanceToDestroy * 4)
        {
            Object.Destroy(this.gameObject);
        }
    }

    public void create(BasicObstacle obstacle)
    {
        newObstacle = Instantiate<GameObject>(obstacle.mesh, transform);

        Collider newCollider = newObstacle.GetComponent<Collider>();
        System.Type newColliderType = newCollider.GetType();

        if (newColliderType == typeof(SphereCollider))
        {
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        } else if(newColliderType == typeof(BoxCollider))
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        }

        float xPos = Random.Range(-4.0f, 4.0f);

        ScoreModifier = obstacle.modifyScore;

        switch (obstacle.tag)
        {
            case "obs_landslide":
                xPos = 3.5f;
                break;
            case "obs_rails":
                xPos = 0;
                break;
            case "obs_truck":
                xPos = -3.5f;
                break;
            case "obs_convoyer":
                xPos = -3.5f;
                break;
        }
        this.transform.position = new Vector3(xPos, 0.0f, GameManager.inst.DistanceToDestroy * 4);
        if(obstacle.tag != null || obstacle.tag != string.Empty)
        {
            this.tag = obstacle.tag;
        } else
        {
            this.tag = "Untagged";
        }

        AudioSource audio = this.GetComponent<AudioSource>();
        if(obstacle.audioClip != null)
        {
            audio.clip = obstacle.audioClip;
            audio.Play();
        }
    }
}
