using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public string scene = "Intro";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("x")) {
			SceneManager.LoadScene(scene, LoadSceneMode.Single);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
