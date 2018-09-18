using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Gameplay3D : MonoBehaviour {

	public float spawnX;
	public float spawnY;

	public float obstacleSpeed;

	public int obstacleNum;
	public int maxObstacleNum;

	public int startBigChance;
	public int startMediumChance;
	public int startSmallChance;
	public int startMiniChance;

	public int bigChance;
	public int mediumChance;
	public int smallChance;
	public int miniChance;

	public GameObject spawnTrigger;
	public List<GameObject> obstacleList;
	public GameObject obstacles;

	public Vector3 firstMousePos;
	public Vector3 newMousePos;
	public float xDiff;
	public float yDiff;

	public bool clicked;
	public float moveFactor;
	public float xDiffScaled;
	public float yDiffScaled;

	public float newPositionX;
	public float newPositionY;

	public GameObject bat;

	public float spawnDistance;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("SpawnObstacles", 0f, 2f);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.S)) {
			NewSpawnTest ();
		}

		if (clicked) {
			newMousePos = Input.mousePosition;
			xDiff = newMousePos.x - firstMousePos.x;
			yDiff = newMousePos.y - firstMousePos.y;

			xDiffScaled = xDiff / moveFactor;
			yDiffScaled = yDiff / moveFactor;



			if (transform.position.x + xDiffScaled < -0.07f)
				xDiffScaled = -0.07f;
			else if (xDiffScaled > 0.07f)
				xDiffScaled = 0.07f;

			if (yDiffScaled < -0.15f)
				yDiffScaled = -0.15f;
			else if (yDiffScaled > 0.15f)
				yDiffScaled = 0.15f;

			transform.position = new Vector3 (transform.position.x + xDiffScaled, transform.position.y + yDiffScaled, transform.position.z);
			if (transform.position.x < -0.07f) {
				transform.position = new Vector3 (-0.07f, transform.position.y + yDiffScaled, transform.position.z);
			}
			if (transform.position.x > 0.07f) {
				transform.position = new Vector3 (0.07f, transform.position.y + yDiffScaled, transform.position.z);
			}
			if (transform.position.y < -0.7f) {
				transform.position = new Vector3 (transform.position.x, -0.72f, transform.position.z);
			}
			if (transform.position.y > -0.45f) {
				transform.position = new Vector3 (transform.position.x, -0.45f, transform.position.z);
			}
			firstMousePos = newMousePos;
		}

		if (Input.GetMouseButtonDown (0)) {
			clicked = true;
			firstMousePos = Input.mousePosition;

		}
		if(Input.GetMouseButtonUp(0))
			clicked = false;
		
		obstacles.transform.position = new Vector3 (obstacles.transform.position.x, obstacles.transform.position.y, obstacles.transform.position.z - obstacleSpeed/10f);
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + obstacleSpeed);
	}

	void OnCollisionEnter(Collision coll){
		Debug.LogError("Kolizija");

	}

	public void SpawnObstacles(){
		if (obstacleNum > maxObstacleNum)
			obstacleNum = maxObstacleNum;
		int obstacleNumber = Random.Range (20, obstacleNum);
		//int obstacleNumber = 1;
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

			float xOffset = Random.Range (-spawnX, spawnX);
			float yOffset = Random.Range (-spawnY, spawnY);
			Vector3 spawnPos = new Vector3 (spawnTrigger.transform.position.x + xOffset, spawnTrigger.transform.position.y + yOffset, spawnTrigger.transform.position.z);

			//Vector3 spawnPos2 = new Vector3 (transform.position.x, transform.position.y, spawnTrigger.transform.position.z);
			//Debug.Log (spawnPos2.ToString());

			GameObject obs = Instantiate (obstacleList[obstacleIndex], spawnPos, Quaternion.identity) as GameObject;
			obs.transform.SetParent (obstacles.transform);

			//GameObject obs2 = Instantiate (obstacleList[obstacleIndex], spawnPos2, Quaternion.identity) as GameObject;
			//Debug.LogError ("stahp");
			obs.GetComponent<Rigidbody> ().AddForce (Vector3.back * obstacleSpeed, ForceMode.Force);
		}
	}

	public void NewSpawnTest(){
		Vector3 playerPos = bat.transform.position;
		Vector3 spawnPos = new Vector3 (playerPos.x, playerPos.y, playerPos.z + spawnDistance);
		GameObject obs = Instantiate (obstacleList[0], spawnPos, Quaternion.identity) as GameObject;
		//obs.transform.SetParent (obstacles.transform);

	}
}
