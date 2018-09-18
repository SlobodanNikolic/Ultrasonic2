using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokega;

public class Tile : MonoBehaviour {

	public bool isEdge;
	public int xIndex;
	public int yIndex;
	public Vector3 colliderStartSize;
	public bool falling;

	// Use this for initialization
	void Start () {
		colliderStartSize = GetComponent<BoxCollider2D> ().size;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.GetComponent<Tile>() != null && falling){
			int x = coll.gameObject.GetComponent<Tile> ().xIndex;
			int y = coll.gameObject.GetComponent<Tile> ().yIndex;
			if (x + 1 < App.builder.tileRows [y + 2].row.Count && x - 1 >= 0) {
				if (App.builder.tileRows [y + 2].row [x + 1] || App.builder.tileRows [y + 2].row [x - 1]) {
					transform.position = App.builder.dumpsterPosition;
					GetComponent<BoxCollider2D> ().size = new Vector2 (App.builder.tileColStartScale, App.builder.tileColStartScale);
					GetComponent<Tile> ().falling = false;
					GetComponent<Tile> ().isEdge = false;
					GetComponent<Rigidbody2D> ().isKinematic = true;
					GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
					GetComponent<Rigidbody2D> ().angularVelocity = 0f;
					GetComponent<SpriteRenderer> ().sortingOrder = 1;
					GetComponent<SpriteRenderer> ().color = Color.white;
					transform.rotation = Quaternion.identity;
					//Za vracanje materijala
					GetComponent<SpriteRenderer> ().material = App.destroyer.maskedMaterial;

					App.destroyer.helperList.Add (gameObject);
					App.destroyer.tileCounter++;
					App.builder.activeFallingTiles.Remove (gameObject);
					if (App.destroyer.tileCounter >= 30) {
						App.builder.DigTunnels (App.destroyer.helperList);
						App.destroyer.tileCounter = 0;
					}
				}
			}
		}
		falling = false;
//		if (falling) {
//			falling = false;
//			GetComponent<Tile> ().isEdge = false;
//			GetComponent<Rigidbody2D> ().isKinematic = true;
//			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
//			GetComponent<Rigidbody2D> ().angularVelocity = 0f;
//			GetComponent<SpriteRenderer> ().sortingOrder = 1;
//			GetComponent<SpriteRenderer> ().color = Color.white;
//			transform.rotation = Quaternion.identity;
//			transform.position = App.builder.dumpsterPosition;
//			//Za vracanje materijala
//			GetComponent<SpriteRenderer>().material = App.destroyer.maskedMaterial;
//
//			App.destroyer.helperList.Add (coll.gameObject);
//			App.destroyer.tileCounter++;
//			App.builder.activeFallingTiles.Remove (coll.gameObject);
//			if (App.destroyer.tileCounter >= 30) {
//				App.builder.DigTunnels (App.destroyer.helperList);
//				App.destroyer.tileCounter = 0;
//			}
//		}
	}

	public void ShouldFall(float waitTime){
		RaycastHit2D hit = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y - App.builder.tileHeight), 
			-Vector2.up, 20f, 1 << LayerMask.NameToLayer("Obstacle"));
		if (hit != null) {
			if (hit.distance > 0f && (Random.Range (0, 100) < App.game.sonar.blockFallChance)) {
				if ((App.game.sonar.fallingTileX == xIndex || App.game.sonar.fallingTileX == xIndex - 1 ||
				   App.game.sonar.fallingTileX == xIndex + 1) && (App.game.sonar.fallingTileY == yIndex || App.game.sonar.fallingTileY == yIndex - 1))
					return;
				else
					Invoke ("Fall", waitTime);
			}
			float offsetDistance = hit.distance;
		}

	}

	public void Fall(){
		App.game.sonar.fallingTileX = xIndex;
		App.game.sonar.fallingTileY = yIndex;
		GetComponent<BoxCollider2D> ().size = GetComponent<BoxCollider2D> ().size / 1.1f;
		GetComponent<Rigidbody2D> ().isKinematic = false;
		GetComponent<SpriteRenderer> ().sortingOrder = 2;
		GetComponent<SpriteRenderer> ().color = Color.gray;
		StartCoroutine(App.camShake.Shake (0.3f,0.3f));
		falling = true;
		isEdge = false;
		//Da se tile vidi i bez sonara
		GetComponent<SpriteRenderer>().material = App.game.sonar.defaultSpriteMat;
		App.builder.activeFallingTiles.Add (gameObject);
	}
}
