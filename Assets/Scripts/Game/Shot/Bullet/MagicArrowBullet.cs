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
	// パーティクルシステム
	[SerializeField]
	ParticleSystem particle;
	// 速度変化のカーブ
	[SerializeField]
	AnimationCurve speedCurve;

	// 弾が発射されてからの時間
	float birthTime;
	// 弾速の初期値
	float initSpeed;

	protected override void InitializeInternal(Transform target)
	{
		birthTime = 0.0f;
		initSpeed = speed;
		// 下180度のランダムな方向に飛ばす
		float initAngle = UnityEngine.Random.Range(angleRange * -0.5f, angleRange * 0.5f);
		float initRadian = initAngle * Mathf.Deg2Rad;
		direction = new Vector3(-Mathf.Sin(initRadian), 0, -Mathf.Cos(initRadian));

		StartCoroutine(MoveCoroutine());
	}

	protected override void SetMoveDirection()
	{
		speed = speedCurve.Evaluate(birthTime) * initSpeed;
		birthTime += Time.deltaTime;

		// 進行方向のほうへ向かせる
		transform.LookAt(transform.position + direction, transform.up);
		// カプセルプリミティブの頭を進行方向へ向けるように修正
		transform.rotation *= Quaternion.AngleAxis(90.0f, Vector3.right);
	}

	IEnumerator MoveCoroutine()
	{
		yield return new WaitForSeconds(particle.main.startDelay.constant);

		while (true)
		{
			if(target == null)
			{
				yield break;
			}
			// 弾からターゲットへの方向ベクトルを算出
			Vector3 targetDir = (target.position - transform.position);
			targetDir.Normalize();
			// 現在の方向ベクトルと足し合わせる
			direction += targetDir * rotationSpeed;
			direction.Normalize();
			yield return null;
		}
	}

	protected override bool IsForgiveDestroy()
	{
		Destroy(gameObject, particle.main.startLifetime.constant);
		speed = 0;

		// 弾を非表示にする
		Renderer mesh = GetComponent<Renderer>();
		if(mesh != null)
		{
			mesh.enabled = false;
		}
		// 弾の当たり判定を無効にする
		Collider collider = GetComponent<Collider>();
		if(collider != null)
		{
			collider.enabled = false;
		}
		// パーティクルを停止させる
		particle.Stop();

		StopCoroutine("MoveCoroutine");

		return false;
	}
}
