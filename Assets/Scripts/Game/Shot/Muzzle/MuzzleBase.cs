using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仮想的な銃口を作成するための基底クラス
/// </summary>
public abstract class MuzzleBase : MonoBehaviour, IMuzzle {
	// 発射する弾のプレハブ
	[SerializeField]
	protected BulletBase bulletPrefab;
	// 発射間隔（秒）
	[SerializeField]
	float shotIntervalSecond;

	// 発射する方向
	protected Vector2 shotDirection;

	// 前回発射した時間
	float shotLastTime;

	/// <summary>
	/// 弾を生成する
	/// </summary>
	public void CreateBullet(Transform target = null)
	{
		GameObject bulletObj = Instantiate(bulletPrefab.gameObject);
		BulletBase bulletBase = bulletObj.GetComponent<BulletBase>();
		bulletBase.transform.position = transform.position;
		bulletBase.Initialize(target, transform.forward);
	}

	/// <summary>
	/// 発射時間を更新する
	/// </summary>
	protected void UpdateTimeStamp()
	{
		shotLastTime = Time.time;
	}

	/// <summary>
	/// 前回の発射から充分に時間が経っているかどうかチェックする
	/// </summary>
	/// <returns></returns>
	protected bool CheckReadyToShot()
	{
		return shotLastTime + shotIntervalSecond < Time.time;
	}

	/// <summary>
	/// 発射する方向を表示する
	/// </summary>
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward);
		Gizmos.color = Color.white;
	}
}
