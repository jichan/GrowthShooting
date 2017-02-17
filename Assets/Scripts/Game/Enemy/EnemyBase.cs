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
	[SerializeField]
	int hp;

	public void Awake()
	{
		hp = maxHP;
		InitializeInternal();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
			Destroy(gameObject);
		}
	}

	protected virtual void InitializeInternal()
	{

	}

	/// <summary>
	/// 移動を行う
	/// </summary>
	protected abstract void Move();

	/// <summary>
	/// 死んだ時に呼ばれる
	/// </summary>
	protected virtual void OnDied()
	{

	}
}
