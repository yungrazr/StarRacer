using UnityEngine;
using System.Collections;

public class RaceTrack : MonoBehaviour
{
    public float scale = 0.1f;
    public float speed = 1.0f;

    public GameObject audioSource;
    private AudioSource musicTrack;
    private float trackLength;

    private Mesh mesh;
    private MeshCollider colliderMesh;
    private Renderer m_Renderer;

    float color = 0;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        mesh = GetComponent<MeshFilter>().mesh;
        colliderMesh = GetComponent<MeshCollider>();

        musicTrack = audioSource.GetComponent<AudioSource>();
        trackLength = musicTrack.clip.length;
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, trackLength * 1.10f);
    }


    void Update()
    {
        color += 0.01f;
        if(color>1.0f)
        {
            color = 0;
        }
        m_Renderer.material.color = Color.HSVToRGB(color, 1f, (float)AudioAnalyzer.bands[1]);

        Vector3[] vertices = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = mesh.vertices[i];
            vertex.y += Mathf.Sin(Time.time * speed + mesh.vertices[i].z) * scale * (float)AudioAnalyzer.bands[1];
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        colliderMesh.sharedMesh = mesh;
    }
}