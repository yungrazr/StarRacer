using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollectible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //If player hits the collectible, points are added
        if(other==Player.rb.GetComponent<BoxCollider>())
        {
            ShowPoints.points++;
            Object.Destroy(this.gameObject);
        }
        //else the collectible is removed by the destroy collider.
        else
        {
            Object.Destroy(this.gameObject);
        }

    }
}
