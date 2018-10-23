using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketWith3DCalibration : MonoBehaviour 
{
	public Transform marker;
	private Vector3 gazeLoc;
	// Use this for initialization
	void Start () 
	{
		PupilData.calculateMovingAverage = true;
	}

	void OnEnable()
	{
		if (PupilTools.IsConnected)
		{
			PupilGazeTracker.Instance.StartVisualizingGaze ();		
		}	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform camera = Camera.main.transform;
		if (PupilTools.IsConnected && PupilTools.IsGazing)
		{
			marker.localPosition = PupilData._3D.GazePosition;
			gazeLoc = PupilData._3D.GazePosition;
			Debug.DrawRay(camera.position, camera.rotation * gazeLoc * 100, Color.red);
		}
	}
}
