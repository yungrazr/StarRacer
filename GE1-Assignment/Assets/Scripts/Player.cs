using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject pCamera;

    Rigidbody rb;
    const float playerSpeed = 10;
    const float playerStrafeSpeed = 15;

    RaycastHit hit;
    float dist;
    Vector3 direction;

    Vector3 frontPos = new Vector3(0, 0, 0);
    Vector3 backPos = new Vector3(0, 0, 0);

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
            rb.AddRelativeForce(Vector3.right * playerStrafeSpeed * Time.deltaTime * 50);
            rb.transform.localRotation = Quaternion.Slerp(rb.transform.localRotation, Quaternion.Euler(0, 0, 10), Time.deltaTime * 5);

        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * playerStrafeSpeed * Time.deltaTime * 50);
            rb.transform.localRotation = Quaternion.Slerp(rb.transform.localRotation, Quaternion.Euler(0, 0, -10), Time.deltaTime * 5);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rb.transform.localRotation = Quaternion.Slerp(rb.transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);
        }

        dist = Mathf.Infinity;
        direction = Vector3.down;
        //edit: to draw ray also//
        Debug.DrawRay(transform.position, direction * dist, Color.green, 20);
        //end edit//
        if (Physics.Raycast(transform.position, direction, out hit, dist))
        {
            rb.transform.position = new Vector3(rb.transform.position.x, Mathf.Lerp(rb.transform.position.y, hit.point.y + 2, Time.deltaTime / 0.1f), rb.transform.position.z);
        }
        Vector3 offset = rb.transform.forward * -1f;
        Vector3 posFront = rb.transform.position + offset;
        Vector3 posBack = rb.transform.position - offset;
        if (Physics.Raycast(posFront, direction, out hit, dist))
        {
            //Debug.Log("Front:" + hit.point);
            frontPos = hit.point;
        }
        if (Physics.Raycast(posBack, direction, out hit, dist))
        {
            //Debug.Log("Back:" + hit.point);
            backPos = hit.point;
        }
        if(frontPos.y>backPos.y)
        {
            float angle = Vector3.Angle(frontPos, backPos);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.Euler(angle, rb.transform.rotation.y, rb.transform.rotation.z), Time.deltaTime);
        }
        if (frontPos.y < backPos.y)
        {
            float angle = Vector3.Angle(frontPos, backPos);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.Euler(-angle, rb.transform.rotation.y, rb.transform.rotation.z), Time.deltaTime);
        }

    }
}
