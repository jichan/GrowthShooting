using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仮想的な銃口を表現するためのインターフェース
/// </summary>
public interface IMuzzle {

	/// <summary>
	/// 弾を発射する
	/// </summary>
	void CreateBullet(Transform target = null);
}
