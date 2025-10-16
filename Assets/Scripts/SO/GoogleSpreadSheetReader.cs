using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using GoogleSheetsToUnity;
using UnityEditor;
using UnityEngine;

namespace ProjectRoulette
{
	[CreateAssetMenu(fileName = "GoogleSpreadSheetReader", menuName = "Scriptable Objects/GoogleSpreadSheetReader")]
	public class GoogleSpreadSheetReader : ScriptableObject
	{
		/// <summary>
		/// 정보를 갱신할 SO 모음
		/// </summary>

		#region Data

		public List<ItemSO> items = new List<ItemSO>();

		#endregion

		[HideInInspector] public Type UpdateDataType = null;

		// TODO: GobalOption으로 수정
		public static string TITLE_KEY = "Key";
		public static string IGNORE_PREFIX = "#";

		/// <summary>
		/// 데이터 갱신
		/// </summary>
		/// <param name="ss"></param>
		public void UpdateData(GstuSpreadSheet ss)
		{
			switch (UpdateDataType)
			{
				case not null when UpdateDataType == typeof(ItemSO):
					UpdateData(ss, items);
					break;
				default:
					Debug.LogError("No such type");
					break;
			}
		}

		/// <summary>
		/// SO 목록 갱신
		/// </summary>
		/// <param name="type"></param>
		public void RefreshSOAssets(string type)
		{
			switch (type)
			{
				case nameof(ItemSO):
					items = GetSOAssets<ItemSO>();
					break;
				default:
					Debug.LogError("No such type");
					break;
			}
		}

		/// <summary>
		/// 해당 타입의 SO를 모두 가져오는 함수
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		private List<T> GetSOAssets<T>() where T : SOBase
		{
			var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
			if (guids.Length == 0)
			{
				Debug.LogError("Assets not found - name : " + typeof(T).Name);
				return null;
			}

			var soList = new List<T>();
			foreach (var guid in guids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				if (path.Length == 0)
				{
					Debug.LogError("Assets not found - guid : " + guid);
					continue;
				}

				var so = AssetDatabase.LoadAssetAtPath<T>(path);
				if (so)
				{
					soList.Add(so);
				}
			}

			return soList;
		}

		/// <summary>
		/// 데이터 갱신
		/// </summary>
		/// <param name="ss"></param>
		/// <param name="soList"> 갱신할  데이터 리스트 </param>
		/// <typeparam name="T"></typeparam>
		private void UpdateData<T>(GstuSpreadSheet ss, List<T> soList) where T : SOBase
		{
			// Script 갱신
			var typeName = typeof(T).Name;
			RewriteTargetScript(typeName, ss);
			RefreshSOAssets(typeName);

			var ssRow = ss.rows.secondaryKeyLink;
			var ssCol = ss.columns.secondaryKeyLink;
			if (ssRow == null || ssCol == null)
			{
				Debug.LogError("Cannot find data");
				return;
			}

			foreach (var row in ssRow)
			{
				if (!HasSOInList(row.Key, out var targetSO))
				{
					if (row.Key != TITLE_KEY)
					{
						Debug.LogWarning("해당 Key에 대응하는 SO가 없습니다 : " + row.Key);
					}

					continue;
				}

				var data = new List<string>();
				foreach (var col in ssCol)
				{
					// #으로 시작하면 무시
					if (col.Key.StartsWith(IGNORE_PREFIX))
					{
						continue;
					}

					// Key 제외
					if (col.Key.Equals(TITLE_KEY, StringComparison.OrdinalIgnoreCase))
					{
						continue;
					}

					data.Add(ss[row.Key, col.Key].value);
				}

				targetSO.UpdateData(data);
			}

			return;

			bool HasSOInList(string key, out T targetSO)
			{
				foreach (var so in soList)
				{
					if (so.Key.ToString() != key) continue;
					targetSO = so;
					return true;
				}

				targetSO = null;
				return false;
			}
		}

		/// <summary>
		/// Sheet에 맞게 Script 갱신
		/// </summary>
		/// <param name="className"></param>
		/// <param name="ss"></param>
		private void RewriteTargetScript(string className, GstuSpreadSheet ss)
		{
			var data = new List<string>();
			var ssCol = ss.columns.secondaryKeyLink;
			if (ssCol == null)
			{
				Debug.LogError("Cannot find ss.columns.secondaryKeyLink");
				return;
			}

			foreach (var col in ssCol)
			{
				data.Add(ss[TITLE_KEY, col.Key].value);
			}

			var scriptPath = GetScriptPathByClassName(className);
			File.WriteAllText(scriptPath, SOBase.GetNewScript(className, data));
			AssetDatabase.Refresh();

			return;

			string GetScriptPathByClassName(string className)
			{
				var scripts = Resources.FindObjectsOfTypeAll<MonoScript>();
				if (scripts == null)
				{
					Debug.LogError("Scripts not found");
					return null;
				}

				foreach (var script in scripts)
				{
					if (script && script.name == className)
					{
						return AssetDatabase.GetAssetPath(script);
					}
				}

				return null; // 못 찾은 경우
			}
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(GoogleSpreadSheetReader))]
	public class GoogleSpreadSheetEditor : Editor
	{
		private static readonly string SheetID = "1vozS06evrJUWsLwDRHl-uuSZ1QLOV-By0qt_2cMBO4k";

		private static readonly string[] WorkSheetList =
		{
			"Item"
		};

		private GoogleSpreadSheetReader _reader;
		private int _selectedWorkSheetIndex = 0;
		private string _currentWorkSheet = "";

		private readonly string[] _types = { nameof(ItemSO) };
		private int _selectedTypeIndex = 0;

		private void OnEnable()
		{
			_reader = (GoogleSpreadSheetReader)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.Label("");
			_selectedTypeIndex = EditorGUILayout.Popup("SO Type", _selectedTypeIndex, _types);

			if (GUILayout.Button("Get SO Assets"))
			{
				_reader.RefreshSOAssets(_types[_selectedTypeIndex]);
			}

			if (GUILayout.Button("Get All SO Assets"))
			{
				foreach (var type in _types)
				{
					_reader.RefreshSOAssets(type);
				}
			}

			GUILayout.Label("");
			_selectedWorkSheetIndex =
				EditorGUILayout.Popup("Data Type", _selectedWorkSheetIndex, WorkSheetList);
			_currentWorkSheet = WorkSheetList[_selectedWorkSheetIndex];

			if (GUILayout.Button("Refresh Data"))
			{
				RefreshData(_currentWorkSheet);
			}

			if (GUILayout.Button("Refresh All Data"))
			{
				foreach (var sheet in WorkSheetList)
				{
					RefreshData(sheet);
				}
			}
		}

		private void RefreshData(string currentWorkSheet)
		{
			switch (currentWorkSheet)
			{
				case "Item":
					SpreadsheetManager.Read(new GSTU_Search(SheetID, currentWorkSheet), _reader.UpdateData);
					_reader.UpdateDataType = typeof(ItemSO);
					break;
				default:
					Debug.LogError("No such sheet");
					break;
			}

			EditorUtility.SetDirty(target);
		}
	}

#endif
}
