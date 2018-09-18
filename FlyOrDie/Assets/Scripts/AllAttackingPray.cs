using UnityEngine;
using System.Collections;
using Pokega;

public class AllAttackingPray : MonoBehaviour {

	public float detectionTime;
	public GameObject eatenParticles;
	public float scoreValue;

	public Vector3 currentPos;
	public Vector3 newPos;
	public float lerpTime;

	public bool left;
	public bool right;

	public float speed;
	public float moveTime;

	// Use this for initialization
	void Start () {
		eatenParticles = GameObject.Find ("BloodParticles");

		InvokeRepeating ("Move", 0f, moveTime);
	}

	// Update is called once per frame
	void Update () {

		if(App.player.transform.position.x > transform.position.x)
			transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z);
		else
			transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z);
		
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
			App.score.ScorePlus (scoreValue, "pray");
			eatenParticles.GetComponent<ParticleSystem> ().Play ();
			Destroy (gameObject);
		}

		if (other.name == "DestroyTrigger")
			Destroy (gameObject);
	}
}
