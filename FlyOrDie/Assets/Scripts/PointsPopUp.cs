using UnityEngine;
using System.Collections;

public class PointsPopUp : MonoBehaviour {

	public int uiRootWidth;
	public int uiRootHeight;
	public float mainCamWidth;
	public float manCamHeight;
	public Transform startPos;
	float pixelsPerUnit;
	public UILabel label;
	public string labelText;
	void Start()
	{
		pixelsPerUnit = uiRootWidth / mainCamWidth;
		float uiX = startPos.position.x * pixelsPerUnit;
		float uiY = startPos.position.y * pixelsPerUnit;
		gameObject.transform.localPosition = new Vector3(uiX, uiY, 0);
		label.text = labelText;
	}
}
