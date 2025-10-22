using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using GoogleSheetsToUnity;
using UnityEditor;
using UnityEngine;

namespace ProjectRoulette
{
	[CreateAssetMenu(fileName = "GoogleSpreadSheetManager", menuName = "Scriptable Objects/GoogleSpreadSheetManager")]
	public class GoogleSpreadSheetManager : ScriptableObject
	{
		/// <summary>
		/// 정보를 갱신할 SO 모음
		/// </summary>

		#region Data

		[SerializeField] private List<ItemSO> items = new List<ItemSO>();

		#endregion

		public Type UpdateDataType = null;
		public Type SendDataType = null;

		// TODO: GobalOption으로 수정
		private string TITLE_KEY = "Key";
		private string IGNORE_PREFIX = "#";

		/// <summary>
		/// SO 목록 갱신
		/// </summary>
		/// <param name="type"></param>
		public void GetSOAssets(string type)
		{
			switch (type)
			{
				case nameof(ItemSO):
					items = GetSOAssets<ItemSO>();
					break;
				default:
					Debug.LogError("No such type : " + type);
					break;
			}
		}

		/// <summary>
		/// 데이터 갱신
		/// </summary>
		/// <param name="ss"></param>
		public void UpdateData(GstuSpreadSheet ss)
		{
			switch (UpdateDataType)
			{
				case not null when UpdateDataType == typeof(ItemSO):
					RewriteTargetScript(nameof(ItemSO), ss); // Script 갱신
					UpdateData(ss, items); // 데이터 갱신
					break;
				default:
					Debug.LogError("No such type : " + UpdateDataType);
					break;
			}
		}

		/// <summary>
		/// 데이어 sheet로 보내기
		/// </summary>
		/// <param name="ss"></param>
		public void SendData(GstuSpreadSheet ss)
		{
			switch (UpdateDataType)
			{
				case not null when SendDataType == typeof(ItemSO):
					SendData(ss, items); // 데이터 전송
					break;
				default:
					Debug.LogError("No such type : " + SendDataType);
					break;
			}
		}

		/// <summary>
		/// 데이터 갱신
		/// </summary>
		/// <param name="ss"></param>
		/// <param name="soList"> 갱신할  데이터 리스트 </param>
		/// <typeparam name="T"></typeparam>
		private void UpdateData<T>(GstuSpreadSheet ss, List<T> soList) where T : SOBase
		{
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
		/// 데이터 전송하는 함수
		/// </summary>
		/// <param name="ss"></param>
		/// <param name="soList"> 전송 대상 List</param>
		/// <typeparam name="T"></typeparam>
		private void SendData<T>(GstuSpreadSheet ss, List<T> soList) where T : SOBase
		{
			// BatchRequestBody updateRequest = new BatchRequestBody();
			// updateRequest.Add(ss[animal.name, "Health"].AddCellToBatchUpdate(animal.associatedSheet, animal.associatedWorksheet, animal.health.ToString()));
			// updateRequest.Add(ss[animal.name, "Defence"].AddCellToBatchUpdate(animal.associatedSheet, animal.associatedWorksheet, animal.health.ToString()));
			// updateRequest.Add(ss[animal.name, "Attack"].AddCellToBatchUpdate(animal.associatedSheet, animal.associatedWorksheet, animal.health.ToString()));
			// updateRequest.Send(animal.associatedSheet, animal.associatedWorksheet, null);
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(GoogleSpreadSheetManager))]
	public class GoogleSpreadSheetEditor : Editor
	{
		private static readonly string SheetID = "1vozS06evrJUWsLwDRHl-uuSZ1QLOV-By0qt_2cMBO4k";

		private static readonly string[] WorkSheetList =
		{
			"Item"
		};

		private GoogleSpreadSheetManager _manager;
		private int _selectedWorkSheetIndex = 0;
		private string _currentWorkSheet = "";

		private readonly string[] _types = { nameof(ItemSO) };
		private int _selectedTypeIndex = 0;

		private void OnEnable()
		{
			_manager = (GoogleSpreadSheetManager)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			// GUILayout.Label("");
			// _selectedTypeIndex = EditorGUILayout.Popup("SO Type", _selectedTypeIndex, _types);
			//
			// if (GUILayout.Button("Get SO Assets"))
			// {
			// 	_reader.RefreshSOAssets(_types[_selectedTypeIndex]);
			// }
			//
			//
			// GUILayout.Label("");
			// _selectedWorkSheetIndex =
			// 	EditorGUILayout.Popup("Data Type", _selectedWorkSheetIndex, WorkSheetList);
			// _currentWorkSheet = WorkSheetList[_selectedWorkSheetIndex];
			//
			// if (GUILayout.Button("Refresh Data"))
			// {
			// 	RefreshData(_currentWorkSheet);
			// }

			if (GUILayout.Button("Get All SO Assets"))
			{
				foreach (var type in _types)
				{
					_manager.GetSOAssets(type);
				}

				Debug.Log("[GoogleSpreadSheetManager] : Get All SO Assets Finished");
			}

			if (GUILayout.Button("Pull All Data"))
			{
				foreach (var sheet in WorkSheetList)
				{
					PullData(sheet);
				}
				
				Debug.Log("[GoogleSpreadSheetManager] : Pull All Data Finished");
			}

			if (GUILayout.Button("Push All Data"))
			{
				Debug.Log("나중에 만들어 주세용~~^^");
				// foreach (var sheet in WorkSheetList)
				// {
				// 	PushData(sheet);
				// }
				// Debug.Log("[GoogleSpreadSheetManager] : Push All Data Finished");
			}
		}

		private void PullData(string currentWorkSheet)
		{
			switch (currentWorkSheet)
			{
				case "Item":
					SpreadsheetManager.Read(new GSTU_Search(SheetID, currentWorkSheet), _manager.UpdateData);
					_manager.UpdateDataType = typeof(ItemSO);
					break;
				default:
					Debug.LogError("No such sheet : " + currentWorkSheet);
					break;
			}

			EditorUtility.SetDirty(target);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		private void PushData(string currentWorkSheet)
		{
			switch (currentWorkSheet)
			{
				case "Item":
					// 데이터 한 번 pull 받은 다음 push한다.
					SpreadsheetManager.Read(new GSTU_Search(SheetID, currentWorkSheet), _manager.SendData);
					_manager.SendDataType = typeof(ItemSO);
					break;
				default:
					//Debug.LogError("No such sheet : " + currentWorkSheet);
					break;
			}

			EditorUtility.SetDirty(target);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
#endif
}
