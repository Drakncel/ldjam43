using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public Vector2 movement = new Vector2(0,0);
	public float speed = 1;
	public bool grabbable = false;
	public bool grabbed = false;

	public GameObject Char;
	// Use this for initialization
	void Start () {
		StartCoroutine ("RandomMovement");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (grabbed) {
			transform.position = GameObject.Find ("Player").transform.position;
		} else {
			transform.Translate (movement * speed * Time.deltaTime);
		}
	}

	IEnumerator RandomMovement()
	{
		while (!grabbable && !grabbed) {
			var direction = new Vector2 (Random.Range (-1.0f, 1.0f), Random.Range (-1.0f, 1.0f));
			movement = direction;
			speed = Random.Range (5.0f, 10.0f);
			yield return new WaitForSeconds(0.5f);
		}
	}

	public void setMovement (Vector2 mov){
		speed = 20;
		movement = mov;
	}

	public void kill() {
		Char.GetComponent<SpriteRenderer> ().color = Color.red;
		grabbable = true;
	}

	public void grab(){
		GetComponent<CircleCollider2D> ().enabled = false;
		grabbed = true;
		grabbable = false;
	}
}
