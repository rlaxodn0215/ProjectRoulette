using UnityEngine;

namespace ProjectRoulette
{
	public class ItemComponent // 정보 보여주는 UI Class 제작하기
	{
		private static int _itemId = 0;
		private int _id = 0;
		public int Id
		{
			get
			{
				if (_id == 0)
				{
					_id = ++_itemId;
				}

				return _id;
			}
		}
		public EItem ItemName { get; private set; }
		private readonly ItemData _itemDataCache;
		private ItemData _currentItemData;

		public ItemComponent(ItemData itemData)
		{
			_currentItemData = _itemDataCache = itemData;
			ItemName = (EItem)_itemDataCache.Key;
		}

		public void Execute()
		{
			// Item Utility에서 버프, 디버프 적용 -> Enum 기준으로 적용하자
			// _currentItemData 조절
		}

		public void ResetData()
		{
			_currentItemData = _itemDataCache;
		}
	}
}
