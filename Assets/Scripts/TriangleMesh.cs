using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMesh : MonoBehaviour
{
    [SerializeField]
    bool liveUpdate;

    [SerializeField]
    Vector3 triA;

    [SerializeField]
    Vector3 triB;

    [SerializeField]
    Vector3 triC;

    MeshFilter      filter;
    MeshRenderer    renderer;

    List<Vector3>   generatedPoints = new List<Vector3>();

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

        int[] indices = new int[generatedPoints.Count];
        for (int i = 0; i < generatedPoints.Count; ++i)
        {
            indices[i] = i;
        }
        createdMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;        
        createdMesh.SetIndices(indices, MeshTopology.Points, 0);
        filter.mesh = createdMesh;
    }

    void RenderTriangle()
    {
        Bounds box = new Bounds(triA, Vector3.zero);
        box.Encapsulate(triB);
        box.Encapsulate(triC);

        Vector3 triA2D = new Vector3(triA.x, triA.y, 1.0f);
        Vector3 triB2D = new Vector3 (triB.x , triB.y, 1.0f);
        Vector3 triC2D = new Vector3(triC.x, triC.y, 1.0f);

        Vector3 lineAB = Vector3.Cross(triB2D, triA2D);
        Vector3 lineBC = Vector3.Cross(triC2D, triB2D);
        Vector3 lineCA = Vector3.Cross(triA2D, triC2D);

        for (float y = box.min.y; y < box.max.y; ++y)
        {
            for (float x = box.min.x; x < box.max.x; ++x)
            {
                Vector3 screenPos = new Vector3 (x, y,1.0f);
                
                float testAB = Vector3.Dot(lineAB, screenPos);
                float testBC = Vector3.Dot(lineBC, screenPos);
                float testCA = Vector3.Dot(lineCA, screenPos);
                
                if( testAB > 0.0f && testBC > 0.0f && testCA > 0.0f) 
                {
                    generatedPoints.Add(screenPos);
                }
            }
        }
    }
}
