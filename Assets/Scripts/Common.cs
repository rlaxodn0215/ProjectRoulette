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

	public enum EPatternType
	{
		Width3,
		Width4,
		Width5,
		Height3,
		Height4,
		Max,
		None
	}

	public enum EItemUseType
	{
		Equip,
		Instant
	}

	// Enum 값 = 아이템 Key 값
	public enum EItem
	{
		TestItem = 1,
	}
}
