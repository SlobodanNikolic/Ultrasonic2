using UnityEngine;
using System.Collections;

public class CameraFolow : MonoBehaviour {

	public Transform bat;
	public float cameraDistance;
	public float yDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void LateUpdate(){
		transform.position = new Vector3 (bat.position.x, bat.position.y + yDistance, bat.position.z - cameraDistance);

	}
}
