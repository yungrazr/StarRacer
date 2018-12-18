using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectible : MonoBehaviour {

    public GameObject prefab;
    public GameObject racetrack;
    private int[] lanes = new int[] { -15, -5, 5, 15 };
    private float length;
    private int laneChosen;
    System.Random rnd = new System.Random();
    RaycastHit hit;
    Renderer m_Renderer;
    float color = 0;

    // Use this for initialization
    void Start () {
        length = racetrack.transform.localScale.z * 20;
        for (int i = 100; i < length; i+=100)
        {
            laneChosen = rnd.Next(0, 4);
            for (int j=0; j<100;j+=10)
            {
                Vector3 pos;
                pos.x = lanes[laneChosen];
                pos.y = 2f;
                pos.z = i+j;
                Quaternion rot = Quaternion.Euler(0,0,0);
                var obj = Instantiate(prefab, pos, rot);
                obj.transform.parent = gameObject.transform;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        color += 0.01f;
        if (color > 1.0f)
        {
            color = 0;
        }
        foreach (Transform child in transform)
        {
            m_Renderer = child.gameObject.GetComponent<Renderer>();
            var dist = Mathf.Infinity;
            var direction = Vector3.down;

            if (Physics.Raycast(child.transform.localPosition, direction, out hit, dist))
            {

                child.transform.localPosition = new Vector3(child.transform.localPosition.x, Mathf.Lerp(child.transform.localPosition.y, hit.point.y+2, Time.deltaTime /0.001f), child.transform.localPosition.z);
            }
            m_Renderer.material.color = Color.HSVToRGB(color, 1f, (float)AudioAnalyzer.bands[1]);

        }

    }

}
