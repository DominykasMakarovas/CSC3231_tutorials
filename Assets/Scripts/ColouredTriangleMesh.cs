using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColouredTriangleMesh : MonoBehaviour
{
    [SerializeField]
    bool liveUpdate;

    [SerializeField]
    Vector3 triA;

    [SerializeField]
    Vector3 triB;

    [SerializeField]
    Vector3 triC;

    [SerializeField]
    Color colourA;

    [SerializeField]
    Color colourB;

    [SerializeField]
    Color colourC;

    MeshFilter      filter;
    MeshRenderer    renderer;

    List<Vector3>   generatedPoints  = new List<Vector3>();
    List<Color>     GeneratedColours = new List<Color>();

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
        if (liveUpdate)
        {
            UpdateMesh();
        }
    }
    void UpdateMesh()
    {
        generatedPoints.Clear();
        GeneratedColours.Clear();
        RenderTriangle();
        FinaliseMesh();
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
        createdMesh.SetColors(GeneratedColours);

        int[] indices = new int[generatedPoints.Count];
        for (int i = 0; i < generatedPoints.Count; ++i)
        {
            indices[i] = i;
        }
        createdMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;        
        createdMesh.SetIndices(indices, MeshTopology.Points, 0);
        filter.mesh = createdMesh;
    }

    void    RenderTriangle()
    {
        
    }
}
