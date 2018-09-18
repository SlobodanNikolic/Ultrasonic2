using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pokega;

public class NewSonar : MonoBehaviour {

	public Vector2 startPos;
	public float time;
	public float sonarRepeatTime;
	public float currentY;
	public float targetY;
	public float sonarStartY;
	public float sonarStartScale;
	public float sonarTargetScale;
	public Transform blackSonar;
	public float blackSonarWait;
	public float sonarBackTime;
	public float blockFallChance;
	public bool goingWide;
	public float sonarEnergyCost;
	public Material defaultSpriteMat;
	public float fallWaitTime = 0.05f;
	public int fallingTileX = -20;
	public int fallingTileY = -20;


	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
		sonarStartScale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SonarOn(){
//		InvokeRepeating ("StartSonar", 0f, sonarRepeatTime);

	}

	public void StartSonar(){
//		if (App.bat.energy >= sonarEnergyCost) {
//			StartCoroutine (ScaleSonar ());
//			App.bat.DecreaseEnergy (sonarEnergyCost);
//		}
		//StartCoroutine (ScaleBlackSonar());
	}

	public IEnumerator MoveSonar(){
//
//		for (float i = 0f; i <= time;) {
//			i += Time.deltaTime;
//			float currentTime = Mathf.Min(i / time, 1f);
//			transform.localPosition = new Vector3 (0f, EasingFunction.EaseInOutQuad (sonarStartY, targetY, currentTime), 0f);
//
			yield return null;
//		}
	}

	public IEnumerator ScaleBlackSonar(){

//		yield return new WaitForSecondsRealtime (blackSonarWait);
//
//		for (float i = 0f; i <= time;) {
//			i += Time.deltaTime;
//			float currentTime = Mathf.Min(i / time, 1f);
//			blackSonar.localScale = new Vector3 (EasingFunction.EaseInOutQuad (sonarStartScale, sonarTargetScale, currentTime), EasingFunction.EaseInOutQuad (sonarStartScale, sonarTargetScale, currentTime), 1f);
//
			yield return null;
//		}
//		blackSonar.localScale = new Vector3 (0f, 0f, 1f);
	}

	public IEnumerator ScaleSonar(){
//		goingWide = true;
//		for (float i = 0f; i <= time;) {
//			i += Time.deltaTime;
//			float currentTime = Mathf.Min(i / time, 1f);
//			transform.localScale = new Vector3 (EasingFunction.EaseInOutQuad (sonarStartScale, sonarTargetScale, currentTime), EasingFunction.EaseInOutQuad (sonarStartScale, sonarTargetScale, currentTime), 1f);
//
//			yield return null;
//		}
//		fallWaitTime = 0.05f;
//		goingWide = false;
//		for (float i = 0f; i <= sonarBackTime;) {
//			i += Time.deltaTime;
//			float currentTime = Mathf.Min(i / time, 1f);
//			transform.localScale = new Vector3 (EasingFunction.EaseInOutQuad (sonarTargetScale, sonarStartScale, currentTime), EasingFunction.EaseInOutQuad (sonarTargetScale, sonarStartScale, currentTime), 1f);
//
			yield return null;
//		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		Tile t;
		if ((t = coll.GetComponent<Tile>()) != null && goingWide && transform.localScale.x > sonarTargetScale/3f) {
			if (t.isEdge) {
				t.ShouldFall (fallWaitTime);
				fallWaitTime += 0.1f;
				//Komenarisem da probam novu foru sa tile fall
//				if (Random.Range (0, 100) < blockFallChance) {
//					t.GetComponent<Rigidbody2D> ().isKinematic = false;
//					t.GetComponent<SpriteRenderer> ().sortingOrder = 2;
//					t.GetComponent<SpriteRenderer> ().color = Color.gray;
//					StartCoroutine(App.camShake.Shake (0.2f,0.3f));
//					t.isEdge = false;
//					//Da se tile vidi i bez sonara
//					t.GetComponent<SpriteRenderer>().material = defaultSpriteMat;
//					App.builder.activeFallingTiles.Add (coll.gameObject);
//				}
			}
		}
	}

}
