using UnityEngine;
using System.Collections;

public class RaceTrack : MonoBehaviour
{
    public float scale = 0.1f;
    public float speed = 1.0f;

    public GameObject audioSource;
    private AudioSource musicTrack;
    private float trackLength;

    private Vector3[] baseHeight;

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
        Debug.Log("track length in sec:" + trackLength);
        this.gameObject.transform.localScale = new Vector3(trackLength * 10 * Time.deltaTime * 60, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
    }


    void Update()
    {
        color += 0.01f;
        if(color>1.0f)
        {
            color = 0;
        }
        m_Renderer.material.color = Color.HSVToRGB(color, 0.5f, (float)AudioAnalyzer.bands[1]);

        if (baseHeight == null)
            baseHeight = mesh.vertices;

        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x) * scale;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        colliderMesh.sharedMesh = mesh;
    }
}