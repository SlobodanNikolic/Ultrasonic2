using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	// Use this for initialization
	private Vector3 startPos;
	void Start () 
	{
		startPos = transform.position;	
	}
	
	// Update is called once per frame
	public IEnumerator Shake(float power, float duration)
	{
		Vector3 currPos = transform.position;
		while (duration > 0) 
		{
			float newX = currPos.x + Random.Range (-power / 2f, power / 2f);
			//float newY = currPos.y + Random.Range (-power / 2f, power / 2f);
			transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
			duration -= Time.deltaTime;
			yield return null;
		}
		//transform.position = startPos;
	}

}
