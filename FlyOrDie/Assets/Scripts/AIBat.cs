using UnityEngine;
using System.Collections;
using Pokega;

public class AIBat : MonoBehaviour {

	public GameObject lRO;
	public GameObject rRO;
	public GameObject mRO;

	public GameObject batBody;
	public int blinkCounter;

	public Vector3 fwd;
	public Vector3 rigth;
	public Vector3 left;
	public RaycastHit objectHit;
	public float speed;
	public bool goLeft;
	public bool goRight;
	public bool leftHit;
	public bool rightHit;
	public bool lHit;
	public bool rHit;
	public bool middleHit;
	public float forwardRayLength;
	public float sideRayLength;
	public bool canMove = false;
	public RecordedGameplay rec;
	public int i = 0;
	public int lives;

	// Use this for initialization
	void Start () {
//		fwd = transform.TransformDirection (Vector3.up);
//		left = transform.TransformDirection (Vector3.left);
//		rigth = transform.TransformDirection (Vector3.right);
	}
	
	// Update is called once per frame
	void Update () {



		if (canMove) {
			if(i<rec.playerPositions.Count)
			transform.position = rec.playerPositions [i];
			i++;
		
		}
		

	}

	public void LivesMinus(){
		lives--;
		if (lives == 0) {
			//KILL PLAYER 2
			gameObject.SetActive(false);
			App.recorded.CancelInvoke ();
			App.gameplay.StartSpawningObstacles ();
		}
	}


	void OnCollisionEnter(Collision coll){
		LivesMinus ();
		BlinkOn ();
	}

	public void BlinkOn(){
		batBody.GetComponent<MeshRenderer>().enabled = false;
		gameObject.GetComponent<BoxCollider> ().enabled = false;
		Invoke ("BlinkOff", 0.2f * Time.timeScale);
	}

	public void BlinkOff(){
		//Debug.Log ("OMG");
		batBody.GetComponent<MeshRenderer>().enabled = true;
		if (blinkCounter > 0) {
			Invoke ("BlinkOn", 0.2f * Time.timeScale);
			blinkCounter--;
		} else {
			gameObject.GetComponent<BoxCollider> ().enabled = true;
			blinkCounter = 4;
		}

	}
}

