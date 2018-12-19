using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Button playBtn, quitBtn;
    public Material[] skybox;
    public GameObject image;
    public Button track1;
    public Button track2;

    // Use this for initialization
    void Start () {
        playBtn.onClick.AddListener(showTracks);
        quitBtn.onClick.AddListener(quit);
        track1.onClick.AddListener(changeScene);
        track2.onClick.AddListener(changeScene2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void showTracks()
    {
        track1.gameObject.SetActive(true);
        track2.gameObject.SetActive(true);

    }
    void changeScene()
    {
        image.SetActive(true);
        SceneManager.LoadScene("Track");
    }

    void changeScene2()
    {
        image.SetActive(true);
        SceneManager.LoadScene("Track2");
    }

    void quit()
    {
        Application.Quit();
    }
}
