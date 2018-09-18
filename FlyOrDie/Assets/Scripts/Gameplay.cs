using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pokega{

	public class Gameplay : MonoBehaviour {

		public List<GameObject> obstacleList;
		public GameObject bat;
		public GameObject obstacles;
		public GameObject sonar;
		public GameObject gameOverLight;
		public GameObject spawnTrigger;
		public GameObject particles;
		public GameObject particleObject;
		public GameObject batBody;
		public GameObject coinPrefab;

		public float particleSpeed;
		public float obstacleSpawnTime;
		public float obstacleSpeed;



		public int startBigChance;
		public int startMediumChance;
		public int startSmallChance;
		public int startMiniChance;

		public int bigChance;
		public int mediumChance;
		public int smallChance;
		public int miniChance;

		public int obstacleNum;
		public int startObstacleNum;
		public int maxObstacleNum;

		public int lives;
		public bool canMove;

		public Vector3 obstacleStartPos;
		float meterCounter;

		public GameObject lifeSprite1;
		public GameObject lifeSprite2;
		public GameObject lifeSprite3;

		public int blinkCounter;
		public string coinsCount;
		public string uid;

		public RecordedGameplay recorded;
		public bool recording = false;
		public bool canSpawn = true;
		public bool startOnlyOnce = true;


		public GameObject pray;
		public float prayZPosition;

		public GameObject movingPray;
		public GameObject attackingPray;

		public GameObject allMovingPray;
		public GameObject allAttackingPray;

		public float attackingPrayMovingSpeed;

		public float prayMovingSpeed;

		public int normalPrayChance;
		public int movingPrayChance;
		public int attackingPrayChance;

		public float praySpawnTime;
		public float prayStartSpawning;
		public float movingPraySpawnTime;
		public float movingPrayStartSpawning;
		public float attackingPraySpawnTime;
		public float attackingPrayStartSpawning;


		// Use this for initialization
		void Start () {
			obstacleStartPos = obstacles.transform.position;
		}



		public void StartSpawningObstacles(){
			if (canSpawn) {
				Debug.Log ("Start Spawning obstacles");
				InvokeRepeating ("SpawnObstacles", 1f, obstacleSpawnTime);
				//InvokeRepeating ("SpawnPray", prayStartSpawning, praySpawnTime);
				//InvokeRepeating ("SpawnMovingPray", movingPrayStartSpawning, movingPraySpawnTime);
				//InvokeRepeating ("SpawnAttackingPray", attackingPrayStartSpawning, attackingPraySpawnTime);

				canSpawn = false;
			}

		}

		public void SetChances(){
			obstacleNum = startObstacleNum;
			bigChance = startBigChance;
			mediumChance = startMediumChance;
			smallChance = startSmallChance;
			miniChance = startMiniChance;
		}


		public void DistancePlus(){
			App.score.ScorePlus (1f, "distance");
		}
		
		// Update is called once per frame
		void Update () {



			if (Input.GetMouseButtonDown (0) && App.game.currentGameState == Game.GameState.START_GAME && startOnlyOnce) {
				recording = true;
				App.recorded.ResetArrays ();
				StartSpawningObstacles ();
				App.game.StartIncreasingDifficulty ();
				App.game.tapToStartLabel.gameObject.SetActive (false);
				App.gameplay.StartCounting ();
				startOnlyOnce = false;
				Debug.Log ("Start Spawning and start game");
			}

			if (Input.GetMouseButton (0)) {
				if (canMove) {
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					RaycastHit hit;
					if (Physics.Raycast (ray, out hit)) {
						if (hit.collider.name == "MovePlane") {
							bat.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, bat.transform.position.y, bat.transform.position.z);
						}
					}
				}
			
			}
			obstacles.transform.position = new Vector3 (obstacles.transform.position.x, obstacles.transform.position.y - obstacleSpeed * Time.deltaTime, obstacles.transform.position.z);
			allMovingPray.transform.position = new Vector3 (allMovingPray.transform.position.x, allMovingPray.transform.position.y - prayMovingSpeed * Time.deltaTime, allMovingPray.transform.position.z);
			allAttackingPray.transform.position = new Vector3 (allAttackingPray.transform.position.x, allAttackingPray.transform.position.y - attackingPrayMovingSpeed * Time.deltaTime, allAttackingPray.transform.position.z);

			if(recording)
				recorded.playerPositions.Add (bat.transform.position);
		}

		void OnCollisionEnter(Collision coll){
			//sonar.GetComponent<Sonar> ().SonarOff ();
			LivesMinus ();
			gameObject.GetComponent<BoxCollider> ().enabled = false;

	//		GameObject par = Instantiate (particleObject, transform.position, Quaternion.identity) as GameObject;
	//		foreach (Transform p in par.transform) {
	//			p.parent = null;
	//			p.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * particleSpeed, ForceMode.Impulse);
	//		}


			//gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
		}

		void OnTriggerEnter(Collider coll){
			if (coll.tag == "coin") {
				//Debug.Log ("AJMO");
			}
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

		public void StartCounting(){
			InvokeRepeating ("DistancePlus", 0f, 0.5f);
		}

		void SlowTime(){
			obstacleSpeed = 0.0001f;
			canMove = false;
		}

		public void LivesMinus(){
			lives--;
			BatHitEffect ();
			SetLifeSprites ();
		}

		public void SetLifeSprites(){

			if (lives == 3) {
				lifeSprite1.SetActive (true);
				lifeSprite2.SetActive (true);
				lifeSprite3.SetActive (true);

			}

			if (lives == 2) {
				lifeSprite3.SetActive (false);
				lifeSprite1.SetActive (true);
				lifeSprite2.SetActive (true);
			}
			else if (lives == 1) {
				lifeSprite3.SetActive (false);
				lifeSprite2.SetActive (false);
				lifeSprite1.SetActive (true);

			}
			else if (lives == 0) {
				lifeSprite1.SetActive (false);
				lifeSprite2.SetActive (false);
				lifeSprite3.SetActive (false);

				if(App.game.currentGameState == Game.GameState.START_GAME)
					App.game.GameOver ();

				if (App.game.currentGameState == Game.GameState.MULTIPLAYER)
					App.game.GameOverMultiplayer ();
			}
		
		}

		public void BatHitEffect(){
			//SlowTime ();
			//ShowLights ();
			gameObject.GetComponent<Collider> ().enabled = false;
			BlinkOn ();

			//Invoke ("Restart", 1f*Time.timeScale);
		}

		void ShowLights(){
			gameOverLight.SetActive (true);

		}

		void Restart(){
			
			canMove = true;
			CancelInvoke ();
			BlinkOn ();

			obstacleSpeed = 0.01f;
			Time.timeScale = 1f;
			sonar.GetComponent<Sonar> ().SonarOn ();
			gameOverLight.SetActive (false);
			InvokeRepeating ("SpawnObstacles", 1f, 2f);
			StartCounting ();

			//obstacles.transform.position = obstacleStartPos;

		}

		public void DestroyObstacles(){
			foreach (Transform obs in obstacles.transform) {
				Destroy (obs.gameObject);
			}
		}

		public void SpawnObstacles(){

			if (obstacleNum > maxObstacleNum)
				obstacleNum = maxObstacleNum;
			int obstacleNumber = Random.Range (5, obstacleNum);

			recorded.obstacleNumbers.Add (obstacleNumber);

			for(int i=0; i<obstacleNumber; i++){
				int rand = Random.Range (1, 100);
				int obstacleIndex;

				if (rand < bigChance)
					obstacleIndex = 0;
				else if (rand > bigChance && rand < mediumChance)
					obstacleIndex = 1;
				else if (rand > mediumChance && rand < miniChance)
					obstacleIndex = 2;
				else
					obstacleIndex = 3;

				recorded.obstacleIndexes.Add (obstacleIndex);

				float xOffset = Random.Range (-0.5f, 0.5f);
				float yOffset = Random.Range (-0.35f, 0.35f);
				Vector3 spawnPos = new Vector3 (spawnTrigger.transform.position.x + xOffset, spawnTrigger.transform.position.y + yOffset);
				//Vector3 spawnPos2 = new Vector3 (spawnTrigger.transform.position.x + xOffset - 0.15f, spawnTrigger.transform.position.y + yOffset -0.15f);

				recorded.spawnPositions.Add (spawnPos);

				GameObject obs = Instantiate (obstacleList[obstacleIndex], spawnPos, Quaternion.identity) as GameObject;
				obs.transform.SetParent (obstacles.transform);
			}
		}

		public void SpawnPray(){

			float xOffset = Random.Range (-0.5f, 0.5f);
			float yOffset = Random.Range (-0.35f, 0.35f);
			Vector3 spawnPos = new Vector3 (spawnTrigger.transform.position.x + xOffset, spawnTrigger.transform.position.y + yOffset, prayZPosition);
			GameObject obs = Instantiate (pray, spawnPos, Quaternion.identity) as GameObject;
			obs.transform.SetParent (obstacles.transform);

		}

		public void SpawnMovingPray(){

			float xOffset2 = Random.Range (-0.5f, 0.5f);
			float yOffset2 = Random.Range (-0.35f, 0.35f);
			Vector3 spawnPos2 = new Vector3 (spawnTrigger.transform.position.x + xOffset2, spawnTrigger.transform.position.y + yOffset2, prayZPosition);
			GameObject obs1 = Instantiate (movingPray, spawnPos2, Quaternion.identity) as GameObject;
			obs1.transform.SetParent (allMovingPray.transform);
		
		}

		public void SpawnAttackingPray(){
			
			float xOffset3 = Random.Range (-0.5f, 0.5f);
			float yOffset3 = Random.Range (-0.35f, 0.35f);
			Vector3 spawnPos3 = new Vector3 (spawnTrigger.transform.position.x + xOffset3, spawnTrigger.transform.position.y + yOffset3, prayZPosition);
			GameObject obs3 = Instantiate (attackingPray, spawnPos3, Quaternion.identity) as GameObject;
			obs3.transform.SetParent (allAttackingPray.transform);

		}


		public void StopSpawnigObstacles(){
			CancelInvoke ();
		}

		public void DestroyObjects(){
			foreach (Transform t in obstacles.transform) {
				Destroy (t.gameObject);
			}
			foreach (Transform t in allMovingPray.transform) {
				Destroy (t.gameObject);
			}
			foreach (Transform t in allAttackingPray.transform) {
				Destroy (t.gameObject);
			}
		}
	}
}