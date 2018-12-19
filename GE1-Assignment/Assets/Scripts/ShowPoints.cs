using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPoints : MonoBehaviour {

    public static int points;
    public static int maxPoints;
    Text pointsText;
    Text MaxPointstext;
    public GameObject MaxPoints;
	// Use this for initialization
	void Start() {
        points = 0;
        MaxPointstext = MaxPoints.GetComponent<Text>();
        pointsText = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        pointsText.text = points.ToString();
        MaxPointstext.text = "/" + maxPoints.ToString();
    }
}
