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
		public ulong Point;
	}

	public class SlotInfo
	{
		public ESymbol SymbolType;

		public SlotInfo(ESymbol symbolType)
		{
			SymbolType = symbolType;
		}
	}

	public class RouletteBoard
	{
		public List<SlotInfo> ListBoardSlotInfo { get; private set; } = new List<SlotInfo>();
		public int DeathSlotIndex { get; private set; }

		public ulong Point { get; private set; }
		public List<PointInfo> ListPointInfo { get; private set; } = new List<PointInfo>();

		public ESymbol CurrentSymbolType { get; private set; }
		public ESymbol NextSymbolType { get; private set; }

		// Events
		public UnityEvent<ulong> OnPointChanged = new UnityEvent<ulong>(); // ulong - 바뀐 점수

		public UnityEvent<int, ESymbol>
			OnSlotClicked = new UnityEvent<int, ESymbol>(); // int - index, ESymbol - current symbol type

		public UnityEvent OnDeathSlotClicked = new UnityEvent();

		public UnityEvent<ESymbol>
			OnUpdatESymbol = new UnityEvent<ESymbol>(); // ESymbol - new symbol type

		public UnityEvent OnRouletteReset = new UnityEvent();


		// 현재 룰렛에 적용되는 심볼 및 패턴
		public List<SymbolData> ListUseSymbol { get; private set; } = new List<SymbolData>();
		public List<PatternData> ListUsePattern { get; private set; } = new List<PatternData>();

		private int _slotCount = GlobalValue.SlotRowCount * GlobalValue.SlotColCount;

		public void Init()
		{
			for (var i = 0; i < _slotCount; i++)
			{
				ListBoardSlotInfo.Add(new SlotInfo(ESymbol.None));
			}

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
			UpdatESymbol();

			OnPointChanged?.Invoke(Point);
			OnSlotClicked?.Invoke(index, CurrentSymbolType);
		}

		private void RefreshBoard()
		{
			Debug.Log("Reset Roulette Board");

			foreach (var slotInfo in ListBoardSlotInfo)
			{
				slotInfo.SymbolType = ESymbol.None;
			}

			DeathSlotIndex = Random.Range(0, _slotCount);

			CurrentSymbolType = GetNextSymbolType();
			NextSymbolType = GetNextSymbolType();
			OnUpdatESymbol?.Invoke(NextSymbolType);
		}

		private List<PointInfo> GetPointInfo()
		{
			var list = new List<PointInfo>();
			// 점수 계산
			// 기호, 조합, 아이템 사용 등...
			return list;
		}

		private ulong CalculatePoint()
		{
			return 0;
		}

		private void UpdatESymbol()
		{
			CurrentSymbolType = NextSymbolType;
			NextSymbolType = GetNextSymbolType();
			OnUpdatESymbol?.Invoke(NextSymbolType);
		}

		private void SelectedDeathSlot()
		{
			Debug.Log("You Lose!");
			RefreshBoard();
			ResetPoint();
			OnDeathSlotClicked?.Invoke();
			DeathSlotIndex = Random.Range(0, _slotCount);
		}

		private ESymbol GetNextSymbolType()
		{
			var list = new List<int>
			{
				// CurrentSymbolChanceData.A,
				// CurrentSymbolChanceData.B,
				// CurrentSymbolChanceData.C,
				// CurrentSymbolChanceData.D,
				// CurrentSymbolChanceData.E
			};

			return (ESymbol)(ProbabilityUtility.GetRandomProbability(list) + 1);
		}

		private void ResetPoint()
		{
			Point = 0;
		}
	}
}
