using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRoulette
{
	public class ItemManager : IManagerInit
	{
		// Active Items
		public List<ItemComponent> ListActiveItem = new List<ItemComponent>();

		public void InitAwake()
		{
		}

		public void InitStart()
		{
		}

		public void CreateItem(EItem item)
		{
			// 아이템 Instantiate
		}

		// public ItemComponent GetItem(int id)
		// {
		// 	foreach (var item in ListItems)
		// 	{
		// 		if (item.Id == id)
		// 		{
		// 			return item;
		// 		}
		// 	}
		//
		// 	Debug.LogError("Item Not Found ID : " + id);
		// 	return null;
		// }
		//
		// public void ReturnItem(int id)
		// {
		// 	foreach (var item in ListItems)
		// 	{
		// 		if (item.Id != id) continue;
		// 		item.Execute();
		// 		return;
		// 	}
		//
		// 	Debug.LogError("Item Not Found ID: " + id);
		// }
	}
}
