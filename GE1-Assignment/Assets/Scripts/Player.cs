using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject pCamera;
    public GameObject trail1, trail2;
    private float color;

    public static Rigidbody rb;
    const float playerSpeed = 20;
    const float playerStrafeSpeed = 40;

    RaycastHit hit;
    float dist;
    Vector3 direction;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 30);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 frontPos = new Vector3(0, 0, 0);
        Vector3 backPos = new Vector3(0, 0, 0);
        rb.AddRelativeForce(Vector3.forward * playerSpeed * Time.deltaTime);

        //Key inputs to move left/right
        if (Input.GetKey(KeyCode.D) || Input.GetAxis("XAxis") == -1)
        {
            rb.AddRelativeForce(Vector3.right * playerStrafeSpeed * Time.deltaTime * 30);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.Euler(0, 0, 15), Time.deltaTime * 5);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetAxis("XAxis") == 1)
        {
            rb.AddRelativeForce(Vector3.left * playerStrafeSpeed * Time.deltaTime * 30);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.Euler(0, 0, -15), Time.deltaTime * 5);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);
        }

        //Collision detection for side of road
        if (rb.transform.position.x < -19)
        {
            rb.transform.position = new Vector3(-19, rb.transform.position.y, rb.transform.position.z);
        }
        if (rb.transform.position.x > 19)
        {
            rb.transform.position = new Vector3(19, rb.transform.position.y, rb.transform.position.z);
        }

        //Raycasting
        dist = Mathf.Infinity;
        direction = Vector3.down;
        Vector3 offset = rb.transform.forward * -1f;
        Vector3 posFront = rb.transform.position + offset;
        Vector3 posBack = rb.transform.position - offset;

        if (Physics.Raycast(transform.position, direction, out hit, dist))
        {
            rb.transform.position = new Vector3(rb.transform.position.x, Mathf.Lerp(rb.transform.position.y, hit.point.y + 2, Time.deltaTime / 0.001f), rb.transform.position.z);
        }
        if (Physics.Raycast(posFront, direction, out hit, dist))
        {
            frontPos = hit.point;
        }
        if (Physics.Raycast(posBack, direction, out hit, dist))
        {
            backPos = hit.point;
        }
        if (frontPos.y > backPos.y)
        {
            float angle = Vector3.Angle(frontPos, backPos);
            rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, Quaternion.Euler(angle*2, 0, 0), Time.deltaTime * 5);
        }
        if (frontPos.y < backPos.y)
        {
            float angle = Vector3.Angle(frontPos, backPos);
            rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, Quaternion.Euler(-angle*2, 0, 0), Time.deltaTime * 5);
        }

        color += 0.01f;
        if (color > 1.0f)
        {
            color = 0;
        }
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.HSVToRGB(color, 1f, (float)AudioAnalyzer.bands[1]), 0.0f), new GradientColorKey(Color.HSVToRGB(color, 1f, (float)AudioAnalyzer.bands[1]), 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1, 0.0f), new GradientAlphaKey(1, 1.0f) }
        );

        if (Physics.Raycast(transform.position, direction, out hit, dist))
        {
            trail1.GetComponent<TrailRenderer>().colorGradient = gradient;
            trail1.transform.position = new Vector3(trail1.transform.position.x, Mathf.Lerp(trail1.transform.position.y, hit.point.y + 2, Time.deltaTime / 0.001f), trail1.transform.position.z);
        }
        if (Physics.Raycast(transform.position, direction, out hit, dist))
        {
            trail2.GetComponent<TrailRenderer>().colorGradient = gradient;
            trail2.transform.position = new Vector3(trail2.transform.position.x, Mathf.Lerp(trail2.transform.position.y, hit.point.y + 2, Time.deltaTime / 0.001f), trail2.transform.position.z);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();

        }
    }
}
