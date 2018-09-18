using UnityEngine;
using System.Collections;

namespace Pokega{

	public class PointsPopUpHolder : MonoBehaviour {

		public static PointsPopUpHolder instance = null;

		void Awake()
		{
			if(instance == null)
				instance = this;
			else if(instance != this)
				Destroy(gameObject);
		}
	}
}