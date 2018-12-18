using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectible : MonoBehaviour {

    public GameObject prefab;
    public GameObject racetrack;
    private int[] lanes = new int[] { -15, -5, 5, 15 };
    private float length;
    System.Random rnd = new System.Random();

    // Use this for initialization
    void Start () {
        length = racetrack.transform.localScale.z * 10;
        for (int i = 0; i < length; i++)
        {
            for (int j=0; j< 10;j++)
            {
                Vector3 pos;
                pos.x = rnd.Next(0, 3);
                pos.y = 0;
                pos.z = i * 10;
                Quaternion rot = Quaternion.Euler(45, 90, 45);
                var obj = Instantiate(prefab, pos, rot);
                obj.transform.parent = gameObject.transform;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
