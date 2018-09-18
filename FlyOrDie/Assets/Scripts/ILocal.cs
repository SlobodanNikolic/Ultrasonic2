using UnityEngine;
using System.Collections;

namespace Pokega{

	public interface ILocal{
		
		void Save();
	
		void Load();

		void Reset();
	}

}