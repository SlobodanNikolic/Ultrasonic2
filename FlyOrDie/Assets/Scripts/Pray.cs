using UnityEngine;
using System.Collections;
using Pokega;

public class Pray : MonoBehaviour {

	public float detectionTime;
	public GameObject eatenParticles;


	// Use this for initialization
	void Start () {
		eatenParticles = GameObject.Find ("BloodParticles");
	

	}


		
	
	// Update is called once per frame
	void Update () {
	}


	public void Detected(){

		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		Invoke ("Dissappear", detectionTime);
	}

	public void Dissappear(){
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}

	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			Debug.Log ("PRAY EATEN");
			App.score.ScorePlus (5f, "pray");
			eatenParticles.GetComponent<ParticleSystem> ().Play ();
			Destroy (gameObject);
		}

		if (other.name == "DestroyTrigger")
			Destroy (gameObject);
	}
}
