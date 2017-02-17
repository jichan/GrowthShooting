using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの位置を原点からの距離で設定する
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraPositionSetter : MonoBehaviour {
	[SerializeField]
	Vector3 padding;
	[SerializeField]
	float radius;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnValidate()
	{
		Vector3 rotation = Vector3.zero - transform.forward;
		rotation.Normalize();
		transform.position = padding + rotation * radius;
	}
}
