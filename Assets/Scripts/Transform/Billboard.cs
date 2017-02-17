using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour {

	// Update is called once per frame
	void OnWillRenderObject() {
		transform.LookAt(Camera.current.transform);
	}
}
