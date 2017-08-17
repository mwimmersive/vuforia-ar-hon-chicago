using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showStaticObject : MonoBehaviour {

	private GameObject staticObjectChild;

	// Use this for initialization
	void Start () {
		// Need to change find parameter when object changed
		staticObjectChild = GameObject.Find("Flying Disk");
	}
	
	// Update is called once per frame
	void Update () {

		GameObject cloudScript = GameObject.Find("CloudRecognition");
		SimpleCloudHandler simpleCloudHandler = cloudScript.GetComponent<SimpleCloudHandler> ();
		bool showStaticObject = simpleCloudHandler.showStaticObject;

		Debug.Log (showStaticObject);

		if (showStaticObject) {
			staticObjectChild.SetActive (true);
		} else {
			staticObjectChild.SetActive (false);	
		}
	}
}
