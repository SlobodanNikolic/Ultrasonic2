using UnityEngine;
using System.Collections;

public class Sonar3D : MonoBehaviour {

	public Vector3 start;
	public float sonarSpeedStep;
	public float sonarMoveSpeed;
	public float sonarDuration;
	public float nextSonarWaitTime;
	public GameObject bat;

	public bool sonar = true;

	// Use this for initialization
	void Start () {
		SonarOn ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SonarOn(){
		sonar = true;
		//gameObject.GetComponent<MeshRenderer> ().enabled = true;
		StartCoroutine (ActivateSonar());
	}

	public void SonarOff(){
		sonar = false;
		StopCoroutine ("ActivateSonar");
		//gameObject.GetComponent<MeshRenderer> ().enabled = false;


	}

	public IEnumerator ActivateSonar(){
		if (sonar) {
			start = bat.transform.position;
			for (float i = start.z; i <= sonarDuration; i += sonarMoveSpeed) {

				transform.position = new Vector3 (start.x, start.y, i); 

				yield return new WaitForSeconds (sonarSpeedStep*Time.timeScale);
			}
			//yield return new WaitForSeconds (nextSonarWaitTime);
			StartCoroutine (ActivateSonar ());
		}

	}
}
