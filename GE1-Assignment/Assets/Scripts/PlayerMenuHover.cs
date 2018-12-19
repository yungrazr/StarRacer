using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuHover : MonoBehaviour {

    Rigidbody rb;
    bool goUp = true;
    bool goDown = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (rb.transform.position.y < 1.1f && goUp)
        {

            rb.transform.position = new Vector3(rb.transform.position.x, Mathf.Lerp(rb.transform.position.y, rb.transform.position.y + 0.1f, Time.deltaTime * 0.5f), rb.transform.position.z);
            if(rb.transform.position.y >= 1.1f)
            {
                goUp = false;
                goDown = true;
            }
        }
        else if (rb.transform.position.y > 0.9f && goDown)
        {
            rb.transform.position = new Vector3(rb.transform.position.x, Mathf.Lerp(rb.transform.position.y, rb.transform.position.y - 0.1f, Time.deltaTime * 0.5f), rb.transform.position.z);
            if (rb.transform.position.y <= 0.9f)
            {
                goUp = true;
                goDown = false;
            }
        }
    }
}
