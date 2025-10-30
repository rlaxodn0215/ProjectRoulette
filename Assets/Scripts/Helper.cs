using System;
using System.Reflection;
using UnityEngine;

namespace ProjectRoulette
{
	public static class Helper
	{
		/// <summary>
		/// 제너릭 메서드를 Type으로 실행합니다.
		/// </summary>
		public static object InvokeGeneric<TTarget>(string methodName, Type genericType, params object[] parameters)
		{
			// 예: typeof(TTarget) 안의 methodName<T> 를 genericType으로 실행
			var method = typeof(TTarget).GetMethod(methodName,
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			if (method == null)
			{
				Debug.LogError($"❌ 메서드 '{methodName}'를 {typeof(TTarget).Name}에서 찾을 수 없습니다.");
				return null;
			}

			var genericMethod = method.MakeGenericMethod(genericType);
			return genericMethod.Invoke(null, parameters); // static 메서드라면 null
		}
	}
}
