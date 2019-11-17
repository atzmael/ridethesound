using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/BasicObstacle")]
public class BasicObstacle : ScriptableObject
{
    public GameObject mesh;
    public int modifyScore = 1;
    public string tag = "obstacle";
    public AudioClip audioClip;
}
