using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Pokega{
	
[System.Serializable]

	public class Score{
	
		public string scoreName;
		public bool isFloat;
		public string score;
		public List<UILabel> scoreLabels;
		public GameObject newBest;

		public Score(float sc){
			score = Crypting.EncryptFloat(sc);
			
		}

		public Score(string name, float sc, bool isItFloat){
			score = Crypting.EncryptFloat(sc);
			scoreName = name;
			isFloat = isItFloat;
		}



	}
}