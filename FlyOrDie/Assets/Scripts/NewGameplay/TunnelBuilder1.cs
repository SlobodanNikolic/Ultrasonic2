using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokega;

public class TunnelBuilder1 : MonoBehaviour {

	public GameObject tile;
	public float tileHeight;
	public List<GameObject> tileList;
	public List<GameObject> helperTileList;
	public float startY1 = -1f;
	public float startX1 = -0.5f;
	public int tunnelWidth;
	public Vector3 dumpsterPosition;
	public List<GameObject> inactiveTiles;
	public List<List<GameObject>> tileMap;
	public int maxTilesInRow;

	public float minX;
	public float maxX;
	public int minTunnelWidth;
	public int maxTunnelWidth;
	public int fieldOfViewWidth;
	public int startOfLastHole = 3;
	public int endOfLastHole = 6;
	public int yPos = 0;
	public int holeLength;
	public int newTunnelStart;
	public bool tunnelFinished = true;
	public bool highTunnel;

	public GameObject lootPrefab;
	public List<GameObject> activeLoot = new List<GameObject>();
	public List<GameObject> activeFallingTiles = new List<GameObject>();
	public List<GameObject> activeWeb = new List<GameObject>();

	public GameObject spiderWebPrefab;
	public int spiderWebChance;

	//Za prvi stage
	public float obstacleStartX;
	public float currObsY;
	public int obstacleY;
	public int obstacleYPlus;
	public List<GameObject> obstacleList;
	public List<GameObject> helperObstacleList;
	public int currentObsIndex = 0;
	public int curObsHorizontalIndex = 0;

	public List<GameObject> comboList;
	public List<GameObject> treeList;
	public float treeChance;

	public int previousHoleStart;
	public int currentHoleStart;
	public GameObject shopPrefab;

	public float tileColStartScale;
	[SerializeField]
	public List<TileRow> tileRows = new List<TileRow>();



