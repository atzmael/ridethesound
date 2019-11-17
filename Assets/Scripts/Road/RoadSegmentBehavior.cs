using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSegmentBehavior : MonoBehaviour
{

    private GameObject newSegment;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (!newSegment) return;
        this.transform.Translate(0, 0, -GameManager.inst.GameSpeed * Time.deltaTime);

        if (this.transform.position.z < -GameManager.inst.DistanceToDestroy)
        {
            Object.Destroy(this.gameObject);
        }
    }

    public void create(BasicRoadSegment segment)
    {
        newSegment = Instantiate<GameObject>(segment.mesh, transform);

        float xPos = Random.Range(-4.0f, 4.0f);

        this.transform.position = new Vector3(xPos, 0.5f, GameManager.inst.RoadLength);

        AudioSource audio = newSegment.GetComponent<AudioSource>();
        if (segment.audioClip != null)
        {
            audio.clip = segment.audioClip;
            audio.Play();
        }
    }
}
