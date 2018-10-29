using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CircleData : MonoBehaviour
{

    public bool _3D = false;
    //public Transform marker;
    private CalibrationDemo calibrationDemo;
    public Vector3 gazeLoc;
    public Ray ray;
    private Camera mainCamera;
    Vector2 gazePointCenter;
    public Vector3 viewportPoint;
    private LineRenderer heading;
    public RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        PupilData.calculateMovingAverage = true;
        heading = gameObject.GetComponent<LineRenderer>();
        calibrationDemo = gameObject.GetComponent<CalibrationDemo>();
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        if (PupilTools.IsConnected)
        {
            PupilGazeTracker.Instance.StartVisualizingGaze();
            //PupilTools.IsGazing = true;
            //PupilTools.SubscribeTo ("gaze");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_3D)
            ray3D();
        else
            ray2D();
    }

    private void ray2D()
    {
        viewportPoint = new Vector3(0.5f, 0.5f, 10);
        if (PupilTools.IsConnected && PupilTools.IsGazing)
        {

            gazePointCenter = PupilData._2D.GazePosition;
            viewportPoint = new Vector3(gazePointCenter.x, gazePointCenter.y, 1f);
        }
        heading.SetPosition(0, mainCamera.transform.position - mainCamera.transform.up);
        ray = mainCamera.ViewportPointToRay(viewportPoint);
        if (Physics.Raycast(ray, out hit))
        {
            heading.SetPosition(1, hit.point);
        }
        else
        {
            heading.SetPosition(1, ray.origin + ray.direction * 50f);
        }
    }

    private void ray3D()
    {
        gazeLoc = PupilData._3D.GazePosition;
        //marker.localPosition = PupilData._3D.GazePosition;
        ray = new Ray(mainCamera.transform.position, mainCamera.transform.rotation * gazeLoc * 10);
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.rotation * gazeLoc * 10);
    }
}
