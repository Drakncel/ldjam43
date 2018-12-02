using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Speech2 : MonoBehaviour {

	public GameObject HeadUser;
	public GameObject UserText;
	public GameObject AhText;
	public GameObject Switch;
	public GameObject UserSound;
	public GameObject AhSound;
	private int currentState = 0;
	private List<State> states = new List<State>();
	private bool canSwitch = true;

	// Use this for initialization
	void Start () {
		var state1 = new State ();
		state1.userText = "There you go,\nwhat next?";
		state1.ahText = "";
		Debug.Log (state1);
		Debug.Log (states);
		states.Add(state1);

		var state2 = new State ();
		state2.userText = "";
		state2.ahText = "You are my most powerful shaman, but I need more power.\nIt is time to make your last sacrifice.";
		states.Add(state2);

		var state3 = new State ();
		state3.userText = "What?";
		state3.ahText = "";
		states.Add(state3);

		var state4 = new State ();
		state4.userText = "";
		state4.ahText = "Let's go save the world.";
		states.Add(state4);

		Switch.GetComponent<Renderer>().enabled = false;
		canSwitch = false;
		StartCoroutine ("Next");
	}

	// Update is called once per frame
	void Update () {
		if (canSwitch && Input.GetKeyDown("x")) {
			Switch.GetComponent<Renderer>().enabled = false;
			canSwitch = false;
			StartCoroutine("Next");
		}
	}

	IEnumerator Next() 
	{
		if (currentState < states.Count) {
			var stateTo = states [currentState];
			UserText.GetComponent<Text> ().text = stateTo.userText;
			AhText.GetComponent<Text> ().text = stateTo.ahText;
			if (stateTo.userText != "") {
				if (currentState == 2) {
					// HeadUser.GetComponent<SpriteRenderer>().flipX = true;
					HeadUser.GetComponent<Animator>().SetBool("suicide", true);
				}
				HeadUser.GetComponent<Animator>().SetBool("speak", true);
				AhSound.GetComponent<AudioSource> ().Stop ();
				yield return new WaitForSeconds(.2f);
				UserSound.GetComponent<AudioSource> ().Play ();
			} else {
				HeadUser.GetComponent<Animator>().SetBool("speak", false);
				UserSound.GetComponent<AudioSource> ().Stop ();
				yield return new WaitForSeconds(.2f);
				AhSound.GetComponent<AudioSource> ().Play ();
			}
		} else {
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);
		}
		yield return new WaitForSeconds(1f);
		currentState++;
		Switch.GetComponent<Renderer>().enabled = true;
		canSwitch = true;
	}


}
