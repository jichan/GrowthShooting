using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵を作成するための基底クラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour {
	// 最大HP
	[SerializeField]
	int maxHP;
	
	// 残りHP
	int hp;

	// 敵の種類
	public EnemyKind Kind { get; set; }

	// 出現してからの時間
	protected float timeAfterSpawn;

	public void Awake()
	{
		hp = maxHP;
		timeAfterSpawn = 0.0f;
		InitializeInternal();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(Move());
	}
	
	// Update is called once per frame
	void Update () {
		timeAfterSpawn += Time.deltaTime;
	}

	/// <summary>
	/// HPを減らす
	/// </summary>
	/// <param name="num"></param>
	public void Damage(int num)
	{
		hp -= num;

		if(hp < 0)
		{
			OnDied();
			EnemyManager.Instance.RemoveEnemy(Kind, this);
			Destroy(gameObject);
		}
	}

	protected virtual void InitializeInternal()
	{

	}

	/// <summary>
	/// 移動を行う
	/// </summary>
	protected abstract IEnumerator Move();

	/// <summary>
	/// 死んだ時に呼ばれる
	/// </summary>
	protected virtual void OnDied()
	{

	}
}
