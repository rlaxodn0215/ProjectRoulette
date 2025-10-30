using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace ProjectRoulette
{
	[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
	public class ItemData : SOBase
	{
		public int Key;
		public int LocalizationKey;
		public List<EItemUseType> UseType;
		public int OperateRate;
		public List<ESymbolType> Symbol;
		public int SymbolRate;
		public List<EPatternType> Pattern;
		public int PatternRate;
		public int Cost;
		public int CostRate;
		public int MoneyRate;
		public int Life;
		public int LifeRate;
		public int DeathRate;
		public bool AvoidDeath;

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
				try { LocalizationKey = (int)Convert.ChangeType(dataList[1], typeof(int)); } catch { }
			}
			if (dataList.Count > 2 && !string.IsNullOrEmpty(dataList[2]))
			{
				UseType = new List<EItemUseType>();
				foreach (var val in dataList[2].Split(','))
				{
					try { UseType.Add((EItemUseType)Convert.ChangeType(val.Trim(), typeof(EItemUseType))); } catch { }
				}
			}
			if (dataList.Count > 3 && !string.IsNullOrEmpty(dataList[3]))
			{
				try { OperateRate = (int)Convert.ChangeType(dataList[3], typeof(int)); } catch { }
			}
			if (dataList.Count > 4 && !string.IsNullOrEmpty(dataList[4]))
			{
				Symbol = new List<ESymbolType>();
				foreach (var val in dataList[4].Split(','))
				{
					try { Symbol.Add((ESymbolType)Convert.ChangeType(val.Trim(), typeof(ESymbolType))); } catch { }
				}
			}
			if (dataList.Count > 5 && !string.IsNullOrEmpty(dataList[5]))
			{
				try { SymbolRate = (int)Convert.ChangeType(dataList[5], typeof(int)); } catch { }
			}
			if (dataList.Count > 6 && !string.IsNullOrEmpty(dataList[6]))
			{
				Pattern = new List<EPatternType>();
				foreach (var val in dataList[6].Split(','))
				{
					try { Pattern.Add((EPatternType)Convert.ChangeType(val.Trim(), typeof(EPatternType))); } catch { }
				}
			}
			if (dataList.Count > 7 && !string.IsNullOrEmpty(dataList[7]))
			{
				try { PatternRate = (int)Convert.ChangeType(dataList[7], typeof(int)); } catch { }
			}
			if (dataList.Count > 8 && !string.IsNullOrEmpty(dataList[8]))
			{
				try { Cost = (int)Convert.ChangeType(dataList[8], typeof(int)); } catch { }
			}
			if (dataList.Count > 9 && !string.IsNullOrEmpty(dataList[9]))
			{
				try { CostRate = (int)Convert.ChangeType(dataList[9], typeof(int)); } catch { }
			}
			if (dataList.Count > 10 && !string.IsNullOrEmpty(dataList[10]))
			{
				try { MoneyRate = (int)Convert.ChangeType(dataList[10], typeof(int)); } catch { }
			}
			if (dataList.Count > 11 && !string.IsNullOrEmpty(dataList[11]))
			{
				try { Life = (int)Convert.ChangeType(dataList[11], typeof(int)); } catch { }
			}
			if (dataList.Count > 12 && !string.IsNullOrEmpty(dataList[12]))
			{
				try { LifeRate = (int)Convert.ChangeType(dataList[12], typeof(int)); } catch { }
			}
			if (dataList.Count > 13 && !string.IsNullOrEmpty(dataList[13]))
			{
				try { DeathRate = (int)Convert.ChangeType(dataList[13], typeof(int)); } catch { }
			}
			if (dataList.Count > 14 && !string.IsNullOrEmpty(dataList[14]))
			{
				try { AvoidDeath = (bool)Convert.ChangeType(dataList[14], typeof(bool)); } catch { }
			}

			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
	#endif
}
