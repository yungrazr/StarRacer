using UnityEngine;
using System.Collections;

public class SineWaveMesh : MonoBehaviour
{
    public float scale = 0.1f;
    public float speed = 1.0f;
    float noiseStrength = 1f;
    float noiseWalk = 1f;

    private Vector3[] baseHeight;

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        MeshCollider colliderMesh = GetComponent<MeshCollider>();

        if (baseHeight == null)
            baseHeight = mesh.vertices;

        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x) * scale;
            //vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        colliderMesh.sharedMesh = mesh;
    }
}