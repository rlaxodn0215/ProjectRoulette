using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectRoulette
{
	public class UIRouletteWindow : MonoBehaviour
	{
		[SerializeField] private List<Button> _buttons = new List<Button>();
		[SerializeField] private List<TMP_Text> _buttonTexts = new List<TMP_Text>();

		[SerializeField] private Button _resetButton = null;

		[SerializeField] private TMP_Text _currencyText = null;

		[SerializeField] private TMP_Text _currentText = null;

		[SerializeField] private TMP_Text _nextText = null;
		
		[SerializeField] private GameObject _deadSign = null;

		void Start()
		{
			Managers.Roulette.OnDeathSwitchClicked.AddListener(DeadSign);
			Managers.Roulette.OnButtonClicked.AddListener(ChangeButtonSymbol);
			Managers.Roulette.OnUpdateSymbolType.AddListener(ChangeSymbolUI);
			_resetButton.onClick.AddListener(ResetButton);
			Managers.Roulette.RefreshRouletteBoard();
		}

		void Update()
		{
		}

		public void ClickButton(int index)
		{
			Managers.Roulette.SelectBoardButton(index);
		}

		private void DeadSign(int index)
		{
			_deadSign.SetActive(true);
		}

		private void ChangeButtonSymbol(int index, ESymbolType symbolType)
		{
			_buttonTexts[index].text = ChangeSymbol(symbolType);
		}

		private void ResetButton()
		{
			_deadSign.SetActive(false);
			Managers.Roulette.RefreshRouletteBoard();
			foreach (var text in _buttonTexts)
			{
				text.text = "";
			}
		}

		private void ChangeSymbolUI(ESymbolType symbolType)
		{
			_currentText.text = _nextText.text;
			_nextText.text = ChangeSymbol(symbolType);
		}
		private string ChangeSymbol(ESymbolType symbolType)
		{
			switch (symbolType)
			{
				case ESymbolType.None:
					return "";
				case ESymbolType.Heart:
					return "A";
				case ESymbolType.Diamond:
					return "B";
				case ESymbolType.Clover:
					return "C";
				case ESymbolType.Spade:
					return "D";
			}
			return symbolType.ToString();
		}
	}
}
