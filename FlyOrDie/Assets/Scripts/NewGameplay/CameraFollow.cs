using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform whoToFollow;
	public bool notShaking;
	public float power;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (notShaking) {
			transform.position = new Vector3 (whoToFollow.position.x, whoToFollow.position.y + 5f, transform.position.z);
		} else {
			float newX = whoToFollow.position.x + Random.Range (-power / 2f, power / 2f);
			float newY = whoToFollow.position.y + 5f + Random.Range (-power / 2f, power / 2f);
			transform.position = new Vector3 (newX, newY, transform.position.z);
		}
	}
}
