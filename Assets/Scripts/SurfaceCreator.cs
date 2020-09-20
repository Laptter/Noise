using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class SurfaceCreator : MonoBehaviour
{
    private Mesh mesh;
    [Range(1, 200)]
    public int resolution = 10;

    private int currentResolution;
    private void OnEnable()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.name = "surface mesh";
            GetComponent<MeshFilter>().mesh = mesh;
        }
        Refresh();
    }

    public void Refresh()
    {
        if (resolution != currentResolution)
        {
            CreateGrid();
        }


       
    }

    private void CreateGrid()
    {
        currentResolution = resolution;
        mesh.Clear();
        Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1)];
        Vector3[] normals = new Vector3[vertices.Length];
        Vector2[] uv = new Vector2[vertices.Length];
        float stepSize = 1f / resolution;
        for (int v = 0, y = 0; y <= resolution; y++)
        {
            for (int x = 0; x <= resolution; x++, v++)
            {
                vertices[v] = new Vector3(x * stepSize - 0.5f, y * stepSize - 0.5f);
                normals[v] = Vector3.back;
                uv[v] = new Vector2(x * stepSize, y * stepSize);
            }
        }
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uv;
        int[] triangles = new int[resolution * resolution * 6];

        for (int t = 0, v = 0, y = 0; y < resolution; y++, v++)
        {
            for (int x = 0; x < resolution; x++, v++, t += 6)
            {
                triangles[t] = v;
                triangles[t + 1] = v + resolution + 1;
                triangles[t + 2] = v + 1;
                triangles[t + 3] = v + 1;
                triangles[t + 4] = v + resolution + 1;
                triangles[t + 5] = v + resolution + 2;
            }
        }
        mesh.triangles = triangles;
    }

    private void OnDisable()
    {
        
    }
}
