using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動の範囲を制限する
/// </summary>
public class PositionClamp : MonoBehaviour {
	[SerializeField]
	Vector2 size;

	Vector2 halfSize;

	void Awake()
	{
		halfSize = size * 0.5f;
	}

	// Use this for initialization
	void Start () {
		
	}

	void Update()
	{
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, -halfSize.x, halfSize.x);
		pos.z = Mathf.Clamp(pos.z, -halfSize.y, halfSize.y);
		transform.position = pos;
	}

	void OnDrawGizmosSelected()
	//void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(size.x, 1.0f, size.y));
	}
}
