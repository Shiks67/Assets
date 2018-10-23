using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DataAccess2 : MonoBehaviour {

	private Vector3 gazeLoc;
	public Transform marker;

	// Use this for initialization
	void Start () 
	{
		PupilData.calculateMovingAverage = true;
	}

	void OnEnable()
    {
        if (PupilTools.IsConnected)
        {
            PupilGazeTracker.Instance.StartVisualizingGaze();
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (PupilTools.IsConnected && PupilTools.IsGazing)
        {
			gazeLoc = PupilData._3D.GazePosition;
            marker.localPosition = PupilData._3D.GazePosition;
		}
		Transform camera = Camera.main.transform;

		Ray ray = new Ray(camera.position, camera.rotation * gazeLoc * 100);
		//Debug raycast
		//Debug.DrawRay(camera.position, gazeLoc * 100, Color.green);
		RaycastHit hit;
		GameObject hitObject = null;
		PointerEventData data = new PointerEventData(EventSystem.current);
		if(Physics.Raycast(ray, out hit))
		{
			
			if(hit.transform.gameObject.tag == "hitBall")
			{
				hitObject = hit.transform.parent.gameObject;
        		hitObject.transform.position = 
				new Vector3(Random.Range(-5, 5),Random.Range(-5, 5),Random.Range(-5, 5));
			}
			else if(hit.transform.gameObject.tag == "hitCube")
			{
				hitObject = hit.transform.parent.gameObject;
				hitObject.transform.localScale = 
				new Vector3(Random.Range(0.5f, 2),Random.Range(0.5f, 2),Random.Range(0.5f, 2));
			}
		}
	}
}
