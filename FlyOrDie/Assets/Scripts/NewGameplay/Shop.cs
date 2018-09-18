using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokega;

public class Shop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Obstacle") {
			coll.transform.localPosition = App.builder.dumpsterPosition;
			coll.gameObject.GetComponent<Tile> ().isEdge = false;
			coll.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
			coll.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			coll.gameObject.GetComponent<Rigidbody2D> ().angularVelocity = 0f;
			coll.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;
			coll.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
			coll.transform.rotation = Quaternion.identity;
			//Za vracanje materijala
			coll.GetComponent<SpriteRenderer>().material = App.destroyer.maskedMaterial;

			App.destroyer.helperList.Add (coll.gameObject);
			App.destroyer.tileCounter++;
			App.builder.activeFallingTiles.Remove (coll.gameObject);
			if (App.destroyer.tileCounter >= 30) {
				App.builder.DigTunnels (App.destroyer.helperList);
				App.destroyer.tileCounter = 0;
			}
		}
			
	}
}
