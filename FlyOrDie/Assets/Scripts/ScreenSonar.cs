using UnityEngine;
using System.Collections;

namespace Pokega{

	public class ScreenSonar : MonoBehaviour {

		public Vector3 sonarStartPos;
		public float sonarSpeedStep;
		public float sonarMoveSpeed;
		public float sonarDuration;
		public float nextSonarWaitTime;

		public bool sonar = false;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void SonarOn(){
			StartCoroutine ("ActivateSonar");
		}

		public IEnumerator ActivateSonar(){
			Debug.Log ("HomeSonarActive");
			for (float i = sonarStartPos.y; i <= sonarDuration; i += sonarMoveSpeed) {

				transform.position = new Vector3 (sonarStartPos.x, i, sonarStartPos.z); 

				yield return new WaitForSeconds (sonarSpeedStep*Time.timeScale);
			}
			//yield return new WaitForSeconds (nextSonarWaitTime);


		}

		public void ResetPosition(){
			transform.position = sonarStartPos;
		}
	}
}