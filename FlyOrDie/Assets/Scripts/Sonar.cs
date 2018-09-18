using UnityEngine;
using System.Collections;

public class Sonar : MonoBehaviour {

	public GameObject bat;
	public float sonarSpeedStep;
	public float sonarMoveSpeed;
	public float sonarDuration;
	public float nextSonarWaitTime;

	public bool sonar = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SonarOn(){
		sonar = true;
		gameObject.GetComponent<MeshRenderer> ().enabled = true;
		StartCoroutine (ActivateSonar());
	}

	public void SonarOff(){
		sonar = false;
		StopCoroutine ("ActivateSonar");
		gameObject.GetComponent<MeshRenderer> ().enabled = false;


	}

	public IEnumerator ActivateSonar(){
		if (sonar) {
			for (float i = bat.transform.position.y; i <= sonarDuration; i += sonarMoveSpeed) {
			
				transform.position = new Vector3 (bat.transform.position.x, i, bat.transform.position.z); 

				yield return new WaitForSeconds (sonarSpeedStep*Time.timeScale);
			}
			//yield return new WaitForSeconds (nextSonarWaitTime);
			StartCoroutine (ActivateSonar ());
		}

	}

	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Pray") {
			Debug.Log ("PRAY DETECTED");
			if(other.gameObject.transform.position.y > -0.5f && other.gameObject.transform.position.y < 1.3f)
			other.gameObject.GetComponent<Pray> ().Detected ();
		}
		else if(other.gameObject.tag == "MovingPray"){
			Debug.Log ("MOVING PRAY DETECTED");
			if(other.gameObject.transform.position.y > -0.5f && other.gameObject.transform.position.y < 1.3f)
				other.gameObject.GetComponent<MovingPray> ().Detected ();
		}
		else if(other.gameObject.tag == "AttackingPray"){
			Debug.Log ("ATTACKING PRAY DETECTED");
			if(other.gameObject.transform.position.y > -0.5f && other.gameObject.transform.position.y < 1.3f)
				other.gameObject.GetComponent<AllAttackingPray> ().Detected ();
		}
	}

}
