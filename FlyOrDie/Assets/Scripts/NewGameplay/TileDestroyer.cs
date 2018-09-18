using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokega;

public class TileDestroyer : MonoBehaviour {

	public int tileCounter;
	public int firstLevObsCounter;
	public List<GameObject> helperList = new List<GameObject>();
	public List<GameObject> firstLevHelperList =  new List<GameObject>();
	public Material maskedMaterial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Obstacle") {
			coll.gameObject.GetComponent<BoxCollider2D> ().size = new Vector2(App.builder.tileColStartScale, App.builder.tileColStartScale);
			coll.gameObject.GetComponent<Tile> ().falling = false;
			coll.gameObject.GetComponent<Tile> ().isEdge = false;
			coll.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
			coll.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			coll.gameObject.GetComponent<Rigidbody2D> ().angularVelocity = 0f;
			coll.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;
			coll.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
			coll.transform.rotation = Quaternion.identity;
			//Za vracanje materijala
			coll.GetComponent<SpriteRenderer>().material = maskedMaterial;

			helperList.Add (coll.gameObject);
			tileCounter++;
			App.builder.activeFallingTiles.Remove (coll.gameObject);
			if (tileCounter >= 30) {
				App.builder.DigTunnels (helperList);
				tileCounter = 0;
			}
		} else if (coll.gameObject.tag == "Loot") {
			Destroy (coll.gameObject);
			App.builder.activeLoot.Remove (coll.gameObject);
		} else if (coll.gameObject.tag == "Web") {
			Destroy (coll.gameObject);
			App.builder.activeWeb.Remove (coll.gameObject);
		} else if (coll.gameObject.tag == "FirstLevelObs") {
			firstLevHelperList.Add (coll.gameObject);
			firstLevObsCounter++;
			if (firstLevObsCounter >= 8) {
				Debug.Log ("TileDestroyer placing obstacles");
				App.builder.PlaceObstacles ();
				firstLevObsCounter = 0;
			}
		}
	}
}
