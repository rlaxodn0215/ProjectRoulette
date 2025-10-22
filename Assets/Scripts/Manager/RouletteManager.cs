using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectRoulette
{
	public enum ESymbolType
	{
		Diamond,
		Heart,
		Clover,
		Spade,
		Max,
		None
	}

	public class PointInfo
	{
		public int Point;
		public int Count;
	}

	public class RouletteManager
	{
		public const int RowCount = 4;
		public const int ColCount = 5;

		public List<ESymbolType> ListBoardButtons = new List<ESymbolType>(RowCount * ColCount);
		public int DeathSwitchIndex = -1;

		public ESymbolType CurrentSymbolType = ESymbolType.None;
		public ESymbolType NextSymbolType = ESymbolType.None;

		public int Point = 0;

		public UnityEvent<int> OnPointChanged = new UnityEvent<int>();
		public UnityEvent<int, ESymbolType> OnButtonClicked = new UnityEvent<int, ESymbolType>();
		public UnityEvent<int> OnDeathSwitchClicked = new UnityEvent<int>();
		public readonly UnityEvent<ESymbolType> OnUpdateSymbolType = new UnityEvent<ESymbolType>();

		public void Init()
		{
			for (var i = 0; i < ListBoardButtons.Capacity; i++)
			{
				ListBoardButtons.Add(ESymbolType.None);
			}
		}

		public void RefreshRouletteBoard()
		{
			Debug.Log("Refresh Roulette Board");

			for (var i = 0; i < ListBoardButtons.Count; i++)
			{
				ListBoardButtons[i] = ESymbolType.None;
			}

			DeathSwitchIndex = Random.Range(0, 20);

			//CurrentSymbolType = (ESymbolType)Random.Range(0, (int)ESymbolType.Max);
			NextSymbolType = (ESymbolType)Random.Range(0, (int)ESymbolType.Max);
			UpdateSymbolType();
			UpdateSymbolType();
		}

		public void UpdateSymbolType()
		{
			// TODO : 추후 확률 적용해서 코드 수정
			CurrentSymbolType = NextSymbolType;
			NextSymbolType = (ESymbolType)Random.Range(0, (int)ESymbolType.Max);
			OnUpdateSymbolType?.Invoke(NextSymbolType);
		}

		public void SelectBoardButton(int index)
		{
			if (index < 0)
			{
				return;
			}

			if (index == DeathSwitchIndex)
			{
				Debug.Log("You Lose!");
				RefreshRouletteBoard();
				ResetPoint();
				OnDeathSwitchClicked?.Invoke(index);
				return;
			}

			ListBoardButtons[index] = CurrentSymbolType;
			GetPointInfo();
			//OnPointChanged?.Invoke(Point);
			OnButtonClicked?.Invoke(index, CurrentSymbolType);
			UpdateSymbolType();
		}

		public List<PointInfo> GetPointInfo()
		{
			var pointInfoList = new List<PointInfo>();
			return pointInfoList;
		}

		private int CalculatePoint()
		{
			return 0;
		}

		private void ResetPoint()
		{
			Point = 0;
		}
	}
}