	public static void Shuffle(List<GameObject> list)  
	{  
		
		int n = list.Count;  
		while (n > 1) {  
			n--;
			int k = Random.Range(0, n + 1);  
			GameObject value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}

	// Use this for initialization
	void Start () {
		Shuffle (tileList);
		helperTileList = new List<GameObject> (tileList);
		tileHeight = tile.GetComponent<SpriteRenderer> ().bounds.size.x;
		startX1 = -0.5f - tileHeight/2;
		fieldOfViewWidth = (int)((1f / tileHeight) + 1)*9;
		//ArrangeTiles (tileList, startX1, startY1);
		//DigTunnel (4, 0, tileMap);

//		DestroyInactive ();
	}

	public void Build(){
		DigTunnels(tileList);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DigTunnels(List<GameObject> tileList){
		//DigBetterWiderRandomTunnel(tileList);
		DigBetterRandomTunnel(tileList);
		tileList.Clear ();
	}

	public void ArrangeTiles(List<GameObject> tileL, float startX, float startY){
		tileMap = new List<List<GameObject>> (tileL.Count / maxTilesInRow);

		for (int j = 0; j < tileL.Count / maxTilesInRow; j++) {
			tileMap.Add(new List<GameObject> (maxTilesInRow));
			for (int i = 0; i < maxTilesInRow; i++) {
				tileL [j*maxTilesInRow + i].transform.localPosition = new Vector3 (startX + i * tileHeight, startY + j*tileHeight, 0f);
				tileMap [j].Add (tileL[j*maxTilesInRow + i]);
			}
		}
	}

	public void DestroyLoot(){
		foreach(GameObject lootObj in activeLoot){
			if (lootObj != null)
				Destroy (lootObj);
		}
		activeLoot.Clear ();
		activeLoot = new List<GameObject> ();
	}

	public void DestroyWeb(){
		foreach(GameObject lootObj in activeWeb){
			if (lootObj != null)
				Destroy (lootObj);
		}
		activeWeb.Clear ();
		activeWeb = new List<GameObject> ();
	}

	public void DisableActiveFallingTiles(){
		foreach(GameObject tile in activeFallingTiles){
			if (tile != null) {
				tile.GetComponent<Tile> ().isEdge = false;
				tile.GetComponent<Rigidbody2D> ().isKinematic = true;
				tile.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				tile.gameObject.GetComponent<Rigidbody2D> ().angularVelocity = 0f;
				tile.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;
				tile.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;

				tile.transform.rotation = Quaternion.identity;
			}
		}
		activeFallingTiles.Clear ();
		activeFallingTiles = new List<GameObject> ();
	}

	public void DestroyInactive(){
		foreach (GameObject obj in inactiveTiles) {
			Debug.Log ("Tile Destroyed - " + obj.name);
			Destroy (obj);
		}
	}


//	public void PlaceObstacles(List<GameObject> obstacleList){
////		for (int i = 0; i < fieldOfViewWidth; i++) {
////			obstacleList [i+currentObsIndex].transform.position = new Vector2 (obstacleStartX + i*tileHeight, obstacleY);
////			currentObsIndex++;
////		}
////
//		int randomDestroyIndex = Random.Range (0, fieldOfViewWidth);
//		for (curObsHorizontalIndex = curObsHorizontalIndex; curObsHorizontalIndex < randomDestroyIndex; curObsHorizontalIndex++) {
//			if (currentObsIndex == obstacleList.Count)
//				return;
//			Debug.Log ("ObstaclePlaced");
//			obstacleList[currentObsIndex].transform.position = new Vector2 (obstacleStartX + curObsHorizontalIndex*tileHeight, obstacleY);
//			currentObsIndex++;
//		}
//
//		curObsHorizontalIndex++;
//
//		for (curObsHorizontalIndex = curObsHorizontalIndex; curObsHorizontalIndex < fieldOfViewWidth; curObsHorizontalIndex++) {
//			if (currentObsIndex == obstacleList.Count)
//				return;
//			obstacleList[currentObsIndex].transform.position = new Vector2 (obstacleStartX + curObsHorizontalIndex*tileHeight, obstacleY);
//			currentObsIndex++;
//		}
//
//		curObsHorizontalIndex = 0;
//		//Dodaj na Y, da bi sledeci red bio iznad
//		obstacleY += obstacleYPlus;
//	}

	public void PlaceObstacles(){
		float shouldPlaceTree = Random.Range (0f, 100f);
		if (shouldPlaceTree < treeChance) {
			int leftOrRight = Random.Range (0, 3);
			//Postavi drvo levo
			if (leftOrRight == 0) {
				int randomTreeIndex = Random.Range (0, treeList.Count);
				GameObject obs = Instantiate (treeList [randomTreeIndex], new Vector3 (obstacleStartX, currObsY, 0f), Quaternion.identity) as GameObject;
				int randomComboIndex = Random.Range (0, comboList.Count);
				GameObject obs2 = Instantiate (comboList [randomComboIndex], new Vector3 (obstacleStartX + 3 * tileHeight, currObsY, 0f), Quaternion.identity) as GameObject;
				int randomComboIndex2 = Random.Range (0, comboList.Count);
				GameObject obs3 = Instantiate (comboList [randomComboIndex2], new Vector3 (obstacleStartX + 6 * tileHeight, currObsY, 0f), Quaternion.identity) as GameObject;

				currObsY += 5 * tileHeight;
			}
			//Postavi drvo desno
			else if (leftOrRight == 1) {
				int randomTreeIndex = Random.Range (0, treeList.Count);
				GameObject obs = Instantiate (treeList [randomTreeIndex], new Vector3 (obstacleStartX + 6 * tileHeight, currObsY, 0f), Quaternion.identity) as GameObject;
				obs.transform.Rotate (Vector3.up, 180f);
				int randomComboIndex = Random.Range (0, comboList.Count);
				GameObject obs2 = Instantiate (comboList [randomComboIndex], new Vector3 (obstacleStartX, currObsY, 0f), Quaternion.identity) as GameObject;
				int randomComboIndex2 = Random.Range (0, comboList.Count);
				GameObject obs3 = Instantiate (comboList [randomComboIndex2], new Vector3 (obstacleStartX + 3 * tileHeight, currObsY, 0f), Quaternion.identity) as GameObject;

				currObsY += 5 * tileHeight;
			}
			//Postavi na obe strane
			else {
				int randomTreeIndex = Random.Range (0, treeList.Count);
				GameObject obs = Instantiate (treeList [randomTreeIndex], new Vector3 (obstacleStartX, currObsY, 0f), Quaternion.identity) as GameObject;
				int randomComboIndex = Random.Range (0, comboList.Count);
				GameObject obs2 = Instantiate (comboList [randomComboIndex], new Vector3 (obstacleStartX + 3 * tileHeight, currObsY, 0f), Quaternion.identity) as GameObject;
				int randomTreeIndex2 = Random.Range (0, treeList.Count);
				GameObject obs4 = Instantiate (treeList [randomTreeIndex2], new Vector3 (obstacleStartX + 6 * tileHeight, currObsY, 0f), Quaternion.identity) as GameObject;
				obs4.transform.Rotate (Vector3.up, 180f);
				currObsY += 5 * tileHeight;
			}
		} else {
			int randomComboIndex = Random.Range (0, comboList.Count);
			GameObject obs2 = Instantiate (comboList [randomComboIndex], new Vector3 (obstacleStartX + 3 * tileHeight, currObsY, 0f), Quaternion.identity) as GameObject;
			int randomComboIndex2 = Random.Range (0, comboList.Count);
			GameObject obs3 = Instantiate (comboList [randomComboIndex2], new Vector3 (obstacleStartX + 6 * tileHeight, currObsY, 0f), Quaternion.identity) as GameObject;
			int randomComboIndex3 = Random.Range (0, comboList.Count);
			GameObject obs4 = Instantiate (comboList [randomComboIndex3], new Vector3 (obstacleStartX, currObsY, 0f), Quaternion.identity) as GameObject;
			currObsY += 5 * tileHeight;
		}
	}

	public void DigBetterRandomTunnel(List<GameObject> tileList){
		//Dodajemo prvi red u tileList
		tileRows.Add (new TileRow());

		int counter = 0;

		while (true) {
			if (tunnelFinished) {
				tunnelWidth = Random.Range (minTunnelWidth, maxTunnelWidth);
				//Nadji najmanju pocetnu tacku za tunel, ako je manja od 0, onda je 0 - moze da pocinje desno max na endOfLastHole
				//Ako desno umesto endOfLastHole stavimo endOfLastHole + 1 - skroz druga prica!
				newTunnelStart = Random.Range (Mathf.Max (1, startOfLastHole - (tunnelWidth -1 )), Mathf.Min (fieldOfViewWidth, endOfLastHole));
			}
			tunnelFinished = false;

			if (App.bat.scoreCounter >= App.bat.shopScoreLimit) {
//				if (newTunnelStart - previousHoleStart >= 2) {
//					Debug.Log ("Can place shop");
				GameObject shop = Instantiate (shopPrefab, new Vector2 (minX + tileHeight/2, startY1 + yPos * tileHeight - 0.5f * tileHeight), Quaternion.identity) as GameObject;
					App.bat.scoreCounter = 0f;
//				}
			}
			previousHoleStart = newTunnelStart;

			for (int i = 0; i < newTunnelStart; i++) {
				//iscrtaj zid od 0 do pocetne tacke
				if (counter >= tileList.Count)
					return;
				holeLength++;
				tileList [counter].transform.position = new Vector2 (minX + i * tileHeight, startY1 + yPos*tileHeight);

				//Dodajemo element u listu boolova
				tileList[counter].GetComponent<Tile>().xIndex = i;
				tileList [counter].GetComponent<Tile> ().yIndex = yPos;
				tileRows [yPos].row [i] = true;

				counter++;
				if (highTunnel) {
					yPos++;
					tileList [counter].transform.position = new Vector2 (minX + i * tileHeight, startY1 + yPos * tileHeight);
					counter++;
					yPos--;
				}
					
			}
				
			holeLength = 0;

			//Za tajlove koji padaju, egde tajlovi
			if (counter >= tileList.Count)
				return;
			tileList [counter - 1].GetComponent<Tile> ().isEdge = true;
			tileList [counter].GetComponent<Tile> ().isEdge = true;


			//Za loot
			if (tunnelWidth > minTunnelWidth + 1) {
				int randomLootOffset = Random.Range (1, tunnelWidth);
				Vector3 lootPos = tileList [counter - 1].transform.position + new Vector3 (randomLootOffset * tileHeight, 0f, 0f);
				GameObject loot = Instantiate (lootPrefab, lootPos, Quaternion.identity) as GameObject;
				activeLoot.Add (loot);
			
				//Za web
				int randomWebChance = Random.Range(0, 100);
				if (randomWebChance < spiderWebChance) {
					randomLootOffset = Random.Range (1, tunnelWidth);
					lootPos = tileList [counter - 1].transform.position + new Vector3 (randomLootOffset * tileHeight, 0f, 0f);
					GameObject web = Instantiate (spiderWebPrefab, lootPos, Quaternion.identity) as GameObject;
					activeWeb.Add (web);
				}
			}




			for (int i = newTunnelStart + tunnelWidth; i < fieldOfViewWidth; i++) {
				
				//iscrtaj zid od krajnje tacke do kraja
				//Debug.Log("AAAAAAA");
				if (counter >= tileList.Count)
					return;
				tileList [counter].transform.position = new Vector2 (minX + i * tileHeight,  startY1 + yPos*tileHeight);

				//Dodajemo i ovaj element u listu boolova
				tileList[counter].GetComponent<Tile>().xIndex = i;
				tileList [counter].GetComponent<Tile> ().yIndex = yPos;
				tileRows [yPos].row [i] = true;

				counter++;

				if (highTunnel) {
					yPos++;
					tileList [counter].transform.position = new Vector2 (minX + i * tileHeight, startY1 + yPos * tileHeight);
					counter++;
					yPos--;
				}
			}
			//predji u red iznad
			yPos++;
			//Dodajemo novi red u tileRows
			tileRows.Add (new TileRow());

			if (highTunnel) {
				yPos++;
				//Dodajemo novi red u tileRows
				tileRows.Add (new TileRow());
			}
			startOfLastHole = newTunnelStart;
			endOfLastHole = newTunnelStart + tunnelWidth - 1;
			tunnelFinished = true;
		}

	}
	//(tunnelWidth - minTunnelWidth + 1)
	public void DigBetterWiderRandomTunnel(List<GameObject> tileList){
		int counter = 0;

		while (true) {
			if (tunnelFinished) {
				tunnelWidth = Random.Range (minTunnelWidth, maxTunnelWidth);
				//Nadji najmanju pocetnu tacku za tunel, ako je manja od 0, onda je 0 - moze da pocinje desno max na endOfLastHole
				//Ako desno umesto endOfLastHole stavimo endOfLastHole + 1 - skroz druga prica!
				newTunnelStart = Random.Range (Mathf.Max (1, startOfLastHole - (tunnelWidth - minTunnelWidth+1)), Mathf.Min (fieldOfViewWidth - (minTunnelWidth - 1), endOfLastHole - (minTunnelWidth-1)));
			}
			tunnelFinished = false;
			for (int i = 0; i < newTunnelStart; i++) {
				//iscrtaj zid od 0 do pocetne tacke
				if (counter >= tileList.Count)
					return;
				holeLength++;
				tileList [counter].transform.position = new Vector2 (minX + i * tileHeight, startY1 + yPos*tileHeight);
				counter++;
				if (highTunnel) {
					yPos++;
					tileList [counter].transform.position = new Vector2 (minX + i * tileHeight, startY1 + yPos * tileHeight);
					counter++;
					yPos--;
				}
			}
			holeLength = 0;

			if (counter >= tileList.Count)
				return;
			tileList [counter - 1].GetComponent<Tile> ().isEdge = true;
			tileList [counter].GetComponent<Tile> ().isEdge = true;


			for (int i = newTunnelStart + tunnelWidth; i < fieldOfViewWidth; i++) {
				//iscrtaj zid od krajnje tacke do kraja
				//Debug.Log("AAAAAAA");
				if (counter >= tileList.Count)
					return;
				tileList [counter].transform.position = new Vector2 (minX + i * tileHeight,  startY1 + yPos*tileHeight);
				counter++;
				if (highTunnel) {
					yPos++;
					tileList [counter].transform.position = new Vector2 (minX + i * tileHeight, startY1 + yPos * tileHeight);
					counter++;
					yPos--;
				}
			}
			//predji u red iznad
			yPos++;


			if(highTunnel)
				yPos++;
			startOfLastHole = newTunnelStart;
			endOfLastHole = newTunnelStart + tunnelWidth - 1;
			tunnelFinished = true;
		}

	}

	
}

[System.Serializable]
public class TileRow{
	[SerializeField]
	public List<bool> row;

	public TileRow(){
		row = new List<bool>();
		for (int i = 0; i < 9; i++)
			row.Add (false);
	}
}
