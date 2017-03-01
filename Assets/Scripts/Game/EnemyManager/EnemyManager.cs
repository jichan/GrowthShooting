using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 敵の出現を管理するクラス
/// </summary>
public class EnemyManager : SingletonMono<EnemyManager> {

	// 敵生成クラス
	Dictionary<EnemyKind, IEnemyFactory> enemyFactories;
	// 敵リスト
	Dictionary<EnemyKind, List<EnemyBase>> enemyList;

	protected override void Awake()
	{
		base.Awake();
		enemyFactories = new Dictionary<EnemyKind, IEnemyFactory>();
		enemyList = new Dictionary<EnemyKind, List<EnemyBase>>();

		System.Type kindType = typeof(EnemyKind);
		foreach (EnemyKind kind in System.Enum.GetValues(kindType))
		{
			enemyList.Add(kind, new List<EnemyBase>());
		}

		// ファクトリクラスの初期化
		RegisterEnemyFactory(EnemyKind.Normal, NormalEnemyFactory.Instance);

		enemyFactories[EnemyKind.Normal].SetWave(30);
	}

	/// <summary>
	/// 指定された種類の敵を指定の数だけ出現させる
	/// </summary>
	/// <param name="kind"></param>
	/// <param name="num"></param>
	public void CreateEnemy(EnemyKind kind)
	{
		if(enemyFactories.ContainsKey(kind))
		{
			EnemyBase enemy = enemyFactories[kind].CreateEnemy();
			enemyList[kind].Add(enemy);
		}
	}

	/// <summary>
	/// 指定されたポイントの近くにいる敵を返す
	/// </summary>
	/// <param name="basePoint"></param>
	/// <param name="num"></param>
	/// <returns></returns>
	public SortedList<float, EnemyBase> GetNearEnemies(Vector3 basePoint, int num)
	{
		SortedList<float, EnemyBase> targets = new SortedList<float, EnemyBase>();
		foreach(var list in enemyList.Values)
		{
			foreach(EnemyBase enemy in list)
			{
				float distance = (enemy.transform.position - basePoint).magnitude;
				targets.Add(distance, enemy);
			}
		}
		// 要素を削減
		for(int i = targets.Count - 1; i > num; i--)
		{
			targets.RemoveAt(i);
		}
		return targets;
	}

	/// <summary>
	/// 敵生成ファクトリーを登録
	/// </summary>
	/// <param name="kind"></param>
	/// <param name="factory"></param>
	public void RegisterEnemyFactory(EnemyKind kind, IEnemyFactory factory)
	{
		enemyFactories.Add(kind, factory);

		// コールバック用に種類を保持
		EnemyKind enemyKind = kind;
		factory.AddSpawnListener(
			(createdEnemy) =>
			{
				enemyList[enemyKind].Add(createdEnemy);
			});
	}

	/// <summary>
	/// 敵データを登録する
	/// </summary>
	/// <param name="kind"></param>
	/// <param name="enemy"></param>
	public void RegisterEnemy(EnemyKind kind, EnemyBase enemy)
	{
		enemyList[kind].Add(enemy);
	}

	/// <summary>
	/// 敵情報を削除する
	/// </summary>
	/// <param name="kind"></param>
	/// <param name="enemy"></param>
	public void RemoveEnemy(EnemyKind kind, EnemyBase enemy)
	{
		enemyList[kind].Remove(enemy);
	}

	//敵を生成するファクトリクラスを作る
	//ファクトリクラスにはベースクラスがあり、それをこのクラスで配列で管理したい
	//敵の種類はenumで管理して敵の出現データを作るときにInspectorから設定するような感じ
	//配列とenumはいい感じにコード自動生成で辻褄を合わせる感じで（Dictionary使う？）
	//コードの自動生成はenumの追加や配列の追加などいろいろあるためその辺で活躍しそう
	//実際にNormalEnemyManagerを実装してどのくらいのコード量になるか見てみてから検討がいいかもしれない
	//敵の出現データの作成はUnity上でいい感じにやりたい
	//

}
