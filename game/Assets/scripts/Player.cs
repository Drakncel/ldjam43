using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	Animator anim;
	public GameObject Char;
	public GameObject KilledText;
	public GameObject SecondsText;
	public GameObject SacrText;
	public GameObject Spawner;
	public float baseSpeed = 7f;
	public float currentSpeed = 7f;
	private float speed = 7f;
	private Vector2 movement;
	private bool isAtk = false;
	private Vector2 lockedMovement;
	private int nbKilled = 0;
	private int secondsLeft = 60;
	private int nbSacr = 0;
	private bool sacrificing = false;
	public AudioClip dash;
	public AudioClip hit;
	public AudioClip sacr;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		StartCoroutine ("Countdown");
		SecondsText.GetComponent<Text> ().text = secondsLeft + " seconds left";
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!isAtk) {
			float inputX = Input.GetAxisRaw ("Horizontal");
			float inputY = Input.GetAxisRaw ("Vertical");
			movement = new Vector2 (inputX, inputY);
			lockedMovement = movement;
			transform.Translate (movement * speed * Time.deltaTime);
		} else {
			transform.Translate (lockedMovement * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown ("x") && !isAtk) {
			audioSource.PlayOneShot (dash, .7f);
			Char.GetComponent<Animator> ().SetBool ("atk", true);
			speed = currentSpeed * 3;
			isAtk = true;
			StartCoroutine ("Atk");
		}
	}
	IEnumerator Countdown()
	{
		while (secondsLeft > 0) {
			yield return new WaitForSeconds(1f);
			secondsLeft--;
			SecondsText.GetComponent<Text> ().text = secondsLeft + " seconds left";
		}

		SceneManager.LoadScene("Lose", LoadSceneMode.Single);
	}

	IEnumerator Atk() 
	{
		yield return new WaitForSeconds(.15f);
		speed = currentSpeed;
		Char.GetComponent<Animator> ().SetBool ("atk", false);
		yield return new WaitForSeconds(.15f);
		isAtk = false;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (Char.GetComponent<Animator> ().GetBool ("atk")) {
			StartCoroutine ("Kill", col.gameObject);
		}
	}

	void OnCollisionStay2D (Collision2D col){
		if (Input.GetKeyDown ("c") && col.gameObject.GetComponent<Enemy>().grabbable) {
			currentSpeed -= .5f;
			col.gameObject.GetComponent<Enemy>().grab();
		}
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.name == "Altar" && !sacrificing) {
			sacrificing = true;
			var enemies = GameObject.FindGameObjectsWithTag("enemy");
			int combo = 0;
			foreach (GameObject enemy in enemies) {
				if (enemy.GetComponent<Enemy> ().grabbed) {
					combo += 1;
					Destroy (enemy);
					secondsLeft += 2;
					nbSacr+=1;
					SacrText.GetComponent<Text> ().text = nbSacr + " sacrificed";
				}
			}
			if (combo > 0) {
				audioSource.PlayOneShot (sacr, .7f);
			}
			currentSpeed = baseSpeed;
			secondsLeft += combo - 1;
			Spawner.GetComponent<Spawner> ().setSacr (nbSacr);
			sacrificing = false;
		}
	}

	IEnumerator Kill(GameObject enemy) 
	{
		enemy.GetComponent<Enemy>().setMovement(lockedMovement);
		audioSource.PlayOneShot (hit, 1f);
		yield return new WaitForSeconds (.2f);
		nbKilled++;
		KilledText.GetComponent<Text> ().text = nbKilled + " killed";
		SecondsText.GetComponent<Text> ().text = secondsLeft + " seconds left";
		enemy.GetComponent<Enemy>().setMovement(new Vector2(0,0));
		enemy.GetComponent<Enemy>().kill();
	}
}