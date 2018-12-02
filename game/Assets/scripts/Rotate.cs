using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
		GetComponent<CircleCollider2D> ().enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3(0,0,1) * 180 * Time.deltaTime);
	}
}
