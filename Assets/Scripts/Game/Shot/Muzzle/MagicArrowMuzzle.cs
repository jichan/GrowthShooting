using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法の矢を射出する
/// </summary>
public class MagicArrowMuzzle : MuzzleBase
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("MagicArrow"))
		{
			GameObject enemy = GameObject.Find("Enemy");

			Shot(enemy.transform);
		}
	}
}
