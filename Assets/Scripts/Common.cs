using UnityEngine;

namespace ProjectRoulette
{
	public enum ESymbolType
	{
		None,
		A,
		B,
		C,
		D,
		E,
		Max,
	}

	public enum EPatternType
	{
		None,
		Width3,
		Width4,
		Width5,
		Height3,
		Height4,
		Max,
	}

	public enum EItemUseType
	{
		None,
		Equip,
		Instant,
		Constant,
		Max,
	}

	// Enum 값 = 아이템 Key 값
	public enum EItem
	{
		None,
		TestItem = 1,
		Max,
	}
	
	// Enum 값 = Symbol 데이터 Key 값
	public enum ESymbolData
	{
		None,
		SymbolChance = 1,
		SymbolPoint = 2,
		Max,
	}
}
