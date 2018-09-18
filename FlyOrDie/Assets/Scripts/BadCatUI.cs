using UnityEngine;
using System.Collections;

namespace Pokega{

	public class BadCatUI : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}

		public void Test(){

			Debug.Log ("TESTING");
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetMouseButtonUp (0)) {
			
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.name == "PlayButton") {
						App.game.StartGame ();
					}
					else if(hit.collider.name == "MultiplayerButton") {
						App.game.StartMultiplayer ();
					}
					else if(hit.collider.name == "GiftButton") {
						App.ui.SetScreen ("UI Home");
						App.game.homeButtons.SetActive (true);
					}else if(hit.collider.name == "PlayButton") {

					}else if(hit.collider.name == "PlayButton") {

					}
				}
			

			}
		}


	}
}