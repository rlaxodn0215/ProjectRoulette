using System;
using System.Collections.Generic;
using GoogleSheetsToUnity;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectRoulette
{
	[CreateAssetMenu(fileName = "GoogleSpreadSheetReader", menuName = "Scriptable Objects/GoogleSpreadSheetReader")]
	public class GoogleSpreadSheetReader : ScriptableObject
	{
		[HideInInspector] public readonly string SheetID = "1vozS06evrJUWsLwDRHl-uuSZ1QLOV-By0qt_2cMBO4k";

		[HideInInspector] public readonly string[] WorkSheetList =
		{
			"Item"
		};

		#region Data

		public List<ItemSO> items = new List<ItemSO>();

		#endregion

		public Type UpdateDataType = null;

		public void UpdateData(GstuSpreadSheet ss)
		{
			switch (UpdateDataType)
			{
				case not null when UpdateDataType == typeof(ItemSO):
					UpdateDataInList(ss, items);
					break;
				default:
					break;
			}
		}

		public void SetSOAssetsInList(string type)
		{
			switch (type)
			{
				case nameof(ItemSO):
					items = GetSOAssets<ItemSO>();
					break;
				default:
					break;
			}
		}

		private List<T> GetSOAssets<T>() where T : SOBase
		{
			var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
			var soList = new List<T>();

			foreach (var guid in guids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				var so = AssetDatabase.LoadAssetAtPath<T>(path);
				if (so)
				{
					soList.Add(so);
				}
			}

			return soList;
		}

		private void UpdateDataInList<T>(GstuSpreadSheet ss, List<T> soList) where T : SOBase
		{
			foreach (var row in ss.rows.secondaryKeyLink)
			{
				SetSOAssetsInList(typeof(T).Name);

				T targetSO = null;
				if (!HasSOInList(row.Key, out targetSO))
				{
					Debug.LogWarning("해당 Key에 대응하는 SO가 없습니다 : " + row.Key);
					continue;
				}

				var data = new List<string>();
				foreach (var col in ss.columns.secondaryKeyLink)
				{
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
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(GoogleSpreadSheetReader))]
	public class GoogleSpreadSheetEditor : Editor
	{
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
				_reader.SetSOAssetsInList(_types[_selectedTypeIndex]);
			}

			if (GUILayout.Button("Get All SO Assets"))
			{
				foreach (var type in _types)
				{
					_reader.SetSOAssetsInList(type);
				}
			}

			GUILayout.Label("");
			_selectedWorkSheetIndex =
				EditorGUILayout.Popup("Data Type", _selectedWorkSheetIndex, _reader.WorkSheetList);
			_currentWorkSheet = _reader.WorkSheetList[_selectedWorkSheetIndex];

			if (GUILayout.Button("Pull Data"))
			{
				UpdateData(_currentWorkSheet);
			}

			if (GUILayout.Button("Pull All Data"))
			{
				foreach (var sheet in _reader.WorkSheetList)
				{
					UpdateData(sheet);
				}
			}
		}

		private void UpdateData(string currentWorkSheet)
		{
			switch (currentWorkSheet)
			{
				case "Item":
					SpreadsheetManager.Read(new GSTU_Search(_reader.SheetID, currentWorkSheet), _reader.UpdateData);
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
