using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody rb;
    float currentHorizontalSpeed = 0;
    const float HorizontalSpeed = 10;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {


        //Continuous acceleration
        rb.AddRelativeForce(Vector3.forward * 5 * Time.deltaTime * 50);

        //Key inputs to move left/right
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * HorizontalSpeed * Time.deltaTime * 50);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * HorizontalSpeed * Time.deltaTime * 50);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, 0, Time.deltaTime / 0.1f);
        }
    }
}
