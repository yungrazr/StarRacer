using UnityEngine;
using System.Collections;

public class RaceTrack : MonoBehaviour
{
    public float scale = 0.1f;
    public float speed = 1.0f;
    float noiseStrength = 1f;
    float noiseWalk = 1f;

    private Vector3[] baseHeight;

    public GameObject audioSource;
    private AudioSource musicTrack;
    private float trackLength;

    void Start()
    {
        musicTrack = audioSource.GetComponent<AudioSource>();
        trackLength = musicTrack.clip.length;
        Debug.Log(trackLength);
        this.gameObject.transform.localScale = new Vector3(trackLength*10, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
    }


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