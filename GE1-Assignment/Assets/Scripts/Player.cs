using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject pCamera;

    Rigidbody rb;
    const float playerSpeed = 10;
    const float playerRotateSpeed = 10;

    RaycastHit hit;
    float dist;
    Vector3 direction;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {


        //Continuous acceleration
        rb.AddRelativeForce(Vector3.forward * playerSpeed * Time.deltaTime * 60);

        //Key inputs to move left/right
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * playerRotateSpeed * Time.deltaTime * 50);
            rb.transform.localRotation = Quaternion.Slerp(rb.transform.localRotation, Quaternion.Euler(0, 0, 10), Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * playerRotateSpeed * Time.deltaTime * 50);
            rb.transform.localRotation = Quaternion.Slerp(rb.transform.localRotation, Quaternion.Euler(0, 0, -10), Time.deltaTime);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rb.transform.localRotation = Quaternion.Slerp(rb.transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
        }

        dist = Mathf.Infinity;
        direction = Vector3.down;
        //edit: to draw ray also//
        Debug.DrawRay(transform.position, direction * dist, Color.green);
        //end edit//
        if (Physics.Raycast(transform.position, direction, out hit, dist))
        {
            rb.transform.position = new Vector3(rb.transform.position.x, Mathf.Lerp(rb.transform.position.y, hit.point.y + 1, Time.deltaTime / 0.1f), rb.transform.position.z);
        }

    }
}
