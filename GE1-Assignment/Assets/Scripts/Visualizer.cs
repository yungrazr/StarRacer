using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Visualizer : MonoBehaviour {

    public int numObjects = 20;
    public float radius = 20;
    public GameObject prefab;
    private float angleRotate=0;
    public float pulseStrength = 10;
    Renderer m_Renderer;
    float color = 0;
    public float distanceFromPlayer;
    public int frequencyBand;
    public int rotationDirection;

    void Start()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            float ang = 360 / numObjects;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(i * ang * Mathf.Deg2Rad);
            pos.y = center.y + radius * Mathf.Cos(i * ang * Mathf.Deg2Rad);
            pos.z = center.z;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            var obj = Instantiate(prefab, pos, rot);
            obj.transform.parent = gameObject.transform;
        }
    }

    void Update()
    {
        color += 0.01f;
        if (color > 1.0f)
        {
            color = 0;
        }

        foreach (Transform child in transform)
        {
            m_Renderer = child.gameObject.GetComponent<Renderer>();
            child.gameObject.transform.localScale = new Vector3(Mathf.Lerp(child.gameObject.transform.localScale.x, (float)AudioAnalyzer.bands[frequencyBand] * pulseStrength, Time.deltaTime / 0.1f),
                Mathf.Lerp(child.gameObject.transform.localScale.y, (float)AudioAnalyzer.bands[frequencyBand] * pulseStrength, Time.deltaTime / 0.1f),
                Mathf.Lerp(child.gameObject.transform.localScale.z, (float)AudioAnalyzer.bands[frequencyBand] *pulseStrength, Time.deltaTime / 0.1f));
            m_Renderer.material.color = Color.HSVToRGB(color, 1f, (float)AudioAnalyzer.bands[frequencyBand]);
        }
        angleRotate += (float)AudioAnalyzer.bands[1] * rotationDirection;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angleRotate), 0.1f);
        transform.position = new Vector3(0, Player.rb.transform.position.y, Player.rb.transform.position.z + distanceFromPlayer);

    }
}
