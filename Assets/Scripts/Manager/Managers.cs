using UnityEngine;

namespace ProjectRoulette
{
	public class Managers : MonoBehaviour
	{
		private static Managers _instance;

		private DataManager _data = new DataManager();
		private RouletteManager _roulette = new RouletteManager();
		private UIManager _ui = new UIManager();
		private ItemManager _item = new ItemManager();
		private GameplayManger _gameplay = new GameplayManger();

		public static DataManager Data => _instance._data;
		public static RouletteManager Roulette => _instance._roulette;
		public static UIManager UI => _instance._ui;
		public static ItemManager Item => _instance._item;
		public static GameplayManger Gameplay => _instance._gameplay;

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
				return;
			}

			_instance = this;
			DontDestroyOnLoad(gameObject);
			Init();
		}

		private static void Init()
		{
			Data.Init();
			Roulette.Init();
			UI.Init();
			Item.Init();
			Gameplay.Init();
		}

		public static void Clear()
		{
			
		}
	}
}
