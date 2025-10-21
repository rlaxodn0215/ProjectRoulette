using System.Collections.Generic;
using UnityEngine;

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

		public void Init()
		{
		}

		public void RefreshRouletteBoard()
		{
			Debug.Log("Refresh Roulette Board");

			for (int i = 0; i < ListBoardButtons.Count; i++)
			{
				ListBoardButtons[i] = ESymbolType.None;
			}

			DeathSwitchIndex = Random.Range(0, 20);

			CurrentSymbolType = (ESymbolType)Random.Range(0, (int)ESymbolType.Max);
			NextSymbolType = (ESymbolType)Random.Range(0, (int)ESymbolType.Max);
		}

		public void UpdateSymbolType()
		{
			// TODO : 추후 확률 적용해서 코드 수정
			CurrentSymbolType = NextSymbolType;
			NextSymbolType = (ESymbolType)Random.Range(0, (int)ESymbolType.Max);
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
				return;
			}

			ListBoardButtons[index] = CurrentSymbolType;
			GetPointInfo();
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
