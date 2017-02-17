using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 作業時間の計測を行う
/// </summary>
[InitializeOnLoad]
public static class WorkTimeMeasure {
	// セーブに使用するためのキー
	static readonly string configKey = "WorkTime";
	// 時間を記録する間隔(秒)
	static readonly double saveInterval = 60.0;
	// 累計作業時間(秒)
	static double totalWorkTime;
	// 前回セーブした時間
	static double savedTime;

	static WorkTimeMeasure()
	{
		EditorApplication.update = Update;
		savedTime = 0.0;

		// 作業時間の読み込み
		string time = EditorUserSettings.GetConfigValue(configKey);
		if(!double.TryParse(time, out totalWorkTime))
		{
			totalWorkTime = 0.0;
			SaveTime(0);
		}
	}
	
	static void Update()
	{
		if(EditorApplication.timeSinceStartup - savedTime > saveInterval)
		{
			totalWorkTime += saveInterval;
			SaveTime(totalWorkTime);
			ShowWorkingTime();
		}
	}

	/// <summary>
	/// 時間を記録する
	/// </summary>
	/// <param name="totalTime"></param>
	static void SaveTime(double totalTime)
	{
		savedTime = EditorApplication.timeSinceStartup;
		EditorUserSettings.SetConfigValue(configKey, totalTime.ToString());
	}

	/// <summary>
	/// ログに現在の累計作業時間を表示する
	/// </summary>
    [MenuItem("Edit/WorkTime/Show")]
	static void ShowWorkingTime()
	{
		double hour = System.Math.Floor(totalWorkTime / 3600.0);
		double minute = System.Math.Floor((totalWorkTime / 60.0) % 60.0);
		System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
		Debug.Log("現在の作業時間は " +
			hour.ToString() + "時間" +
			minute.ToString() + "分です。" +
			"起動後" + System.Math.Floor(EditorApplication.timeSinceStartup) + "秒 " +
			System.DateTime.Now.ToString("F", ci));
	}

	/// <summary>
	/// 作業時間を初期化する
	/// </summary>
    [MenuItem("Edit/WorkTime/Clear")]
	static void ClearWorkingTime()
	{
		if(EditorUtility.DisplayDialog("確認", "作業時間をクリアしてもよろしいですか？", "はい", "いいえ"))
		{
			totalWorkTime = 0;
			SaveTime(0);
		}
	}
}
