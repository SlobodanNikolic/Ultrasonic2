using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pokega;

public class Blink : MonoBehaviour {

	public float blinkTime;
	public UILabel label;


	void Awake(){
		
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void BlinkNewBest(){
		gameObject.SetActive (true);
		StartCoroutine ("BlinkNewBestLabel");
	}

	public IEnumerator BlinkNewBestLabel(){
		for (int i = 0; i < 10; i++) {
			label.enabled = true;
			yield return new WaitForSeconds (blinkTime);

			label.enabled = false;
			yield return new WaitForSeconds (blinkTime);
		}
		label.enabled = true;
	}

	public void SetNewBestFalse(){
		label.enabled = false;
	}
}
