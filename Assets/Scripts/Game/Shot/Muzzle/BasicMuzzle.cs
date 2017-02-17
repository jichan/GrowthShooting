using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zキーを押すと弾を発射する
/// </summary>
public class BasicMuzzle : MuzzleBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("MainShot"))
		{
			Shot();
		}
	}
}
