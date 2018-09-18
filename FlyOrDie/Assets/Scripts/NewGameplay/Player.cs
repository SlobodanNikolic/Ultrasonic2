using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokega;

public class Player : MonoBehaviour {

	public bool flying;
	public float speed;
	public float slow;
	public float fast;
	public float strafeSpeed;
	public bool keyboard;
	public Vector3 position;
	public Vector3 playerStartPosition;
	public GameObject movePlaneObject;
	public bool lerpFinished;
	public float rayDistance;
	public Vector3 localPoint;
	public Vector3 point;
	public string isHit = "";
	public GUIStyle textStyle;
	public Transform particleParent;
	public float particleSpeed;
	public GameObject playerObject;
	public UISprite energyFillSprite;
	public float energy;
	public float energyChangeTime;
	public float lootEnergyIncrease;
	public bool inWeb = false;
	public GameObject webObject;
	public float inWebTime;
	public bool canMove = true;
	public bool touch;
	public float touchStrafeSpeed;
	public float currentPos;
	public float previousPos;
	public bool fastStrafe;
	public float minBatPosX;
	public float maxBatPosX;
	public float velocity;
	public bool memoryControl;
	public float memoryYLimit;
	public float memoryXLimit;
	public float scoreCounter;
	public float shopScoreLimit;



