using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

/// <summary>
/// スクリプトの自動生成を行うためのサポートクラス
/// </summary>
public class AutoScriptCreator {
	//無効な文字を管理する配列
	static readonly string[] invalidChars =
	{
		" ", "!", "\"", "#", "$",
		"%", "&", "\'", "(", ")",
		"-", "=", "^",  "~", "\\",
		"|", "[", "{",  "@", "`",
		"]", "}", ":",  "*", ";",
		"+", "/", "?",  ".", ">",
		",", "<", " "
  };
	// テンプレートファイルが格納されるフォルダへのパス
	static readonly string templateFolder = Application.dataPath + "/Scripts/Editor/GenerateSourceTemplate/";
	// 自動生成されたソースコードを保存するフォルダ
	static readonly string saveFolder = Application.dataPath + "/GeneratedScripts/";

	// ファイルに書き込むための内容
	string textContent;
	
	public AutoScriptCreator()
	{
		textContent = "";
	}

	/// <summary>
	/// 原型となるファイルを読み込む
	/// </summary>
	/// <param name="filename">読み込むファイル名</param>
	public void LoadFile(string filename)
	{
		StreamReader sourceTextFile = File.OpenText(Path.Combine(templateFolder, filename));
		textContent = sourceTextFile.ReadToEnd();
	}

	/// <summary>
	/// ソースコードの置換シンボルを指定されたテキストに置き換える
	/// </summary>
	/// <param name="replaceSymbol"></param>
	/// <param name="replaceText"></param>
	public void ReplaceText(string replaceSymbol, string replaceText)
	{
		textContent = textContent.Replace(replaceSymbol, replaceText);
	}

	/// <summary>
	/// ファイルを保存する
	/// </summary>
	/// <param name="filename"></param>
	public void Save(string filename)
	{
		string path = Path.Combine(saveFolder, filename);
		File.WriteAllText(path, textContent);

		AssetDatabase.Refresh();
	}

	/// <summary>
	/// publicの変数宣言の文字列を作成する
	/// </summary>
	/// <typeparam name="T">変数の型</typeparam>
	/// <param name="variableName">変数名</param>
	/// <returns></returns>
	public static string CreatePublicVariable<T>(string variableName)
	{
		variableName = removeInvalidChars(variableName);
		return "\tpublic " + typeof(T).Name + " " + variableName + ";\r\n";
	}

	/// <summary>
	/// publicの変数宣言の文字列を作成する
	/// </summary>
	/// <typeparam name="T">変数の型</typeparam>
	/// <param name="variableName">変数名</param>
	/// <returns></returns>
	public static string CreatePublicVariable<T>(string variableName, string initValue)
	{
		variableName = removeInvalidChars(variableName);
		string typeName = typeof(T).Name;
		typeName = fixBuiltinTypeName(typeName);
		return "\tpublic " + typeName + " " + variableName + " = " + initValue + ";\r\n";
	}

	/// <summary>
	/// publicの変数宣言の文字列を作成する
	/// </summary>
	/// <typeparam name="T">変数の型</typeparam>
	/// <param name="variableName">変数名</param>
	/// <returns></returns>
	public static string CreatePublicStaticReadonlyVariable<T>(string variableName, string initValue)
	{
		variableName = removeInvalidChars(variableName);
		string typeName = typeof(T).Name;
		typeName = fixBuiltinTypeName(typeName);
		return "\tpublic static readonly " + typeName + " " + variableName + " = " + initValue + ";\r\n";
	}

	/// <summary>
	/// enumの定数宣言の文字列を作成する
	/// </summary>
	/// <param name="constantName"></param>
	/// <returns></returns>
	public static string CreateEnumConstant(string constantName)
	{
		constantName = removeInvalidChars(constantName);
		return "\t" + constantName + ",\r\n";
	}

	/// <summary>
	/// enumの初期値ありの定数宣言の文字列を作成する
	/// </summary>
	/// <param name="constantName"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string CreateEnumConstant(string constantName, int value)
	{
		constantName = removeInvalidChars(constantName);
		return "\t" + constantName + " = " + value + ",\r\n";
	}

	/// <summary>
	/// 無効な文字を削除
	/// </summary>
	static string removeInvalidChars(string str)
	{
		System.Array.ForEach(invalidChars, c => str = str.Replace(c, string.Empty));
		return str;
	}

	/// <summary>
	/// 整数リテラルの作成
	/// </summary>
	/// <param name="n"></param>
	/// <returns></returns>
	public static string CreateLiteral(int n)
	{
		return n.ToString();
	}

	/// <summary>
	/// floatリテラルの作成
	/// </summary>
	/// <param name="f"></param>
	/// <returns></returns>
	public static string CreateLiteral(float f)
	{
		return f + "f";
	}

	/// <summary>
	/// doubleリテラルの作成
	/// </summary>
	/// <param name="d"></param>
	/// <returns></returns>
	public static string CreateLiteral(double d)
	{
		return d.ToString();
	}

	/// <summary>
	/// 文字列リテラルの作成
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	public static string CreateLiteral(string s)
	{
		return '\"' + s + '\"';
	}

	/// <summary>
	/// 組み込み型のクラス名を変換する
	/// </summary>
	/// <param name="typeName"></param>
	/// <returns></returns>
	static string fixBuiltinTypeName(string typeName)
	{
		switch(typeName)
		{
			case "String":
				return "string";
			case "Int32":
				return "int";

			default:
				return typeName;
		}
	}
}
