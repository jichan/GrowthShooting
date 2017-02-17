using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法の矢てきなやつ
/// </summary>
public class MagicArrowBullet : BulletBase {
	// 弾の初期角度
	// (0,0,-1)に対するランダムな角度の範囲
	[SerializeField]
	float angleRange;
	// 弾の回転速度
	[SerializeField]
	float rotationSpeed;

	protected override void InitializeInternal(Transform target)
	{
		float initAngle = UnityEngine.Random.Range(angleRange * -0.5f, angleRange * 0.5f);
		float initRadian = initAngle * Mathf.Deg2Rad;
		direction = new Vector3(-Mathf.Sin(initRadian), 0, -Mathf.Cos(initRadian));
	}

	protected override void SetMoveDirection()
	{
		// 弾からターゲットへの方向ベクトルを算出
		Vector3 targetDir = (target.position - transform.position).normalized;
		// 現在の方向ベクトルと足し合わせる
		direction += targetDir * rotationSpeed;
		direction.Normalize();
		// 進行方向のほうへ向かせる
		transform.LookAt(transform.position + direction, transform.up);
		// カプセルプリミティブの頭を進行方向へ向けるように修正
		transform.rotation *= Quaternion.AngleAxis(90.0f, Vector3.right);
	}
}
