using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Button playBtn, quitBtn;

    // Use this for initialization
    void Start () {
        playBtn.onClick.AddListener(changeScene);
        quitBtn.onClick.AddListener(quit);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void changeScene()
    {
        SceneManager.LoadScene("Track");
    }

    void quit()
    {
        Application.Quit();
    }
}
