using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 発射型の弾を作成するための基底クラス
/// </summary>
public abstract class BulletBase : MonoBehaviour {
	// 弾の寿命
	[SerializeField]
	float lifeSpanSecond;
	// 弾の攻撃力
	[SerializeField]
	int power;
	// 弾の速度
	[SerializeField]
	protected float speed;

	// 方向
	protected Vector3 direction;
	// ターゲット
	protected Transform target;

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="target">標的</param>
	/// <param name="initDirection">方向の初期値</param>
	public void Initialize(Transform target, Vector3 initDirection)
	{
		Destroy(gameObject, lifeSpanSecond);
		direction = initDirection;
		this.target = target;
		InitializeInternal(target);
	}
	protected abstract void InitializeInternal(Transform target);

	/// <summary>
	/// 移動
	/// </summary>
	protected abstract void SetMoveDirection();

	/// <summary>
	/// 弾を即時削除してよいかどうか
	/// </summary>
	/// <returns></returns>
	protected virtual bool IsForgiveDestroy()
	{
		return true;
	}

	/// <summary>
	/// 座標の更新
	/// </summary>
	void Update()
	{
		SetMoveDirection();
		transform.position += direction.normalized * speed * Time.deltaTime;
	}

	/// <summary>
	/// 衝突判定
	/// </summary>
	/// <param name="target"></param>
	void OnTriggerEnter(Collider target)
	{
		if (target.gameObject.layer != Layer.Enemy)
		{
			return;
		}

		EnemyBase enemyBase = target.GetComponent<EnemyBase>();
		if(enemyBase == null)
		{
			return;
		}
		enemyBase.Damage(power);

		if (IsForgiveDestroy())
		{
			Destroy(gameObject);
		}
	}
}
