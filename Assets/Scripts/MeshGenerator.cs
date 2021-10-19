using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    MeshFilter filter;
    MeshRenderer renderer;

    [SerializeField]
    bool liveUpdate;

    [SerializeField]
    MeshTopology topology = MeshTopology.Triangles;

    [SerializeField]
    List<Vector3> vertexPositions;

    [SerializeField]
    List<Color> vertexColours;

    [SerializeField]
    List<Vector2> vertexUVs;

    [SerializeField]
    int[] indices = new int[3];

    Mesh createdMesh;
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
        if(vertexPositions == null || vertexPositions.Count == 0)
        {
            vertexPositions = new List<Vector3>();
            vertexPositions.Add(new Vector3(-1, -1, 0));
            vertexPositions.Add(new Vector3(1, -1, 0));
            vertexPositions.Add(new Vector3(0.0f, 1, 0));

            indices = new int[3];
            indices[0] = 0;
            indices[1] = 2;
            indices[2] = 1;
        }
        createdMesh = new Mesh();
        createdMesh.name = gameObject.name;
        createdMesh.SetVertices(vertexPositions);

        if (vertexColours.Count > 0)
        {
            createdMesh.SetColors(vertexColours);
        }

        if (vertexUVs.Count > 0)
        {
            createdMesh.SetUVs(0, vertexUVs);
        }

        createdMesh.SetIndices(indices, topology, 0); 
        filter.mesh = createdMesh;
    }

    // Update is called once per frame
    void Update()
    {
        if(liveUpdate)
        {
            createdMesh.SetVertices(vertexPositions);
            createdMesh.SetIndices(indices, topology, 0);

            if(vertexColours.Count > 0)
            {
                createdMesh.SetColors(vertexColours);
            }

            if (vertexUVs.Count > 0)
            {
                createdMesh.SetUVs(0, vertexUVs);
            }
        }

        MeshRenderer r = GetComponent<MeshRenderer>();

        Matrix4x4 unityMatrix = Matrix4x4.identity;
        r.material.SetMatrix("AMatrix", unityMatrix);

        r.material.SetFloat("AFloat", 5.0f);
        int i = 6;
        r.material.SetInt("AnInt", i);
    }
}