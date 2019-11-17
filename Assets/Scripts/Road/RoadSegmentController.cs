using UnityEngine;
using System.Collections;

public class SegmentController : MonoBehaviour
{

    public BasicRoadSegment[] refSegment;
    public GameObject segmentBehavior;

    public GameObject segmentPrefab;
    public float totalRoadSegments;

    private float _roadLength = 3f;

    [SerializeField]
    private float speedCreate = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.inst.OnGameStart += Inst_OnGameStart;
    }

    private void Inst_OnGameStart()
    {
        _roadLength = GameManager.inst.RoadLength;

        // Create X road segments
        for (int i = 1; i < totalRoadSegments; i++)
        {
            segmentPrefab = Instantiate(segmentPrefab, new Vector3(segmentPrefab.transform.position.x, segmentPrefab.transform.position.y, i * _roadLength), Quaternion.identity);
            segmentPrefab.transform.parent = transform;
            segmentPrefab.name = "Segment_" + i;
        }

        InvokeRepeating("CreateSegments", 1.0f, speedCreate);
    }

    private void Update()
    {
        int presetNumber = Random.Range(0, refSegment.Length);
        GameObject segment = Instantiate<GameObject>(segmentBehavior, transform);
        segment.GetComponent<RoadSegmentBehavior>().create(refSegment[presetNumber]);
    }

    void CreateSegments()
    {
        
    }
}
