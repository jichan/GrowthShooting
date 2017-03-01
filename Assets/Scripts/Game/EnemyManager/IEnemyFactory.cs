using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// EnemyFactoryのインターフェース
/// </summary>
public interface IEnemyFactory
{
	EnemyBase CreateEnemy();
	void SetWave(int num);
	void AddSpawnListener(UnityAction<EnemyBase> callback);
	void RemoveSpawnListener(UnityAction<EnemyBase> callback);
	void ClearSpawnListener();
}
