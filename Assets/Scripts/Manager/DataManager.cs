using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectRoulette
{
	public class DataManager : IManagerInit
	{
		private Dictionary<EItem, ItemData> _itemData = new Dictionary<EItem, ItemData>();
		private Dictionary<ESymbolData, SymbolData> _symbolData = new Dictionary<ESymbolData, SymbolData>();

		public void InitAwake()
		{
			// Load Data
			var itemData = Resources.LoadAll<ItemData>(GlobalValue.SOpath);
			var symbolData = Resources.LoadAll<SymbolData>(GlobalValue.SOpath);

			// Cache Data
			for (var i = 1; i < (int)EItem.Max; i++)
			{
				_itemData[(EItem)i] = itemData.FirstOrDefault(x => x.Key == i);
			}

			for (var i = 1; i < (int)ESymbolData.Max; i++)
			{
				_symbolData[(ESymbolData)i] = symbolData.FirstOrDefault(x => x.Key == i);
			}
		}

		public void InitStart()
		{
		}

		public ItemData GetItemData(EItem item)
		{
			var itemData = _itemData.GetValueOrDefault(item);

			if (itemData == null)
			{
				Debug.LogError("Item Not Found : " + item);
			}

			return itemData;
		}

		public SymbolData GetSymbolData(ESymbolData data)
		{
			var symbolData = _symbolData.GetValueOrDefault(data);

			if (symbolData == null)
			{
				Debug.LogError("SymbolData Not Found : " + data);
			}

			return symbolData;
		}

		public void SaveGame()
		{
		}

		public void LoadGame()
		{
		}
	}
}
