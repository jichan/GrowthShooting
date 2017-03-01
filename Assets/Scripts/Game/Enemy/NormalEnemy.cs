using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通の雑魚敵
/// </summary>
public class NormalEnemy : EnemyBase {
	// 出現位置のZ軸
	[SerializeField]
	float spawnDepth;
	// 出現位置の幅
	[SerializeField]
	float spawnWidth;
	// 向かう場所の範囲
	[SerializeField]
	Rect arrivalPoint;
	// 目的地までの距離の変遷
	[SerializeField]
	AnimationCurve moveDistanceAnimation;
	// 目的地の辿り着く時間
	[SerializeField]
	float arrivalTime;

	// 初期位置
	Vector3 defaultPoint;
	// 目的地
	Vector3 destination;
	// 目的地までの距離の初期値
	float destinationDistance;
	// 初期位置から目的地までのベクトル
	Vector3 moveVector;

	protected override void InitializeInternal()
	{
		base.InitializeInternal();

		// 出現する位置をランダムで決める
		float spawnX = UnityEngine.Random.Range(spawnWidth * -0.5f, spawnWidth * 0.5f);
		transform.position = new Vector3(spawnX, 0, spawnDepth);
		defaultPoint = transform.position;

		// 向かう場所をランダムで決める
		Vector3 point = new Vector3();
		point.x = UnityEngine.Random.Range(arrivalPoint.xMin, arrivalPoint.xMax) - arrivalPoint.size.x * 0.5f;
		point.z = UnityEngine.Random.Range(arrivalPoint.yMin, arrivalPoint.yMax) - arrivalPoint.size.y * 0.5f;
		destination = point;
		// 目的地への方向ベクトル
		moveVector = destination - transform.position;
		moveVector.Normalize();
		
		// 距離を算出する
		destinationDistance = Vector3.Distance(destination, transform.position);
	}

	protected override IEnumerator Move()
	{
		// 目的地への移動アニメーション
		while(timeAfterSpawn < arrivalTime)
		{
			transform.position = defaultPoint + moveVector * destinationDistance * moveDistanceAnimation.Evaluate(timeAfterSpawn);
			yield return null;
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(new Vector3(spawnWidth * -0.5f, 0, spawnDepth), new Vector3(spawnWidth * 0.5f, 0, spawnDepth));
		Gizmos.DrawWireCube(new Vector3(arrivalPoint.x, 0, arrivalPoint.y), new Vector3(arrivalPoint.size.x, 1.0f, arrivalPoint.size.y));
	}
}
