using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class State {
	public string userText;
	public string ahText;
	public string userSound;
	public string ahSound;
}

public class Speech : MonoBehaviour {

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
		state1.userText = "What do you want\nthis time Master?";
		state1.ahText = "";
		Debug.Log (state1);
		Debug.Log (states);
		states.Add(state1);

		var state2 = new State ();
		state2.userText = "";
		state2.ahText = "I am bored, sacrifice me a hundred people. If you do not you will die.";
		states.Add(state2);

		var state3 = new State ();
		state3.userText = "Alright.";
		state3.ahText = "";
		states.Add(state3);

		var state4 = new State ();
		state4.userText = "";
		state4.ahText = "Dash and kill with X.\nCarry the corpses with C.\nMove with the arrow keys.\nGet bonus time when you bring more than 1 on the altar.";
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
					HeadUser.GetComponent<SpriteRenderer>().flipX = true;
					HeadUser.GetComponent<Animator>().SetBool("moveAway", true);
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
			SceneManager.LoadScene("Play", LoadSceneMode.Single);
		}
		yield return new WaitForSeconds(1f);
		currentState++;
		Switch.GetComponent<Renderer>().enabled = true;
		canSwitch = true;
	}


}
