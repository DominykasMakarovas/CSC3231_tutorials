using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColouredLineMesh : MonoBehaviour
{
    [SerializeField]
    bool liveUpdate;

    [SerializeField]
    bool useBresenhams;

    [SerializeField]
    Vector3 startPoint;

    [SerializeField]
    Vector3 endPoint;

    [SerializeField]
    Color startColour;

    [SerializeField]
    Color endColour;

    MeshFilter      filter;
    MeshRenderer    renderer;

    List<Vector3> generatedPoints   = new List<Vector3>();
    List<Color>   generatedColours  = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {
        filter      = GetComponent<MeshFilter>();
        renderer    = GetComponent<MeshRenderer>();

        if (!filter)
        {
            filter = gameObject.AddComponent<MeshFilter>();
        }
        if (!renderer)
        {
            renderer = gameObject.AddComponent<MeshRenderer>();
        }
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if(liveUpdate)
        {
            UpdateMesh();
        }
    }

    void UpdateMesh()
    {
        generatedPoints.Clear();
        generatedColours.Clear();
        if (useBresenhams)
        {
            BresenhamLine();
        }
        else
        {
            SimpleLine();
        }
        FinaliseMesh();
    }


    void SimpleLine()
    {

    }

    void BresenhamLine()
    {        
        
    }
    Mesh createdMesh;
    void FinaliseMesh()
    {
        if (!createdMesh)
        {
            createdMesh = new Mesh();
        }
        createdMesh.name = gameObject.name;
        createdMesh.SetVertices(generatedPoints);
        createdMesh.SetColors(generatedColours); //This is the only new bit versus the LineMesh class!

        int[] indices = new int[generatedPoints.Count];
        for(int i = 0; i < generatedPoints.Count; ++i)
        {
            indices[i] = i;
        }

        createdMesh.SetIndices(indices, MeshTopology.Points, 0);
        filter.mesh = createdMesh;
    }
}