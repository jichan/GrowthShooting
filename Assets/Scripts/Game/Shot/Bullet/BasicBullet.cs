using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自機の通常弾
/// </summary>
public class BasicBullet : BulletBase {
	protected override void InitializeInternal(Transform target)
	{
		direction = Vector3.forward;
	}

	protected override void SetMoveDirection()
	{
	}
}
