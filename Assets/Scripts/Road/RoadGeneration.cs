using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{
    public GameObject segmentPrefab;
    public float totalRoadSegments;

	private float _roadLength;

	void Start ()
    {
        _roadLength = GameManager.inst.RoadLength;

        // Create X road segments
        for (int i = 1; i < totalRoadSegments; i++)
        {
            segmentPrefab = Instantiate(segmentPrefab, new Vector3(segmentPrefab.transform.position.x, segmentPrefab.transform.position.y, i * _roadLength), Quaternion.identity);
            segmentPrefab.transform.parent = transform;
            segmentPrefab.name = "Segment_" + i;
        }
    }
    
    void Update ()
    {
        // Update road position
        foreach (Transform road in transform)
        {
            Vector3 newRoadPos = road.position;
            newRoadPos.z -= GameManager.inst.GameSpeed * Time.deltaTime;

            if (newRoadPos.z < - GameManager.inst.DistanceToDestroy * 0.75)
            {
                newRoadPos.z += _roadLength * transform.childCount;
            }

            road.position = newRoadPos;
        }
    }
}