	// Use this for initialization
	void Start () {
		playerStartPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && App.game.currentGameState == Game.GameState.START_GAME && !inWeb) {
			flying = true;
			App.game.tapToStartObject.SetActive (false);
		}
//		if (keyboard) {
//			if (Input.GetKey (KeyCode.LeftArrow)) {
//				transform.Translate (Vector3.left * strafeSpeed);
//
//			}
//			if (Input.GetKey (KeyCode.RightArrow)) {
//				transform.Translate (Vector3.right * strafeSpeed);
//
//			}
//		} else if (touch) {
//			
//			if (Input.GetMouseButton (0) && canMove) {
//				speed = slow;
//
//				float pointer_x = Input.GetAxis ("Mouse X");
//				float pointer_y = Input.GetAxis ("Mouse Y");
//			
//				if (Input.touchCount > 0) {
//					pointer_x = Input.touches [0].deltaPosition.x;
//					pointer_y = Input.touches [0].deltaPosition.y;
//				}
//				previousPos = transform.position.x;
//				transform.Translate (pointer_x * touchStrafeSpeed, 
//					0f, 0f);
//
//				if (transform.localPosition.x < minBatPosX)
//					transform.localPosition = new Vector2 (minBatPosX, transform.localPosition.y);
//				if (transform.localPosition.x > maxBatPosX)
//					transform.localPosition = new Vector2 (maxBatPosX, transform.localPosition.y);
//
//				currentPos = transform.position.x;
//				velocity = (currentPos - previousPos) / Time.deltaTime;
//				if (velocity > 40f || velocity < -40f) {
//					fastStrafe = true;
//				} else {
//					fastStrafe = false;
//				}
//			}
//
//		} else if (memoryControl) {
//			flying = false;
//			if(Input.GetMouseButtonDown(0)){
//				if(canMove){
//					speed = slow;
//					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
//					point = ray.origin + (ray.direction * rayDistance);
//					localPoint = transform.InverseTransformDirection(point);
//					//Za input samo ispod misa
//					if(localPoint.y >= memoryYLimit){
//						transform.localPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y + App.builder.tileHeight/2);
//						memoryYLimit += App.builder.tileHeight / 2;
//					}
//					else if(localPoint.x >= memoryXLimit){
//						transform.localPosition = new Vector2 (transform.localPosition.x + App.builder.tileHeight/2, transform.localPosition.y);
//
//					}
//					else if(localPoint.x < memoryXLimit){
//						transform.localPosition = new Vector2 (transform.localPosition.x - App.builder.tileHeight/2, transform.localPosition.y);
//
//					}
//
//					Debug.Log( "World point " + localPoint.ToString());		
//				}
//			}
//		}
//		else {
//			
//			if(Input.GetMouseButton(0)){
//				if(canMove){
//					speed = slow;
//					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
//					point = ray.origin + (ray.direction * rayDistance);
//					localPoint = transform.InverseTransformDirection(point);
//					//Za input samo ispod misa
//					if(localPoint.y < transform.position.y)
//						transform.position = new Vector3(localPoint.x, transform.position.y, transform.position.z);
//					//Debug.Log( "World point " + point);		
//				}
//			}
//

//		}
//
//		if (Input.GetMouseButtonUp (0))
//			speed = fast;
//
//		if (flying) {
//			transform.Translate (Vector3.up * speed);
//			if (speed != fast) {
//				App.score.ScorePlus (0.03f, "distance");
//				scoreCounter += 0.03f;
//			} else {
//				App.score.ScorePlus (0.05f * 3, "distance");
//				scoreCounter += 0.05f * 3;
//
//			}
//		}

	}

	public void InWeb(){
		flying = false;
		inWeb = true;
		canMove = false;
		Invoke ("OutOfWeb", inWebTime);
	}

	public void OutOfWeb(){
		
		inWeb = false;
		canMove = true;
		Destroy (webObject);
		Invoke ("FlyOut", 1f);
	}

	public void FlyOut(){
		flying = true;
	}

	public void DecreaseEnergy(float amount){
		if (energy - amount < 0f) {
			energy = 0f;
		} else {
			energy -= amount;
		}
		//energyFillSprite.fillAmount = energy/100f;
		StartCoroutine (EnergyChange(energy/100f, energyChangeTime));

	}

	public void IncreaseEnergy(float amount){
		if (energy + amount >= 100f) {
			energy = 100f;
		} else {
			energy += amount;
		}
		StartCoroutine (EnergyChange(energy/100f, energyChangeTime));
	}

	public IEnumerator EnergyChange(float targetValue, float time){

		float startValue = energyFillSprite.fillAmount;
		for (float i = 0f; i <= time;) {
			i += Time.deltaTime;
			float currentTime = Mathf.Min(i / time, 1f);
			energyFillSprite.fillAmount = EasingFunction.EaseInOutQuad (startValue, targetValue, currentTime);
			yield return null;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Obstacle") {
//			ObstacleHit ();
		} else if (coll.tag == "Loot") {
			Destroy (coll.gameObject);
			IncreaseEnergy (lootEnergyIncrease);
//			Debug.Log ("LOOT");

		}
		else if(coll.gameObject.tag == "Web"){
			webObject = coll.gameObject;

			if (speed == fast || fastStrafe) {
				DestroyWeb ();
			} else {
				InWeb ();
			}
		}
	}

	public void DestroyWeb(){
		flying = true;
		inWeb = false;
		canMove = true;
		Destroy (webObject);
	}

	public void GetOut(){
		
	}

	void OnGUI()
	{
		GUI.Label(new Rect(200,0, 30,30),""+isHit,textStyle);
	}

	public void ObstacleHit(){
		isHit = "HIT!";
		Invoke ("SetHitLabelOff", 1f);
		ParticleEffect ();
		App.game.InvokeGameOver ();
	}
		

	public void SetHitLabelOff(){
		isHit = "";
	}

	public IEnumerator LerpMovementRight(Vector3 startPos, Vector3 endPos){

		lerpFinished = false;
		float startX = startPos.x;
		float endX = endPos.x;

		for(float i = startX; i < endX; i += 0.05f){
			transform.position = new Vector3(i, startPos.y, startPos.z);
			yield return null;
		}

		lerpFinished = true;
	}

	public IEnumerator LerpMovementLeft(Vector3 startPos, Vector3 endPos){

		lerpFinished = false;
		float startX = startPos.x;
		float endX = endPos.x;

		for(float i = startX; i > endX; i -= 0.05f){
			transform.position = new Vector3(i, startPos.y, startPos.z);
			yield return null;
		}

		lerpFinished = true;
	}

	public void ParticleEffect(){
		StartCoroutine(App.camShake.Shake (0.4f,0.3f));
		particleParent.SetParent (null);
		particleParent.gameObject.SetActive (true);
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		playerObject.SetActive (false);
		foreach (Transform child in particleParent) {
			child.GetComponent<Rigidbody2D> ().AddForce (new Vector2(Random.Range(0f, 1f) * particleSpeed, Random.Range(0f,1f) * particleSpeed), ForceMode2D.Impulse);
		}
		Invoke ("DisableParticles", 1.5f);
	}

	public void DisableParticles(){
		particleParent.SetParent (transform);
		particleParent.transform.localPosition = Vector2.zero;
		particleParent.gameObject.SetActive (false);
		foreach (Transform child in particleParent) {
			child.transform.localPosition = Vector3.zero;
			child.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		}
	}
}
