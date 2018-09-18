using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pokega
{
	public class Game : MonoBehaviour 
	{

		public enum GameState {START_GAME, GAMEOVER, GET_READY, PAUSE, UNPAUSE, LEVEL_COMPLETED, RESTART, STAGES, SHOP, TUTORIAL, HOME, SAVEME, MULTIPLAYER};
		public GameState currentGameState;

		public delegate void GameAction ();
		public static event GameAction StartGameEvent;
		public static event GameAction GameOverEvent;
		public static event GameAction GetReadyEvent;
		public static event GameAction PausedEvent;
		public static event GameAction UnpausedEvent;
		public static event GameAction LevelCompletedEvent;
		public static event GameAction RestartEvent;
		public static event GameAction StagesEvent;
		public static event GameAction ShopEvent;
		public static event GameAction TutorialEvent;
		public static event GameAction HomeEvent;
		public static event GameAction SaveMeEvent;
		public static event GameAction GifSavedEvent;

		public GameObject gameObjects;
		public GameObject homeButtons;
		public GameObject gameOverButtons;
		public UILabel tapToStartLabel;

		public int obstacleIncrease;
		public int obstacleIncreaseStart;

		public float difficultyIncreaseTime;

		public bool canIncreaseDifficulty = true;
		public int difficulty;

		public GameObject objects;
		public GameObject tapToStartObject;
		public NewSonar sonar;
		public Player bat;
		public TunnelBuilder1 builder;
		public TileDestroyer destroyer;

		void Start(){
			
		}

		public void Tutorial(){
			currentGameState = GameState.TUTORIAL;
			if (TutorialEvent != null)
				TutorialEvent ();
		}

		public void GetReady()
		{
			currentGameState = GameState.GET_READY;
			if(GetReadyEvent != null)
				GetReadyEvent();
		}

		public void SaveMe(){
			currentGameState = GameState.SAVEME;
			if (SaveMeEvent != null)
				SaveMeEvent ();
		}

		public void Home(){
			
			currentGameState = GameState.HOME;
			App.ui.SetScreen ("UIHome2");
			objects.SetActive (false);
			bat.transform.position = bat.playerStartPosition;

//			App.screenSonar.ResetPosition ();
//			App.screenSonar.gameObject.SetActive (true);
//			App.screenSonar.SonarOn ();
//			App.ai.gameObject.SetActive (false);
//			currentGameState = GameState.HOME;
//			gameObjects.SetActive (false);
//			if(HomeEvent != null)
//				HomeEvent();
//
//			tapToStartLabel.gameObject.SetActive (false);
//			App.score.StopNewBestLabels ();
		}

		public void GameScreen(){
			DestroyActiveFallingTiles ();
			App.score.SetScore (0, "pray");
			App.score.SetScore (0, "distance");
			App.score.ResetScoreLabels ();
			App.bat.canMove = true;
			App.bat.energy = 100f;
			currentGameState = GameState.GET_READY;
			objects.SetActive (true);
			tapToStartObject.SetActive (true);
			bat.transform.position = bat.playerStartPosition;
			bat.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			bat.gameObject.SetActive (true);
			bat.transform.Find ("Player").gameObject.SetActive(true);
			sonar.gameObject.SetActive (true);
			builder.Build ();
			App.ui.SetScreen ("UIGame2");
			sonar.SonarOn ();
			currentGameState = GameState.START_GAME;
//			App.camShake.gameObject.transform.position = new Vector2 (11.2f, 0f);
		}
			
		public void StartFirstStage(){

			App.score.SetScore (0, "pray");
			App.score.SetScore (0, "distance");
			App.score.ResetScoreLabels ();
			App.bat.canMove = true;
			App.bat.energy = 100f;
			currentGameState = GameState.GET_READY;
			objects.SetActive (true);
			tapToStartObject.SetActive (true);
			bat.transform.position = bat.playerStartPosition;
			bat.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
			bat.gameObject.SetActive (true);
			bat.transform.Find ("Player").gameObject.SetActive(true);
			sonar.gameObject.SetActive (true);

			for(int i = 0; i < 20; i++)
				builder.PlaceObstacles ();
			App.camShake.gameObject.transform.position = new Vector2 (11.2f, 0f);

			App.ui.SetScreen ("UIGame2");

			sonar.SonarOn ();
			currentGameState = GameState.START_GAME;


		}

		public void Restart2(){
			GameScreen ();
		}
	
		public void InvokeGameOver(){
			Invoke ("GameOver2", 1.5f);
		}

		public void GameOver2(){
			App.ui.SetScreen ("UIGameOver2");
			sonar.gameObject.SetActive (false);
			bat.transform.position = bat.playerStartPosition;
			bat.flying = false;
			//Da bi prepreke pocele gde treba
			builder.yPos = 0;
			builder.tileList = new List<GameObject>(builder.helperTileList);
			currentGameState = GameState.GAMEOVER;
			sonar.CancelInvoke ();
			destroyer.helperList.Clear ();
			destroyer.tileCounter = 0;
			App.builder.DestroyLoot ();
			DestroyActiveFallingTiles ();
			App.builder.DestroyWeb ();
			App.bat.inWeb = false;
			App.score.SetBestScoreLabels ();

			App.score.SetAndSaveBestScore ();
			App.bat.scoreCounter = 0;
			App.builder.tileRows = new List<TileRow> ();
		}	

		public void StartTutorial(){
			objects.SetActive (true);
		}


		public void StartGame()
		{
			App.score.StopNewBestLabels ();

			difficulty = 0;
			tapToStartLabel.gameObject.SetActive (true);
			obstacleIncrease = obstacleIncreaseStart;

			App.gameplay.obstacleSpawnTime = 2f;
			App.gameplay.SetChances ();

			//OVO DA VRATIM

			App.screenSonar.gameObject.SetActive (false);
			App.ui.SetScreen ("UI Game");
			gameObjects.SetActive (true);
			homeButtons.SetActive (false);
			gameOverButtons.SetActive (false);
			App.gameplay.gameObject.GetComponent<BoxCollider> ().enabled = true;
			App.gameplay.canMove = true;
			//App.gameplay.StartSpawningObstacles ();
			App.sonar.SonarOn ();
			currentGameState = GameState.START_GAME;
			App.score.ResetScoreLabels ();
			App.score.SetScore (0f, "distance");
			App.score.SetScore (0f, "pray");
			App.gameplay.batBody.GetComponent<MeshRenderer> ().enabled = true;
			App.gameplay.SetLifeSprites ();

			Time.timeScale = 1f;

			if (StartGameEvent != null)
				StartGameEvent ();
			
		}
			
		public void DestroyActiveFallingTiles(){
			for (int i = 0; i<App.builder.activeFallingTiles.Count; i++) {
				App.builder.activeFallingTiles[i].GetComponent<BoxCollider2D> ().size = new Vector2(App.builder.tileColStartScale, App.builder.tileColStartScale);
				App.builder.activeFallingTiles[i].GetComponent<Tile> ().falling = false;
				App.builder.activeFallingTiles[i].GetComponent<Tile> ().isEdge = false;
				App.builder.activeFallingTiles[i].GetComponent<Rigidbody2D> ().isKinematic = true;
				App.builder.activeFallingTiles[i].GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				App.builder.activeFallingTiles[i].GetComponent<Rigidbody2D> ().angularVelocity = 0f;
				App.builder.activeFallingTiles[i].GetComponent<SpriteRenderer> ().sortingOrder = 1;
				App.builder.activeFallingTiles[i].GetComponent<SpriteRenderer> ().color = Color.white;
				App.builder.activeFallingTiles[i].transform.rotation = Quaternion.identity;
				//Za vracanje materijala
				App.builder.activeFallingTiles[i].GetComponent<SpriteRenderer> ().material = App.destroyer.maskedMaterial;
			}

			App.builder.activeFallingTiles.Clear ();
			App.builder.activeFallingTiles = new List<GameObject> ();
		}

		public void StartIncreasingDifficulty(){
			if (canIncreaseDifficulty) {
				InvokeRepeating ("IncreaseDifficulty", difficultyIncreaseTime * Time.timeScale, difficultyIncreaseTime * Time.timeScale);
				canIncreaseDifficulty = false;
			}
		}

		public void StartMultiplayer(){

			App.score.StopNewBestLabels ();


			//ZA P1 KAD P2 ODAPNE
			obstacleIncrease = obstacleIncreaseStart;

			App.gameplay.obstacleSpawnTime = 2f;
			App.gameplay.SetChances ();
			//END

			App.recorded.canSpawn = true;
			App.ai.i = 0;
			App.score.SetScore (0f, "distance");
			App.score.SetScore (0f, "pray");
			App.ai.gameObject.SetActive (true);
			App.gameplay.gameObject.GetComponent<BoxCollider> ().enabled = true;
			App.gameplay.gameObject.transform.Find ("Player").gameObject.SetActive (true);
			App.gameplay.batBody.GetComponent<MeshRenderer> ().enabled = true;
			App.gameplay.SetLifeSprites ();
			tapToStartLabel.gameObject.SetActive (true);
			App.screenSonar.gameObject.SetActive (false);
			App.ui.SetScreen ("UI Game");
			gameObjects.SetActive (true);
			homeButtons.SetActive (false);
			gameOverButtons.SetActive (false);
			App.gameplay.canMove = true;
			App.sonar.SonarOn ();
			currentGameState = GameState.MULTIPLAYER;
			App.gameplay.SetLifeSprites ();
			if (StartGameEvent != null)
				StartGameEvent ();
		
		}

		public void GameOverMultiplayer(){	

			difficulty = 0;
			gameOverButtons.SetActive (true);
			App.gameplay.lives = 3;
			App.gameplay.SetLifeSprites ();
			App.gameplay.StopSpawnigObstacles ();
			App.gameplay.DestroyObstacles ();
			App.sonar.SonarOff ();
			App.gameplay.obstacleSpeed = 0.01f;
			App.gameplay.gameOverLight.SetActive (false);
			gameObjects.SetActive (false);
			App.ui.SetScreen ("UI GameOver");
			App.screenSonar.gameObject.SetActive (true);
			App.screenSonar.SonarOn ();
			currentGameState = GameState.GAMEOVER;
			App.gameplay.recording = false;
			App.recorded.SaveRecording ();
			canIncreaseDifficulty = true;
			App.gameplay.startOnlyOnce = true;
			App.gameplay.canSpawn = true;
			App.gameplay.CancelInvoke ();
			CancelInvoke ();
			App.gameplay.DestroyObjects ();
			App.score.SetBestScoreLabels ();
			App.score.SetScoreLabels ();
			App.score.SetAndSaveBestScore ();
			App.ai.gameObject.SetActive (false);

		}

		public void IncreaseDifficulty(){


			Debug.Log ("Difficulty increased");
			obstacleIncrease--;
			if (obstacleIncrease == 0) {
				App.gameplay.obstacleNum++;

				//OVO DA VRATIM
//				if (App.gameplay.obstacleSpawnTime >= 1f)
//					App.gameplay.obstacleSpawnTime -= 0.02f;
				obstacleIncrease = obstacleIncreaseStart;
			}
			App.gameplay.mediumChance += 1;
			App.gameplay.smallChance += 1;

		}

		public void Paused()
		{
			currentGameState = GameState.PAUSE;
			if(PausedEvent != null)
				PausedEvent();
		}

		public void Unpaused()
		{
			currentGameState = GameState.UNPAUSE;
			UnpausedEvent();
		}

		public void GameOver()
		{
			difficulty = 0;
			gameOverButtons.SetActive (true);
			App.gameplay.lives = 3;
			App.gameplay.SetLifeSprites ();
			App.gameplay.StopSpawnigObstacles ();
			App.gameplay.DestroyObstacles ();
			App.sonar.SonarOff ();
			App.gameplay.obstacleSpeed = 0.01f;
			App.gameplay.gameOverLight.SetActive (false);
			gameObjects.SetActive (false);
			App.ui.SetScreen ("UI GameOver");
			App.screenSonar.gameObject.SetActive (true);
			App.screenSonar.SonarOn ();
			currentGameState = GameState.GAMEOVER;
			App.gameplay.recording = false;
			App.recorded.SaveRecording ();
			canIncreaseDifficulty = true;
			App.gameplay.startOnlyOnce = true;
			App.gameplay.canSpawn = true;
			App.gameplay.CancelInvoke ();
			CancelInvoke ();
			App.gameplay.DestroyObjects ();
			App.score.SetBestScoreLabels ();
			App.score.SetScoreLabels ();
			App.score.SetAndSaveBestScore ();

		}

		public void LevelCompleted()
		{
			currentGameState = GameState.LEVEL_COMPLETED;
			LevelCompletedEvent();
		}

		public void Restart()
		{
			App.score.StopNewBestLabels ();

			currentGameState = GameState.RESTART;
			RestartEvent();

		}

		public void Stages()
		{
			currentGameState = GameState.STAGES;
			if(StagesEvent != null)
				StagesEvent();

		}

		public void Shop()
		{
			currentGameState = GameState.SHOP;
			if(ShopEvent != null)
				ShopEvent();

		}

		public void SetState(string state){
			Invoke(state, 0f);
		}

		public void RemoveBats(){
			
		}
	}
}
