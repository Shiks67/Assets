// originally created by Theo and Kiefer (French interns at AAU Fall 2017) 
// modified by Bianca

// outcommented parts doesn't work in Pupil Labs Plugin, but is used in another project

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

namespace RockVR.Video.Demo
{
    public class LoggerBehavior : MonoBehaviour
    {

        private static Logger _logger;
        private static List<object> _toLog;
        private Vector3 gazeToWorld;
        private static string CSVheader = AppConstants.CsvFirstRow;
        //[SerializeField] private Camera dedicatedCapture;
        //[SerializeField] private Transform viveCtrlRight;
        private Camera dedicatedCapture;

        //CircleTruc log var
        private GameObject circleObject;
        private float timer;
        private float TTFF;

        //private Camera dedicatedCapture;

        public static string sceneName = "_";

        #region Unity Methods

        private void Start()
        {
            _toLog = new List<object>();
            dedicatedCapture = Camera.main;
        }

        private void Update()
        {
            if (GameObject.FindGameObjectsWithTag("hitCircle").Length == 1)
            {
                CircleData();
                DoLog();
                AddToLog();
            }
        }

        private void CircleData()
        {
            if (circleObject != GameObject.FindGameObjectWithTag("hitCircle").gameObject)
                circleObject = GameObject.FindGameObjectWithTag("hitCircle").gameObject;
            if (!circleObject.GetComponent<CircleLife>().isTTFF)
                TTFF = circleObject.GetComponent<CircleLife>().TTFF;
            else
                TTFF = 0;
            timer += Time.deltaTime;
        }

        private void AddToLog()
        {
            if (PupilData._2D.GazePosition != Vector2.zero)
            {
                gazeToWorld = dedicatedCapture.ViewportToWorldPoint(new Vector3(PupilData._2D.GazePosition.x, PupilData._2D.GazePosition.y, Camera.main.nearClipPlane));
            }

            //var raycastHit = EyeRay.CurrentlyHit;
            var tmp = new
            {
                // default variables for all scenes
                a = Math.Round(timer, 3),
                fps = (int)(1.0f / Time.unscaledDeltaTime), //frames per second during the last frame, could calculate an average frame rate instead

                circleXpos = circleObject != null ? Math.Round(circleObject.transform.localPosition.x, 3) : double.NaN,
                circleYpos = circleObject != null ? Math.Round(circleObject.transform.localPosition.y, 3) : double.NaN,

                j = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilData._2D.GazePosition.x, 3) : double.NaN,
                k = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilData._2D.GazePosition.y, 3) : double.NaN,
                l = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(gazeToWorld.x, 3) : double.NaN,
                m = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(gazeToWorld.y, 3) : double.NaN,
                n = PupilData._2D.GazePosition != Vector2.zero ? Math.Round(PupilTools.FloatFromDictionary(PupilTools.gazeDictionary, "confidence"), 3) : double.NaN, // confidence value calculated after calibration 

                circleSize = circleObject != null ? Math.Round(circleObject.transform.localScale.x, 3) : double.NaN,
                TimeToFirstFix = TTFF != 0 ? Math.Round(TTFF, 3) : double.NaN

                /* 
                // Baking tray variables
                o = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.position.x : double.NaN,
                p = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.position.y : double.NaN,
                q = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.position.z : double.NaN,
                r = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.rotation.x : double.NaN,
                s = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.rotation.y : double.NaN,
                t = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.rotation.z : double.NaN,
                tt = SceneManage.loadTestScene == 2 ? ControllerGrabObject.bunLastActive.ctrlEventHolder : "null",
                ttt = SceneManage.loadTestScene == 2 && ControllerGrabObject.objectInHand != null ? ControllerGrabObject.bunLastActive.bunActive.name : "null",
                u = SceneManage.loadTestScene == 2 && ControllerGrabObject.objectInHand != null ? ControllerGrabObject.bunLastActive.bunActive.transform.position.x : double.NaN,
                v = SceneManage.loadTestScene == 2 && ControllerGrabObject.objectInHand != null ? ControllerGrabObject.bunLastActive.bunActive.transform.position.y : double.NaN,
                x = SceneManage.loadTestScene == 2 && ControllerGrabObject.objectInHand != null ? ControllerGrabObject.bunLastActive.bunActive.transform.position.z : double.NaN,

                // Museum variables
                cbis = SceneManage.loadTestScene == 3 && PaintingsSets.CurrentSet != null ? PaintingsSets.CurrentSet.SetName : "null",
                ord = SceneManage.loadTestScene == 3 ? PaintingsSets.myIntArrayString : "null",
                oo = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? raycastHit.transform.name : "null",
                ox = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? raycastHit.transform.position.x : double.NaN,
                oy = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? raycastHit.transform.position.y : double.NaN,
                oz = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? raycastHit.transform.position.z : double.NaN,
                oox = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? CalculEyeGazeOnObject(raycastHit).x : double.NaN,
                ooy = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? CalculEyeGazeOnObject(raycastHit).y : double.NaN,
                ooz = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? CalculEyeGazeOnObject(raycastHit).z : double.NaN
                */
            };
            _toLog.Add(tmp);
        }

        private Vector3 CalculEyeGazeOnObject(RaycastHit hit)
        {
            return hit.transform.InverseTransformPoint(hit.point);
        }

        public static void DoLog()
        {
            CSVheader = AppConstants.CsvFirstRow;
            _logger = Logger.Instance;
            if (_toLog.Count == 0)
            {
                var firstRow = new { CSVheader };
                _toLog.Add(firstRow);
            }
            _logger.Log(_toLog.ToArray());
            _toLog.Clear();
        }

        #endregion
    }
}