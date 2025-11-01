using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace ProjectRoulette
{
	[CreateAssetMenu(fileName = "SymbolData", menuName = "Scriptable Objects/SymbolData")]
	public class SymbolData : SOBase
	{
		public int Key;
		public int Diamond;
		public int Heart;
		public int Spade;
		public int Clover;

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
				try { Diamond = (int)Convert.ChangeType(dataList[1], typeof(int)); } catch { }
			}
			if (dataList.Count > 2 && !string.IsNullOrEmpty(dataList[2]))
			{
				try { Heart = (int)Convert.ChangeType(dataList[2], typeof(int)); } catch { }
			}
			if (dataList.Count > 3 && !string.IsNullOrEmpty(dataList[3]))
			{
				try { Spade = (int)Convert.ChangeType(dataList[3], typeof(int)); } catch { }
			}
			if (dataList.Count > 4 && !string.IsNullOrEmpty(dataList[4]))
			{
				try { Clover = (int)Convert.ChangeType(dataList[4], typeof(int)); } catch { }
			}

			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
	#endif
}
