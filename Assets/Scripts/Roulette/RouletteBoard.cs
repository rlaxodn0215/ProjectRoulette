using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace ProjectRoulette
{
	public struct PointInfo
	{
		public decimal Point;
	}

	public class SlotInfo
	{
		public ESymbolType SymbolType;

		public SlotInfo(ESymbolType symbolType)
		{
			SymbolType = symbolType;
		}
	}

	public class RouletteBoard
	{
		public List<SlotInfo> ListBoardSlotInfo { get; private set; } = new List<SlotInfo>();
		public int DeathSlotIndex { get; private set; }

		public decimal Point { get; private set; }
		public List<PointInfo> ListPointInfo { get; private set; } = new List<PointInfo>();

		public ESymbolType CurrentSymbolType { get; private set; }
		public ESymbolType NextSymbolType { get; private set; }

		// public List<ItemComponent> ListActiveItem = new List<ItemComponent>();
		// => ItemManager 이용

		// Events
		public UnityEvent<decimal> OnPointChanged = new UnityEvent<decimal>(); // decimal - 바뀐 점수

		public UnityEvent<int, ESymbolType>
			OnSlotClicked = new UnityEvent<int, ESymbolType>(); // int - index, ESymbolType - current symbol type

		public UnityEvent OnDeathSlotClicked = new UnityEvent();

		public UnityEvent<ESymbolType>
			OnUpdateSymbolType = new UnityEvent<ESymbolType>(); // ESymbolType - new symbol type

		public UnityEvent OnRouletteReset = new UnityEvent();

		// Data
		public SymbolData CurrentSymbolChanceData { get; private set; }
		private SymbolData _symbolChanceDataCache;
		public SymbolData CurrentSymbolPointData { get; private set; }
		private SymbolData _symbolPointDataCache;


		private int _slotCount = GlobalValue.SlotRowCount * GlobalValue.SlotColCount;

		public void Init()
		{
			for (var i = 0; i < _slotCount; i++)
			{
				ListBoardSlotInfo.Add(new SlotInfo(ESymbolType.None));
			}

			// Data Init
			CurrentSymbolChanceData = _symbolChanceDataCache = Managers.Data.GetSymbolData(ESymbolData.SymbolChance);
			CurrentSymbolPointData = _symbolPointDataCache = Managers.Data.GetSymbolData(ESymbolData.SymbolPoint);

			RefreshBoard();
		}

		public void SelectBoardButton(int index)
		{
			Debug.Log("Board Button Selected : " + index);

			if (index < 0)
			{
				Debug.LogError("Index is less than 0");
				return;
			}

			if (index == DeathSlotIndex)
			{
				SelectedDeathSlot();
				return;
			}

			ListBoardSlotInfo[index].SymbolType = CurrentSymbolType;
			ListPointInfo = GetPointInfo();
			UpdateSymbolType();

			OnPointChanged?.Invoke(Point);
			OnSlotClicked?.Invoke(index, CurrentSymbolType);
		}

		private void RefreshBoard()
		{
			Debug.Log("Reset Roulette Board");

			foreach (var slotInfo in ListBoardSlotInfo)
			{
				slotInfo.SymbolType = ESymbolType.None;
			}

			DeathSlotIndex = Random.Range(0, _slotCount);

			CurrentSymbolType = GetNextSymbolType();
			NextSymbolType = GetNextSymbolType();
			OnUpdateSymbolType?.Invoke(NextSymbolType);
		}

		private List<PointInfo> GetPointInfo()
		{
			var list = new List<PointInfo>();
			// 점수 계산
			// 기호, 조합, 아이템 사용 등...
			return list;
		}

		private decimal CalculatePoint()
		{
			return 0;
		}

		private void UpdateSymbolType()
		{
			CurrentSymbolType = NextSymbolType;
			NextSymbolType = GetNextSymbolType();
			OnUpdateSymbolType?.Invoke(NextSymbolType);
		}

		private void SelectedDeathSlot()
		{
			Debug.Log("You Lose!");
			RefreshBoard();
			ResetPoint();
			OnDeathSlotClicked?.Invoke();
			DeathSlotIndex = Random.Range(0, _slotCount);
		}

		private ESymbolType GetNextSymbolType()
		{
			var list = new List<int>
			{
				CurrentSymbolChanceData.A,
				CurrentSymbolChanceData.B,
				CurrentSymbolChanceData.C,
				CurrentSymbolChanceData.D,
				CurrentSymbolChanceData.E
			};

			return (ESymbolType)(ProbabilityUtility.GetRandomProbability(list) + 1);
		}

		private void ResetPoint()
		{
			Point = 0;
		}
	}
}
