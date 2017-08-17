using System;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This MonoBehaviour implements the Cloud Reco Event handling for this sample.
/// It registers itself at the CloudRecoBehaviour and is notified of new search results.
/// </summary>
public class SimpleCloudHandler : MonoBehaviour, ICloudRecoEventHandler
{
	// Value grabbed by cloud and shared with testTouch.cs
	public string metaData_Content;

	void MobileVibrate() {

		Handheld.Vibrate ();

	}

	#region PRIVATE_MEMBER_VARIABLES

	// CloudRecoBehaviour reference to avoid lookups
	private CloudRecoBehaviour mCloudRecoBehaviour;
	// ImageTracker reference to avoid lookups
	private ObjectTracker mImageTracker;

	private bool mIsScanning = false;

	private string mTargetMetadata = "";

	#endregion // PRIVATE_MEMBER_VARIABLES


	#region EXPOSED_PUBLIC_VARIABLES

	/// <summary>
	/// can be set in the Unity inspector to reference a ImageTargetBehaviour that is used for augmentations of new cloud reco results.
	/// </summary>
	public ImageTargetBehaviour ImageTargetTemplate;

	#endregion

	#region UNTIY_MONOBEHAVIOUR_METHODS

	/// <summary>
	/// register for events at the CloudRecoBehaviour
	/// </summary>
	void Start()
	{
		// register this event handler at the cloud reco behaviour
		CloudRecoBehaviour cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
		if (cloudRecoBehaviour)
		{
			cloudRecoBehaviour.RegisterEventHandler(this);
		}
		// remember cloudRecoBehaviour for later
		mCloudRecoBehaviour = cloudRecoBehaviour;

	}

	#endregion // UNTIY_MONOBEHAVIOUR_METHODS


	#region ICloudRecoEventHandler_IMPLEMENTATION

	/// <summary>
	/// called when TargetFinder has been initialized successfully
	/// </summary>
	public void OnInitialized()
	{
		// get a reference to the Image Tracker, remember it
		mImageTracker = (ObjectTracker)TrackerManager.Instance.GetTracker<ObjectTracker>();
	}

	/// <summary>
	/// visualize initialization errors
	/// </summary>
	public void OnInitError(TargetFinder.InitState initError)
	{
	}

	/// <summary>
	/// visualize update errors
	/// </summary>
	public void OnUpdateError(TargetFinder.UpdateState updateError)
	{
	}

	/// <summary>
	/// when we start scanning, unregister Trackable from the ImageTargetTemplate, then delete all trackables
	/// </summary>
	public void OnStateChanged(bool scanning) {
		mIsScanning = scanning;
		if (scanning) {
			// clear all known trackables
			ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			tracker.TargetFinder.ClearTrackables (false);
		}
	}

	/// <summary>
	/// Handles new search results
	/// </summary>
	/// <param name="targetSearchResult"></param>
//	public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
	public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
	{
		// duplicate the referenced image target
		GameObject newImageTarget = Instantiate(ImageTargetTemplate.gameObject) as GameObject;

		GameObject augmentation = null;

		metaData_Content = targetSearchResult.MetaData;
//		Debug.Log (metaData_Content);
		string model_name = "myCube";

		MobileVibrate ();

		// ***************
		//
		// This snippet makes big assumption that the gameObjects's name must always be myCube.
		// From this point on, metadata should only include information about the sign, --> the sign name, url
		// Place the actual object underneath myCube
		//
		// ***************

		if( augmentation != null )
			augmentation.transform.parent = newImageTarget.transform;

		// enable the new result with the same ImageTargetBehaviour:
		ImageTargetAbstractBehaviour imageTargetBehaviour = mImageTracker.TargetFinder.EnableTracking(targetSearchResult, newImageTarget);

		mTargetMetadata = model_name;

		// Not my code, kept as a sample of how to destroy object if multiple objects are possible with the cloud
//		switch( model_name ){
//		case "Cube" :
//			Destroy( imageTargetBehaviour.gameObject.transform.Find("Capsule").gameObject );
//			break;
//		case "Capsule" :
//			Destroy( imageTargetBehaviour.gameObject.transform.Find("Cube").gameObject );
//			break;
//		}

		if (!mIsScanning)
		{
			// stop the target finder
			mCloudRecoBehaviour.CloudRecoEnabled = true;
		}
	}

	#endregion // ICloudRecoEventHandler_IMPLEMENTATION
}