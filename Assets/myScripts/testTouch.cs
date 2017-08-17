using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTouch : MonoBehaviour {

	void MobileVibrate() {

		Handheld.Vibrate ();

	}

	//Method fires an alert when the GameObject is touched
	void alertPopUp(string metaData_Content){

		//Metadata content is structered in the cloud as one string, formatted like this:
		//name_of_sign!,url_of_landing_webpage
		//Here we split that string and take the sign_name and url_name as two variables
		string[] tokens = metaData_Content.Split (',');
		string sign_name = tokens [0];
		string url_name = tokens [1];

		// Designs alert message
		string template = "You discovered {0}";
		string data = sign_name;
		string message = string.Format(template, data);

//		Debug.Log (metaData_Content);
//		Debug.Log (sign_name);
//		Debug.Log (url_name);

		//MNPopup is special type of object created by plugin.  Essentially creates a native mobile alert.
		MNPopup popup = new MNPopup ("Honorary Chicago", message); // (title, subtitle/message)
		popup.AddAction ("Find out more!", () => {Application.OpenURL(url_name);}); // adds button with actions
		popup.AddAction ("Not now", () => {Debug.Log("action 2 action callback");}); // adds button with actions
		popup.AddDismissListener (() => {Debug.Log("dismiss listener");});
		popup.Show ();
	}

	void Update () {
		// MY CODE
		// Grabs metaData_Content string from SimpleCloudHandler.cs
		GameObject cloudScript = GameObject.Find("CloudRecognition");
		SimpleCloudHandler simpleCloudHandler = cloudScript.GetComponent<SimpleCloudHandler> ();
		string metaData_Content = simpleCloudHandler.metaData_Content;

		//

		if (Input.GetMouseButtonDown (0))
		{
			//Shoots ray from camera to the location that is clicked/touched
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 
			ShootRay(ray,metaData_Content);
//			Debug.Log ("Ray shot!");
//			ShootRay (ray);
		}
	}

	void ShootRay (Ray ray, string metaData_Content)
//	void ShootRay (Ray ray)
	{  

		RaycastHit rhit;

		bool objectHit = false;
		GameObject gObjectHit = null;


		if (Physics.Raycast (ray, out rhit, Mathf.Infinity) ) { //If the ray hits the GameObject
			// Borrowed Code
			objectHit = true;
			gObjectHit = rhit.collider.gameObject;
//			Debug.Log ("Ray shot and hit!");
			//
			// My Code
			alertPopUp(metaData_Content);
			MobileVibrate ();
			//

		}

	} 
}
