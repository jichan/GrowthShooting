using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScreenshotCapture {

	[MenuItem("Edit/CaptureScreenshot #%F12")]
	static void Capture()
	{
		// 現在時刻からファイル名を決定
		var folderName = "ScreenShot";
		var savefolder = Application.dataPath + "/../" + folderName;
		var filename = folderName + "/" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";

		if(!System.IO.Directory.Exists(savefolder))
		{
			System.IO.Directory.CreateDirectory(savefolder);
		}

		// キャプチャを撮る
		Application.CaptureScreenshot(filename); // ← GameViewにフォーカスがない場合、この時点では撮られない
		
		// GameViewを取得してくる
		var assembly = typeof(EditorWindow).Assembly;
		var type = assembly.GetType("UnityEditor.GameView");
		var gameview = EditorWindow.GetWindow(type);
		// GameViewを再描画
		gameview.Repaint();

		System.Diagnostics.Process.Start(savefolder);
	}
}
