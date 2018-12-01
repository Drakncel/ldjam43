using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

	public GameObject Indiana;
	public GameObject LevelText;
	public int level = 1;
	public int sacr = 0;
	public string title = "Part 1 - Eat their hats";

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
			if (level == 1 || level == 2) {
				Instantiate(Indiana, new Vector2(Random.Range(-9f, 9f), Random.Range(-5f,5f)), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSacr (int sacrNb){
		sacr = sacrNb;
		if (sacr >= 10 && level == 1){
			level = 2;
			title = "Part 2 - Don't fall in their traps";
			StartCoroutine ("ShowTitle");
		}
	}
}
