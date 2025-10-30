using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectRoulette
{
	public class PointInfo
	{
		public int Point;
	}

	public class SlotController
	{
		public ESymbolType SymbolType;

		public SlotController(ESymbolType symbolType)
		{
			SymbolType = symbolType;
		}
	}

	public class RouletteManager
	{
		public List<SlotController> ListSlotControllers = new List<SlotController>();
		public int DeathSlotIndex = -1;

		public ESymbolType CurrentSymbolType = ESymbolType.None;
		public ESymbolType NextSymbolType = ESymbolType.None;

		public int Point = 0;

		public UnityEvent<int> OnPointChanged = new UnityEvent<int>();
		public UnityEvent<int, ESymbolType> OnSlotClicked = new UnityEvent<int, ESymbolType>();
		public UnityEvent OnDeathSlotClicked = new UnityEvent();
		public UnityEvent<ESymbolType> OnUpdateSymbolType = new UnityEvent<ESymbolType>();
		public UnityEvent OnRouletteReset = new UnityEvent();

		public void Init()
		{
			for (var i = 0; i < GlobalOption.SlotRowCount * GlobalOption.SlotColCount; i++)
			{
				ListSlotControllers.Add(new SlotController(ESymbolType.None));
			}
		}

		public void ResetRouletteBoard()
		{
			Debug.Log("Reset Roulette Board");

			foreach (var t in ListSlotControllers)
			{
				t.SymbolType = ESymbolType.None;
			}

			DeathSlotIndex = Random.Range(0, 20);

			// TODO: 추후 확률 적용
			CurrentSymbolType = (ESymbolType)Random.Range(0, (int)ESymbolType.Max);
			NextSymbolType = (ESymbolType)Random.Range(0, (int)ESymbolType.Max);
			OnUpdateSymbolType?.Invoke(NextSymbolType);
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

			// if (index == DeathSwitchIndex)
			// {
			// 	Debug.Log("You Lose!");
			// 	RefreshRouletteBoard();
			// 	ResetPoint();
			// 	OnDeathSwitchClicked?.Invoke(index);
			// 	return;
			// }
			//
			// ListBoardButtons[index] = CurrentSymbolType;
			// GetPointInfo();
			// //OnPointChanged?.Invoke(Point);
			// OnButtonClicked?.Invoke(index, CurrentSymbolType);
			// UpdateSymbolType();
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
