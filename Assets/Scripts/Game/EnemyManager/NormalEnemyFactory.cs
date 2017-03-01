using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通常の敵を作成するためのクラス
/// </summary>
public class NormalEnemyFactory : EnemyFactoryBase<NormalEnemyFactory>
{
	// 出現間隔
	readonly float spawnInterval = 0.2f;

	public override EnemyBase CreateEnemy()
	{
		GameObject obj = Instantiate(enemyPrefab);
		EnemyBase enemy = obj.GetComponent<EnemyBase>();
		spawnCallback.Invoke(enemy);
		return enemy;
	}

	/// <summary>
	/// 敵を出現させる
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	protected override IEnumerator SpawnEnemy(int num)
	{
		for (int i = 0; i < num; i++)
		{
			CreateEnemy();
			yield return new WaitForSeconds(spawnInterval);
		}
		yield return null;
	}
}
