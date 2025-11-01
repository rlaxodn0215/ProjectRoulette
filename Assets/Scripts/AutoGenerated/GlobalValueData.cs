using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace ProjectRoulette
{
	[CreateAssetMenu(fileName = "GlobalValueData", menuName = "Scriptable Objects/GlobalValueData")]
	public class GlobalValueData : SOBase
	{
		public int Key;
		public string TestOption_1;
		public float TestingOne;

	#if UNITY_EDITOR
		public override void UpdateData(List<string> dataList)
		{
			base.UpdateData(dataList);

			if (dataList.Count > 0 && !string.IsNullOrEmpty(dataList[0]))
			{
				try { Key = (int)Convert.ChangeType(dataList[0], typeof(int)); } catch { }
			}
			if (dataList.Count > 1 && !string.IsNullOrEmpty(dataList[1]))
			{
				try { TestOption_1 = (string)Convert.ChangeType(dataList[1], typeof(string)); } catch { }
			}
			if (dataList.Count > 2 && !string.IsNullOrEmpty(dataList[2]))
			{
				try { TestingOne = (float)Convert.ChangeType(dataList[2], typeof(float)); } catch { }
			}

			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
	#endif
}
