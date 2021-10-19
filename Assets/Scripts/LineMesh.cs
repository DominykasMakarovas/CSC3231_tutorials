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
        /*
         * Not ideal to use as when you change the values in x and y you can see that the line becomes jagged in places
         * due to slight floating point errors over the course of the line. 
         */
        float lineLength = (endPoint - startPoint).magnitude;
        float reciprocal = 1.0f / (lineLength - 1);
        
        float t = 0;
        for (int i = 0; i < (int) lineLength; ++i)
        {
            Vector3 newPoint = Vector3.Lerp(startPoint, endPoint, t);
            generatedPoints.Add(newPoint);
            t += reciprocal;
        }

    }

    void BresenhamLine()
    {
        // Direction required to know whether line is 'steep' or not.
        Vector3 direction = endPoint - startPoint;
        float slope = 0.0f;
        int numSteps = 0;

        Vector3 scanChange = Vector3.zero;
        Vector3 periodicChange = Vector3.zero;

        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            // Steep line
            slope = Mathf.Abs(direction.x / direction.y);
            scanChange.y = (direction.y < 0.0f) ? -1.0f : 1.0f;
            periodicChange.x = (direction.x < 0.0f) ? -1.0f : 1.0f;
            numSteps = (int) Mathf.Abs(direction.y);
        }
        else
        {
            slope = Mathf.Abs(direction.y / direction.x);
            scanChange.x = (direction.x < 0.0f) ? -1.0f : 1.0f;
            periodicChange.y = (direction.y < 0.0f) ? -1.0f : 1.0f;
            numSteps = (int) Mathf.Abs(direction.x);
        }

        Vector3 currentPoint = startPoint;
        float error = 0.0f;
        for (int i = 0; i < numSteps; ++i)
        {
            error += slope;
            if (error > 0.5f)
            {
                error -= 1.0f;
                currentPoint += periodicChange;
            }
            currentPoint += scanChange;
            generatedPoints.Add(currentPoint);
        }
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