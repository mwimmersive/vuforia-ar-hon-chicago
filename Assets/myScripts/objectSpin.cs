using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpin : MonoBehaviour {
	// value controls speed at which object spins around axis (x,y,z)
	private float spinx = 1f;
	private float spiny = 0f;
	private float spinz = 0f;

	// Update is called once per frame
	void Update () {
		// rotates object
		transform.Rotate (spinx, spiny, spinz);
	}
}
