using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugViewMatrix : MonoBehaviour
{
    [SerializeField]
    Matrix4x4 viewMat = Matrix4x4.identity;

    [SerializeField]
    bool writeValues;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (writeValues)
        {
            cam.worldToCameraMatrix = viewMat;
        }
        else
        {
            cam.ResetWorldToCameraMatrix();
            viewMat = cam.worldToCameraMatrix;
        }
    }
}
