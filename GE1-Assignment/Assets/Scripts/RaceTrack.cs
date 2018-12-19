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

    void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
        mesh = GetComponent<MeshFilter>().mesh;
        colliderMesh = GetComponent<MeshCollider>();

        musicTrack = audioSource.GetComponent<AudioSource>();
        trackLength = musicTrack.clip.length;
        //Debug.Log(trackLength);
        //Scale the track to the match the length of the song playing
        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, trackLength * 0.8f);
    }


    void Update()
    {
        color += 0.01f;
        if(color>1.0f)
        {
            color = 0;
        }
        m_Renderer.material.color = Color.HSVToRGB(color, 1f, (float)AudioAnalyzer.bands[1]);

        //Loop to run the Y vertices thru a sine wave, which is magnified by the bass frequency of the track
        Vector3[] vertices = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = mesh.vertices[i];
            vertex.y += Mathf.Sin(Time.time * speed + mesh.vertices[i].z) * scale * (float)AudioAnalyzer.bands[1];
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        //Set collider to the match the new mesh
        colliderMesh.sharedMesh = mesh;
    }
}