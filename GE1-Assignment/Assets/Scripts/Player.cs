using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody rb;
    float currentHorizontalSpeed = 0;
    const float HorizontalSpeed = 600;

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
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, -HorizontalSpeed, Time.deltaTime / 0.2f);
            rb.AddRelativeForce(Vector3.left * 5 * Time.deltaTime * 50);
        }

        if (Input.GetKey(KeyCode.A))
        {
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, HorizontalSpeed, Time.deltaTime / 0.2f);
            rb.AddRelativeForce(Vector3.right * 5 * Time.deltaTime * 50);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            currentHorizontalSpeed = Mathf.Lerp(currentHorizontalSpeed, 0, Time.deltaTime / 0.1f);
        }
    }
}
