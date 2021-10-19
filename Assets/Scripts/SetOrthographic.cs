using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOrthographic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.orthographicSize = Screen.height;
    }
}
