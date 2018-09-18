using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pokega;

public class RecordedGameplay : MonoBehaviour{

	public List<int> obstacleNumbers;
	public List<int> obstacleIndexes;
	public List<Vector3> spawnPositions;
	public List<Vector3> playerPositions;

	public int j=0;
	public int m=0;

	public MPlayer1 mp1;
	public bool canSpawn = true;

	void Start(){
		
	}

	public void ResetArrays(){
		obstacleNumbers = new List<int> ();
		obstacleIndexes = new List<int> ();
		spawnPositions = new List<Vector3>();
		playerPositions = new List<Vector3>();

	}

	void Update(){
//		if (Input.GetKeyDown (KeyCode.M)) {
//			InvokeRepeating ("SpawnObstacles", 1f, 2f);
//			App.ai.canMove = true;
//		}

		if (Input.GetMouseButtonDown (0) && App.game.currentGameState == Game.GameState.MULTIPLAYER && canSpawn) {
			Debug.Log ("ONLY ONCE");

			InvokeRepeating ("SpawnObstacles", 1f, 2f);
			App.ai.canMove = true;
			App.game.tapToStartLabel.gameObject.SetActive (false);
			App.gameplay.StartCounting ();
			canSpawn = false;


			//DODATAK ZA NASTAVAK MULTIPLAYERA KAD POGINE P2
			App.game.StartIncreasingDifficulty();
			App.ai.lives = 3;
		}
	}

	public void SaveRecording(){

		string obsNum = "";
		foreach(int n in obstacleNumbers)
			obsNum += n.ToString() + ";";

		string obsInd= "";
		foreach(int n in obstacleIndexes)
			obsInd += n.ToString() + ";";

		string spawnPos= "";
		foreach(Vector3 n in spawnPositions)
			spawnPos += n.x.ToString() + "." + n.y.ToString() + ";";

		string playPos= "";
		foreach(Vector3 n in playerPositions)
			playPos += n.x.ToString() + "." + n.y.ToString() + ";";

		PlayerPrefs.SetString("obstacleNumbers", obsNum);
		PlayerPrefs.SetString("obstacleIndexes", obsInd);
		PlayerPrefs.SetString("spawnPositions", spawnPos);
		PlayerPrefs.SetString("playerPositions",playPos);
		PlayerPrefs.Save ();
	}

	public void StartSpawningObstacles(){
		InvokeRepeating ("SpawnObstacles", 1f, 2f);

	}

	public void StartSpawningObstacles2(){
		InvokeRepeating ("SpawnObstacles2", 1f, 2f);

	}

	public void SpawnObstacles(){

		for(int i=0; i<obstacleNumbers[j]; i++, m++){

			//Vector3 spawnPos2 = new Vector3 (spawnTrigger.transform.position.x + xOffset - 0.15f, spawnTrigger.transform.position.y + yOffset -0.15f);

			GameObject obs = Instantiate (App.gameplay.obstacleList[obstacleIndexes[m]], spawnPositions[m], Quaternion.identity) as GameObject;
			obs.transform.SetParent (App.gameplay.obstacles.transform);
		}
		j++;
	}

	public void SpawnObstacles2(){


		int obstacleNumber = 5;

		for(int i=0; i<obstacleNumber; i++){

			GameObject obs = Instantiate (mp1.obstacleList[j], mp1.obstaclePositions[j], Quaternion.identity) as GameObject;
			obs.transform.SetParent (mp1.obstacles.transform);
			j++;
		}

	}
		
}
