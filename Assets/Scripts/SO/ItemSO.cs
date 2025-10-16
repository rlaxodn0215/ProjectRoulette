using UnityEngine;
using System;
using System.Collections.Generic;

namespace ProjectRoulette
{
	[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
	public class ItemSO : SOBase
	{
		public int LocalizationKey;
		public List<string> UseType;
		public int OperateRate;
		public List<string> Symbol;
		public int SymbolRate;
		public List<string> Pattern;
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
				try { LocalizationKey = (int)Convert.ChangeType(dataList[0], typeof(int)); } catch { }
			}
			if (dataList.Count > 1 && !string.IsNullOrEmpty(dataList[1]))
			{
				UseType = new List<string>();
				foreach (var val in dataList[1].Split(','))
				{
					try { UseType.Add((string)Convert.ChangeType(val.Trim(), typeof(string))); } catch { }
				}
			}
			if (dataList.Count > 2 && !string.IsNullOrEmpty(dataList[2]))
			{
				try { OperateRate = (int)Convert.ChangeType(dataList[2], typeof(int)); } catch { }
			}
			if (dataList.Count > 3 && !string.IsNullOrEmpty(dataList[3]))
			{
				Symbol = new List<string>();
				foreach (var val in dataList[3].Split(','))
				{
					try { Symbol.Add((string)Convert.ChangeType(val.Trim(), typeof(string))); } catch { }
				}
			}
			if (dataList.Count > 4 && !string.IsNullOrEmpty(dataList[4]))
			{
				try { SymbolRate = (int)Convert.ChangeType(dataList[4], typeof(int)); } catch { }
			}
			if (dataList.Count > 5 && !string.IsNullOrEmpty(dataList[5]))
			{
				Pattern = new List<string>();
				foreach (var val in dataList[5].Split(','))
				{
					try { Pattern.Add((string)Convert.ChangeType(val.Trim(), typeof(string))); } catch { }
				}
			}
			if (dataList.Count > 6 && !string.IsNullOrEmpty(dataList[6]))
			{
				try { PatternRate = (int)Convert.ChangeType(dataList[6], typeof(int)); } catch { }
			}
			if (dataList.Count > 7 && !string.IsNullOrEmpty(dataList[7]))
			{
				try { Cost = (int)Convert.ChangeType(dataList[7], typeof(int)); } catch { }
			}
			if (dataList.Count > 8 && !string.IsNullOrEmpty(dataList[8]))
			{
				try { CostRate = (int)Convert.ChangeType(dataList[8], typeof(int)); } catch { }
			}
			if (dataList.Count > 9 && !string.IsNullOrEmpty(dataList[9]))
			{
				try { MoneyRate = (int)Convert.ChangeType(dataList[9], typeof(int)); } catch { }
			}
			if (dataList.Count > 10 && !string.IsNullOrEmpty(dataList[10]))
			{
				try { Life = (int)Convert.ChangeType(dataList[10], typeof(int)); } catch { }
			}
			if (dataList.Count > 11 && !string.IsNullOrEmpty(dataList[11]))
			{
				try { LifeRate = (int)Convert.ChangeType(dataList[11], typeof(int)); } catch { }
			}
			if (dataList.Count > 12 && !string.IsNullOrEmpty(dataList[12]))
			{
				try { DeathRate = (int)Convert.ChangeType(dataList[12], typeof(int)); } catch { }
			}
			if (dataList.Count > 13 && !string.IsNullOrEmpty(dataList[13]))
			{
				try { AvoidDeath = (bool)Convert.ChangeType(dataList[13], typeof(bool)); } catch { }
			}
		}
	}
	#endif
}
