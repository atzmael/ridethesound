using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour
{
    public ProbabilityObtacle[] probaObstacle;
    public GameObject obstacleBehavior;

    private float speedCreate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.inst.OnGameStart += Inst_OnGameStart;
    }

    private void Inst_OnGameStart()
    {
        InvokeRepeating("CreateObstacles", 0.1f, speedCreate);
    }

    private void Update()
    {
    }

    void CreateObstacles()
    {
        if (GameManager.inst.GameSpeed > 0)
        {
            int presetNumber = Random.Range(0, probaObstacle.Length);
            if (Random.value > probaObstacle[presetNumber].proba) return;
            GameObject obstacle = Instantiate<GameObject>(obstacleBehavior, transform);
            obstacle.GetComponent<ObstacleBehavior>().create(probaObstacle[presetNumber].obstacle);
        }
    }
}
