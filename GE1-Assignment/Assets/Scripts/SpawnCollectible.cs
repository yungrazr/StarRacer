using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectible : MonoBehaviour
{

    public GameObject prefab;
    public GameObject racetrack;
    private int[] lanes = new int[] { -15, -5, 5, 15 };
    private float length;
    private int laneChosen;
    System.Random rnd = new System.Random();
    RaycastHit hit;
    Renderer m_Renderer;
    float color = 0;
    int i;
    bool firstThirdGenerated=false;
    bool secondThirdGenerated = false;
    bool ThirdGenerated = false;
    bool FourthGenerated = false;
    private int TotalCollectible;

    // Use this for initialization
    void Start()
    {
        //Generate the collectibles for the whole length of track
        length = racetrack.transform.localScale.z * 100;
    }

    // Update is called once per frame
    void Update()
    {
        while(Player.rb.gameObject.transform.position.z <= length/4 && !firstThirdGenerated)
        {
            for (i = 100; i < (length / 4); i += 100)
            {
                laneChosen = rnd.Next(0, 4);
                for (int j = 0; j < 100; j += 25)
                {
                    Vector3 pos;
                    pos.x = lanes[laneChosen];
                    pos.y = 2f;
                    pos.z = i + j;
                    Quaternion rot = Quaternion.Euler(0, 0, 0);
                    var obj = Instantiate(prefab, pos, rot);
                    obj.transform.parent = gameObject.transform;
                    TotalCollectible++;
                }
            }
            firstThirdGenerated = true;
            Debug.Log("first section generated");
            TotalCollectible *= 4;
            Debug.Log(TotalCollectible);
            ShowPoints.maxPoints = TotalCollectible;
        }

        while (Player.rb.gameObject.transform.position.z >=(length/4)-200 && !secondThirdGenerated)
        {
            for (i = i; i < (length / 4)*2; i += 100)
            {
                laneChosen = rnd.Next(0, 4);
                for (int j = 0; j < 100; j += 25)
                {
                    Vector3 pos;
                    pos.x = lanes[laneChosen];
                    pos.y = 2f;
                    pos.z = i + j;
                    Quaternion rot = Quaternion.Euler(0, 0, 0);
                    var obj = Instantiate(prefab, pos, rot);
                    obj.transform.parent = gameObject.transform;
                }
            }
            secondThirdGenerated = true;
            Debug.Log("2nd section generated");
        }

        while (Player.rb.gameObject.transform.position.z >=(length / 4)*2-200 && !ThirdGenerated)
        {
            for (i = i; i < (length / 4) * 3; i += 100)
            {
                laneChosen = rnd.Next(0, 4);
                for (int j = 0; j < 100; j += 25)
                {
                    Vector3 pos;
                    pos.x = lanes[laneChosen];
                    pos.y = 2f;
                    pos.z = i + j;
                    Quaternion rot = Quaternion.Euler(0, 0, 0);
                    var obj = Instantiate(prefab, pos, rot);
                    obj.transform.parent = gameObject.transform;
                }
            }
            ThirdGenerated = true;
            Debug.Log("third section generated");
        }

        while (Player.rb.gameObject.transform.position.z >= (length / 4)*3 - 200 && !FourthGenerated)
        {
            for (i = i; i < length - 100; i += 100)
            {
                laneChosen = rnd.Next(0, 4);
                for (int j = 0; j < 100; j += 25)
                {
                    Vector3 pos;
                    pos.x = lanes[laneChosen];
                    pos.y = 2f;
                    pos.z = i + j;
                    Quaternion rot = Quaternion.Euler(0, 0, 0);
                    var obj = Instantiate(prefab, pos, rot);
                    obj.transform.parent = gameObject.transform;
                }
            }
            FourthGenerated = true;
            Debug.Log("fourth section generated");
        }


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

                child.transform.localPosition = new Vector3(child.transform.localPosition.x, Mathf.Lerp(child.transform.localPosition.y, hit.point.y + 2, Time.deltaTime / 0.001f), child.transform.localPosition.z);
            }
            m_Renderer.material.color = Color.HSVToRGB(color, 1f, (float)AudioAnalyzer.bands[1]);

        }

    }
}
