using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePoss : MonoBehaviour
{

    void Start()
    {
        PupilTools.OnConnected += StartPupilSubscription;
        PupilTools.OnDisconnecting += StopPupilSubscription;

        PupilTools.OnReceiveData += CustomReceiveData;
    }

    void StartPupilSubscription()
    {
        PupilTools.CalibrationMode = Calibration.Mode._2D;
        PupilTools.SubscribeTo("pupil.");
    }

    void StopPupilSubscription()
    {
        PupilTools.UnSubscribeFrom("pupil.");
    }

    void CustomReceiveData(string topic, Dictionary<string, object> dictionary, byte[] thirdFrame = null)
    {
        if (topic.StartsWith("pupil.1"))
        {
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "norm_pos":
                        var positionForKey = PupilTools.VectorFromDictionary(dictionary, item.Key);
                        print("norm_pos_x : " + positionForKey.x + " / norm_pos_y : " + positionForKey.y);
                        if (positionForKey.x != 0 && positionForKey.y != 1)
                        {
                            positionForKey.x -= 0.5f;
                            positionForKey.y -= 0.5f;
                            GameObject.FindGameObjectWithTag("lEye").transform.localPosition = positionForKey;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if (topic.StartsWith("pupil.0"))
        {
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "norm_pos":
                        var positionForKey = PupilTools.VectorFromDictionary(dictionary, item.Key);
                        print("norm_pos_x : " + positionForKey.x + " / norm_pos_y : " + positionForKey.y);
                        if (positionForKey.x != 0 && positionForKey.y != 1)
                        {
                            positionForKey.x -= 0.5f;
                            positionForKey.y -= 0.5f;
                            GameObject.FindGameObjectWithTag("rEye").transform.localPosition = positionForKey;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void OnDisable()
    {
        PupilTools.OnConnected -= StartPupilSubscription;
        PupilTools.OnDisconnecting -= StopPupilSubscription;

        PupilTools.OnReceiveData -= CustomReceiveData;
    }
}
