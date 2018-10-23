using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GazePointLocation : MonoBehaviour {

    //Eyes location and gaze/marker
    private PupilMarker rEye, lEye, gaze, marker;
    //Pupil-lab settings
    public PupilSettings settings;

	public delegate void OnUpdateDeleg ();

    public OnUpdateDeleg OnUpdate;

    //Singleton
    static GazePointLocation _Instance;
    public static GazePointLocation Instance
	{
		get
		{
			if (_Instance == null)
			{
				_Instance = new GameObject ("GazePointLocation").
                AddComponent<GazePointLocation> ();
			}
			return _Instance;
		}
	}

    public GazePointLocation ()
	{
		_Instance = this;
	}

	// Use this for initialization
	void Start () {
        //PupilSettings.Instance.currentCamera = Camera.main;
        settings = PupilSettings.Instance;

        if(GazePointLocation._Instance == null)
        {
            GazePointLocation._Instance = this;
        }

        var relativeRightEyePosition = UnityEngine.XR.InputTracking.GetLocalPosition (UnityEngine.XR.XRNode.RightEye) - UnityEngine.XR.InputTracking.GetLocalPosition (UnityEngine.XR.XRNode.CenterEye);
		PupilTools.Calibration.rightEyeTranslation = new float[] { relativeRightEyePosition.z*PupilSettings.PupilUnitScalingFactor, 0, 0 };
		var relativeLeftEyePosition = UnityEngine.XR.InputTracking.GetLocalPosition (UnityEngine.XR.XRNode.LeftEye) - UnityEngine.XR.InputTracking.GetLocalPosition (UnityEngine.XR.XRNode.CenterEye);
		PupilTools.Calibration.leftEyeTranslation = new float[] { relativeLeftEyePosition.z*PupilSettings.PupilUnitScalingFactor, 0, 0 };

        lEye = new PupilMarker("LeftEye", PupilSettings.leftEyeColor);
        rEye = new PupilMarker("RightEye", PupilSettings.rightEyeColor);
        gaze = new PupilMarker("Gaze", Color.yellow);
        marker = new PupilMarker("Marker", Color.red);

        print(rEye.name + " pos : " + rEye.position);
        print(lEye.name + " pos : " + lEye.position);
        print(gaze.name + " pos : " + gaze.position);
        print(marker.name + " pos : " + marker.position);

        RunConnect();

        /*PupilTools.IsGazing = true;
        PupilTools.SubscribeTo("gaze");*/
    }

    public void RunConnect()
    {
        StartCoroutine(PupilTools.Connect(retry: true, retryDelay:5f));
    }

    // Update is called once per frame
    void Update () {

        if (PupilTools.IsCalibrating)
		{
			PupilTools.Calibration.UpdateCalibration ();
		}
        
        PupilTools.Connection.UpdateSubscriptionSockets ();

        if(Instance.OnUpdate != null)
        {
            Instance.OnUpdate();
        }

        if (Input.GetKeyUp (KeyCode.S))
        {
            SceneManager.LoadScene("SampleScene");
            //SceneManager.LoadScene("SampleScene", LoadSceneMode.single);
        }

        /*lEye.UpdatePosition(PupilData._2D.LeftEyePosition);
        rEye.UpdatePosition(PupilData._2D.RightEyePosition);
        marker.UpdatePosition(PupilData._2D.GazePosition);

        gaze.UpdatePosition(PupilData._3D.GazePosition);*/
        print("Updated pos :" + gaze.position);

          //|***************************************************|||//
         //||***********************Next************************||//
        //|||***************************************************|//

        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, gaze.position);
        RaycastHit hit;
        GameObject hitObject = null;
        PointerEventData data = new PointerEventData(EventSystem.current);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "hitBall")
            {
                hitObject = hit.transform.parent.gameObject;
                SetRandomPosition(hitObject);
            }
        }
    }

    public void StartVisualizingGaze ()
	{
		Instance.OnUpdate += VisualizeGaze;

        PupilSettings.Instance.currentCamera = Camera.main;

        if ( !PupilMarker.TryToReset(lEye) )
			lEye= new PupilMarker("LeftEye_2D",PupilSettings.leftEyeColor);
		if ( !PupilMarker.TryToReset(rEye) )
			rEye = new PupilMarker("RightEye_2D",PupilSettings.rightEyeColor);
		if ( !PupilMarker.TryToReset(marker) )
			marker = new PupilMarker("Gaze_2D",Color.red);
		if ( !PupilMarker.TryToReset(gaze) )
			gaze = new PupilMarker("Gaze_3D", Color.yellow);

		PupilTools.IsGazing = true;
		PupilTools.SubscribeTo("gaze");
	}

    void VisualizeGaze ()
	{
		if (PupilTools.IsGazing)
		{
			lEye.UpdatePosition(PupilData._2D.LeftEyePosition);
			rEye.UpdatePosition (PupilData._2D.RightEyePosition);
			marker.UpdatePosition (PupilData._2D.GazePosition);
			gaze.UpdatePosition(PupilData._3D.GazePosition);
		} 
	}

    void SetRandomPosition(GameObject target)
    {
        float x = Random.Range(-5, 5);
        float z = Random.Range(-5, 5);
        float y = Random.Range(0, 5);
        target.transform.position = new Vector3(x, y, z);
    }
}
