using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法の矢を射出する
/// </summary>
public class MagicArrowMuzzle : MuzzleBase
{
	[SerializeField]
	int maxTarget;
	public int MaxTarget {
		get { return maxTarget; }
		set { maxTarget = value; }
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("MagicArrow"))
		{
			var enemyList = EnemyManager.Instance.GetNearEnemies(transform.position, maxTarget);

			for (int i = 0; i < MaxTarget; i++)
			{
				if (i >= enemyList.Count)
				{
					CreateBullet(null);
				} else {
					CreateBullet(enemyList.Values[i].transform);
				}
			}
		}
	}
}
