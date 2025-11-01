using System.Collections.Generic;
using UnityEngine;

namespace ProjectRoulette
{
	public class ProbabilityUtility
	{
		private static readonly int ProbabilityMaxCount = Managers.Data.GetGlobalValueData().ProbabilityMaxCount;
		public static int GetRandomProbability(List<int> probabilityList)
		{
			var sum = 0;
			foreach (var item in probabilityList)
			{
				sum += item;
			}

			if (sum != 1000)
			{
				Debug.LogError("Sum of probability is not 1000");
				return -1;
			}

			sum = 0;
			var randomNum = Random.Range(1, ProbabilityMaxCount + 1);
			for (var i = 0; i < probabilityList.Count; i++)
			{
				sum += probabilityList[i];
				if (randomNum <= sum)
				{
					return i;
				}
			}

			return -1;
		}

		public static bool IsChanceSuccess(int probability)
		{
			return Random.Range(0, ProbabilityMaxCount) < probability;
		}
	}
}
