using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloorTextures : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer rend = null;
    float offset;
    float height;

    void Start()
    {
        rend = GetComponent<Renderer>();
        height = rend.material.GetFloat("_HeightMax");
    }

    void Update()
    {
        if(GameManager.inst.GameSpeed > 0)
        {
            offset -= GameManager.inst.GameSpeed * 0.005f;
            Vector2 noiseOffset = rend.material.GetTextureOffset("_Noise");
            noiseOffset.y -= Time.deltaTime * 0.3f;
            rend.material.mainTextureOffset = new Vector2(0, offset);
            rend.material.SetTextureOffset("_Noise", noiseOffset);
        }
        if (GameManager.inst.distanceRemaining < 200 && height > 0)
        {
            height -= 0.2f;
            if(height < 0.0f)
            {
                height = 0.0f;
            }
            rend.material.SetFloat("_HeightMax", height);
        }
    }
}
