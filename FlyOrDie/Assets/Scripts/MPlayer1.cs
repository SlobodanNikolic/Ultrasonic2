using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Pokega;

public class MPlayer1 : MonoBehaviour {

	public List<Vector3> obstaclePositions;
	public List<GameObject> obstacleList;
	public GameObject obstacles;
	public RecordedGameplay recorded;
	public GameObject bat;
	public bool recording;
	public float obstacleSpeed;
	int j = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
//		if (Input.GetKeyDown (KeyCode.S)) {
//			InvokeRepeating ("SpawnObstacles", 1f, 2f);
//			recording = true;
//		}

//		if (Input.GetKeyDown (KeyCode.P)) {
//			recording = false;
//		}

		if (Input.GetMouseButtonDown (0) && App.game.currentGameState == Game.GameState.MULTIPLAYER) {
			recorded.StartSpawningObstacles2 ();
			App.ai.canMove = true;
			App.game.tapToStartLabel.gameObject.SetActive (false);
		}

		if (Input.GetMouseButton (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.name == "MovePlane") {
						bat.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, bat.transform.position.y, bat.transform.position.z);
					}
				}
			

		}

		obstacles.transform.position = new Vector3 (obstacles.transform.position.x, obstacles.transform.position.y - obstacleSpeed, obstacles.transform.position.z);
		if(recording)
			recorded.playerPositions.Add (bat.transform.position);
	}

	public void SpawnObstacles(){

		int obstacleNumber = 5;

		for(int i=0; i<obstacleNumber; i++){
			
			GameObject obs = Instantiate (obstacleList[j], obstaclePositions[j], Quaternion.identity) as GameObject;
			recorded.spawnPositions.Add (obs.transform.position);
			obs.transform.SetParent (obstacles.transform);
			j++;
		}

	}
}
