using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/BasicElement")]
public class BasicElement : ScriptableObject
{
    public GameObject mesh;
    public int side;
}
