using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMesh : MonoBehaviour
{
    [SerializeField]
    bool liveUpdate;

    [SerializeField]
    bool useBresenhams;

    [SerializeField]
    Vector3 startPoint;

    [SerializeField]
    Vector3 endPoint;

    MeshFilter      filter;
    MeshRenderer    renderer;

    List<Vector3> generatedPoints = new List<Vector3>();

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
        float lineLength = ( endPoint - startPoint ). magnitude;
        float reciprocal = 1.0f / ( lineLength - 1);
        
        float t = 0;
        for (int i = 0; i < (int) lineLength; ++i)
        {
            Vector3 newPoint = Vector3.Lerp (startPoint , endPoint , t);
            generatedPoints.Add(newPoint);
            t += reciprocal;
        }

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

        int[] indices = new int[generatedPoints.Count];
        for(int i = 0; i < generatedPoints.Count; ++i)
        {
            indices[i] = i;
        }

        createdMesh.SetIndices(indices, MeshTopology.Points, 0);
        filter.mesh = createdMesh;
    }
}