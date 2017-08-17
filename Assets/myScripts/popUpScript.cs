using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class popUpScript : MonoBehaviour, ITrackableEventHandler{

	// MY CODE

	void alertPopUp(string metaData_Content){

		string[] tokens = metaData_Content.Split (',');
		string sign_name = tokens [0];
		string url_name = tokens [1];

		string template = "You discovered {0}";
		string data = sign_name;
		string message = string.Format(template, data);


		MNPopup popup = new MNPopup ("Honorary Chicago", message);
		popup.AddAction ("Find out more!", () => {Application.OpenURL(url_name);});
		popup.AddAction ("Not now", () => {Debug.Log("action 2 action callback");});
		popup.AddDismissListener (() => {Debug.Log("dismiss listener");});
		popup.Show ();
		Handheld.Vibrate ();
	}


	// END


	private TrackableBehaviour mTrackableBehaviour;

	private bool mShowGUIButton = false;

	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);

		}
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED)
		{
			mShowGUIButton = true; 
		}
		else
		{
//			mShowGUIButton = false
		}
	}


	void Update () {

		GameObject cloudScript = GameObject.Find("CloudRecognition");
		SimpleCloudHandler simpleCloudHandler = cloudScript.GetComponent<SimpleCloudHandler> ();
		string metaData_Content = simpleCloudHandler.metaData_Content;


		if (mShowGUIButton) {

 			Debug.Log (metaData_Content);
			Debug.Log ("That wasn't anything was it?");
			alertPopUp (metaData_Content);
			mShowGUIButton = false;
			}
	}
	
}
