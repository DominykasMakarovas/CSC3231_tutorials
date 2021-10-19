using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthDisplayEffect : MonoBehaviour
{
    [SerializeField]
    Material depthDisplayEffect;

    [SerializeField]
    Color colour;

    [SerializeField]
    bool lineariseDepth = true;


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        depthDisplayEffect.SetInt("useLinear", lineariseDepth ? 1 : 0);
        Graphics.Blit(source, destination, depthDisplayEffect);
    }
}
