using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugProjectionMatrix : MonoBehaviour
{
    [SerializeField]
    Matrix4x4 projMatrix;

    [SerializeField]
    bool writeValues;

    Camera attachedCam;
    // Start is called before the first frame update
    void Start()
    {
        attachedCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(writeValues)
        {
            attachedCam.projectionMatrix = projMatrix;
        }
        else
        {
            attachedCam.ResetProjectionMatrix();
            projMatrix = attachedCam.projectionMatrix;
        }
    }
}
