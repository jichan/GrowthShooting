using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 敵を作成するための基底クラス
/// </summary>
public abstract class EnemyFactoryBase<T> : SingletonMono<T>, IEnemyFactory where T : EnemyFactoryBase<T>
{
	[SerializeField]
	protected GameObject enemyPrefab;
	[SerializeField]
	protected UnityAction<EnemyBase> spawnCallback;

	// このクラスで作成した敵リスト
	protected List<EnemyBase> createdEnemyList;

	public abstract EnemyBase CreateEnemy();

	/// <summary>
	/// 敵を出現させる処理
	/// </summary>
	/// <returns></returns>
	protected abstract IEnumerator SpawnEnemy(int num);

	/// <summary>
	/// ウェーブの設定をする
	/// </summary>
	/// <param name="num">敵を出す数</param>
	public void SetWave(int num)
	{
		StartCoroutine(SpawnEnemy(num));
	}

	/// <summary>
	/// 敵が出現したときのコールバックを設定する
	/// </summary>
	public void AddSpawnListener(UnityAction<EnemyBase> callback)
	{
		spawnCallback += callback;
	}

	/// <summary>
	/// 敵が出現したときのコールバックを削除する
	/// </summary>
	/// <param name="callback"></param>
	public void RemoveSpawnListener(UnityAction<EnemyBase> callback)
	{
		spawnCallback -= callback;
	}

	/// <summary>
	/// 敵が出現したときのコールバックをすべて削除する
	/// </summary>
	public void ClearSpawnListener()
	{
		spawnCallback = new UnityAction<EnemyBase>((enemy) => { });
	}
}
