using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {

	public GameObject Indiana;
	public GameObject LevelText;
	public int level = 1;
	public int sacr = 0;
	public string title = "Eat their hats";

	// Use this for initialization
	void Start () {
		StartCoroutine ("ShowTitle");
		StartCoroutine("SpawnIndiana");
	}

	IEnumerator ShowTitle()
	{
		LevelText.GetComponent<Text> ().text = title;
		yield return new WaitForSeconds (4f);
		LevelText.GetComponent<Text> ().text = "";
	}

	IEnumerator SpawnIndiana()
	{
		while (true) {
			yield return new WaitForSeconds (2f);
			if (level == 1 || level == 2 || level == 3) {
				Instantiate(Indiana, new Vector2(Random.Range(-9f, 9f), Random.Range(-5f,5f)), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSacr (int sacrNb){
		sacr = sacrNb;
		if (sacr >= 15 && level == 1){
			level = 2;
			title = "Don't fall in their traps";
			StartCoroutine ("ShowTitle");
			var traps = GameObject.FindGameObjectsWithTag("killer");
			int max = 1;
			int i = 0;
			foreach (GameObject trap in traps) {
				if (i <= max) {
					trap.GetComponent<Renderer>().enabled = true;
					trap.GetComponent<CircleCollider2D> ().enabled = true;
				}
				i++;
			}
		}

		if (sacr >= 30 && level == 2){
			level = 3;
			title = "More traps?";
			StartCoroutine ("ShowTitle");
			var traps = GameObject.FindGameObjectsWithTag("killer");
			foreach (GameObject trap in traps) {
				trap.GetComponent<Renderer>().enabled = true;
				trap.GetComponent<CircleCollider2D> ().enabled = true;
			}
		}
		if (sacr >= 50) {
			SceneManager.LoadScene("Middle", LoadSceneMode.Single);
		}
	}
}
