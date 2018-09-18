using UnityEngine;
using System.Collections;
using Pokega;

public class MovingPray : MonoBehaviour {

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
		currentPos = transform.position;
		newPos = transform.position;
		InvokeRepeating ("Move", 0f, moveTime);
	}

	// Update is called once per frame
	void Update () {
		if (left && transform.position.x >= -0.5f) {
			transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z);
		}
		if (right && transform.position.x <= 0.5f) {
			transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z);
		}
	}

	public void Move(){
		int r = Random.Range (0, 3);
		if (r == 0) {
			Debug.Log ("LEFT");

			left = true;
			right = false; 
		}
		else if (r == 1) {
			Debug.Log ("RIGHT");

			left = false;
			right = true;
		} else {
			Debug.Log ("STRAIGHT");

			left = false;
			right = false;
		}
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
