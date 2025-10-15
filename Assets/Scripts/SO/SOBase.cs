using System.Collections.Generic;
using UnityEngine;

namespace ProjectRoulette
{
	//[CreateAssetMenu(fileName = "SOBase", menuName = "Scriptable Objects/SOBase")]
	public class SOBase : ScriptableObject
	{
		public int Key;
		//public 

		public virtual void UpdateData(List<string> data)
		{
			Debug.Log("UpdateData - " + this.GetType().Name);
		}
	}
}
