using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPoints : MonoBehaviour {

    public static int points;
    Text text;
	// Use this for initialization
	void Start() {
		text = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = points.ToString();
	}
}
