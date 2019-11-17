using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BasicRoadSegment")]
public class BasicRoadSegment : ScriptableObject
{
    public GameObject mesh;
    public AudioClip audioClip;
}
