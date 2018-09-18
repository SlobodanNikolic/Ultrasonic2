using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour {

	public float speed;
	public bool falling;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F))
			falling = true;
		if(falling)
			transform.Translate(0f, -speed, 0f);
	}
}
