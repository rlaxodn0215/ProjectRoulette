using UnityEngine;

namespace ProjectRoulette
{
	public enum ESymbol
	{
		None,
		A,
		B,
		C,
		D,
		Block,
		Mimic,
		All,
		Max,
	}

	// Enum 값 = 패턴 Key 값
	public enum EPattern
	{
		None,
		Single,
		Row3,
		Row4,
		Row5,
		Col3,
		Col4,
		Col5,
		Diag3_Main,
		Diag4_Main,
		Diag5_Main,
		Diag3_Anti,
		Diag4_Anti,
		Diag5_Anti,
		LShape,
		LShape_Anti,
		TShape,
		Cross,
		Square_2x2,
		ZShape,
		SShape,
		Arc_Left,
		Arc_Right,
		Plus_Small,
		WShape,
		All,
		Max,
	}

	// Enum 값 = 아이템 Key 값
	public enum EItem
	{
		None,
		A,
		B,
		C,
		D,
		Block,
		Mimic,
		Single,
		Row3,
		Row4,
		Row5,
		Col3,
		Col4,
		Col5,
		Diag3_Main,
		Diag4_Main,
		Diag5_Main,
		Diag3_Anti,
		Diag4_Anti,
		Diag5_Anti,
		LShape,
		LShape_Anti,
		TShape,
		Cross,
		Square_2x2,
		ZShape,
		SShape,
		Arc_Left,
		Arc_Right,
		Plus_Small,
		WShape,
		Apple,
		Max,
	}

	public enum EItemType
	{
		CreateSymbol,
		CreatePattern,
		Upgrade
	}

	public enum EItemSkill
	{
		None,
		BlocSlot,
		Mimic,
		UpSymbolChance,
		UpSymbolValue,
		UpPatternChance,
		UpPatternValue
	}
}
