using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// タグとレイヤーをenum値へ自動生成する
/// </summary>
public class TagAndLayerEditor : AssetPostprocessor
{
	// 変更を監視するディレクトリ名
	private const string targetDirectoryName = "ProjectSettings";
	// コマンド名
	private const string commandName = "Tools/Create/Setting Class";

	// シーン名を定義するファイルのテンプレートファイル
	static readonly string sceneNameTemplateFilePath = "SceneName.cs.txt";
	// タグ名を定義するファイルのテンプレートファイル
	static readonly string tagTemplateFilePath = "Tag.cs.txt";
	// レイヤー名を定義するファイルのテンプレートファイル
	static readonly string layerTemplateFilePath = "Layer.cs.txt";
	// ソーティングレイヤー名を定義するファイルのテンプレートファイル
	static readonly string sortingLayerNameTemplateFilePath = "SortingLayerName.cs.txt";

	// シーン名を定義するファイルの保存先
	static readonly string sceneNameSavePath = "SceneName.cs";
	// タグ名を定義するファイルの保存先
	static readonly string tagSavePath = "Tag.cs";
	// レイヤー名を定義するファイルの保存先
	static readonly string layerSavePath = "Layer.cs";
	// ソーティングレイヤー名を定義するファイルの保存先
	static readonly string sortingLayerNameSavePath = "SortingLayerName.cs";

	/// <summary>
	/// 入力されたassetsの中に、ディレクトリのパスがdirectoryNameの物はあるか
	/// </summary>
	protected static bool ExistsDirectoryInAssets(List<string[]> assetsList, List<string> targetDirectoryNameList)
	{
		
		return assetsList
			.Any(assets => assets                                       //入力されたassetsListに以下の条件を満たすか要素が含まれているか判定
			 .Select(asset => Path.GetDirectoryName(asset))   //assetsに含まれているファイルのディレクトリ名だけをリストにして取得
			 .Intersect(targetDirectoryNameList)                         //上記のリストと入力されたディレクトリ名のリストの一致している物のリストを取得
			 .Count() > 0);                                              //一致している物があるか

	}

	public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		List<string[]> assetsList = new List<string[]>(){
			importedAssets
		};

		List<string> targetDirectoryNameList = new List<string>(){
			targetDirectoryName
		};

		if (ExistsDirectoryInAssets(assetsList, targetDirectoryNameList))
		{
			Create();
		}
	}

	//スクリプトを作成します
	[MenuItem(commandName)]
	private static void Create()
	{

		//タグ
		AutoScriptCreator tagCreator = new AutoScriptCreator();
		tagCreator.LoadFile(tagTemplateFilePath);
		string tagText = "";
		foreach(string tag in InternalEditorUtility.tags)
		{
			tagText += AutoScriptCreator.CreatePublicStaticReadonlyVariable<string>(tag, AutoScriptCreator.CreateLiteral(tag));
		}
		tagCreator.ReplaceText("#TAGS#", tagText);
		tagCreator.Save(tagSavePath);

		//シーン
		AutoScriptCreator sceneNameCreator = new AutoScriptCreator();
		sceneNameCreator.LoadFile(sceneNameTemplateFilePath);
		string sceneNameText = "";
		foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
		{
			string sceneName = Path.GetFileNameWithoutExtension(scene.path);
			sceneNameText += AutoScriptCreator.CreatePublicStaticReadonlyVariable<string>(sceneName, AutoScriptCreator.CreateLiteral(sceneName));
		}
		sceneNameCreator.ReplaceText("#SCENES#", sceneNameText);
		sceneNameCreator.Save(sceneNameSavePath);

		// レイヤー
		AutoScriptCreator layerCreator = new AutoScriptCreator();
		layerCreator.LoadFile(layerTemplateFilePath);
		string layerNameText = "";
		foreach (string layer in InternalEditorUtility.layers)
		{
			int layerNum = LayerMask.NameToLayer(layer);
			layerNameText += AutoScriptCreator.CreatePublicStaticReadonlyVariable<int>(layer, layerNum.ToString());
		}
		layerCreator.ReplaceText("#LAYERS#", layerNameText);
		layerCreator.Save(layerSavePath);

		// ソーティングレイヤー
		AutoScriptCreator sortingLayerNameCreator = new AutoScriptCreator();
		sortingLayerNameCreator.LoadFile(sortingLayerNameTemplateFilePath);
		string sortingLayerNameText = "";
		foreach (SortingLayer sortingLayer in SortingLayer.layers)
		{
			sortingLayerNameText += AutoScriptCreator.CreateEnumConstant(sortingLayer.name, sortingLayer.value);
		}
		sortingLayerNameCreator.ReplaceText("#SORTINGLAYERS#", sortingLayerNameText);
		sortingLayerNameCreator.Save(sortingLayerNameSavePath);

	}

	/// <summary>
	/// ソーティングレイヤーの名前一覧を取得
	/// </summary>
	/// <returns></returns>
	static string[] GetSortingLayerNames()
	{
		Type internalEditorUtilityType = typeof(InternalEditorUtility);
		PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
		return (string[])sortingLayersProperty.GetValue(null, new object[0]);
	}
}
