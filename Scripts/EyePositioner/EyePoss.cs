﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyePoss : MonoBehaviour
{

    public Text lconf;
    public Text rconf;

    public GameObject leftPupil;
    public GameObject rightPupil;

    public float refreshTime;
    private float countDown;

    void Start()
    {
        PupilTools.OnConnected += StartPupilSubscription;
        PupilTools.OnDisconnecting += StopPupilSubscription;

        PupilTools.OnReceiveData += CustomReceiveData;
        countDown = refreshTime;
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
                    case "confidence":
                        countDown -= Time.deltaTime;
                        if (countDown < 0)
                        {
                            var confidence = PupilTools.FloatFromDictionary(dictionary, item.Key);
                            lconf.text = "lConf\n" + (confidence * 100) + "%";
                        }
                        break;
                    case "norm_pos":
                        var positionForKey = PupilTools.VectorFromDictionary(dictionary, item.Key);
                        // print("norm_pos_x : " + positionForKey.x + " / norm_pos_y : " + positionForKey.y);
                        if (positionForKey.x != 0 && positionForKey.y != 1 && leftPupil != null)
                        {
                            positionForKey.x -= 0.5f;
                            positionForKey.y -= 0.5f;
                            positionForKey.x *= -1;
                            leftPupil.transform.localPosition = positionForKey;
                        }
                        break;
                    case "ellipse":
                        var dictionaryForKey = PupilTools.DictionaryFromDictionary(dictionary, item.Key);
                        foreach (var pupilEllipse in dictionaryForKey)
                        {
                            switch (pupilEllipse.Key.ToString())
                            {
                                case "center":
                                    // var center = PupilTools.ObjectToVector(pupilEllipse.Value);
                                    // // center.x -= 0.5f;
                                    // // center.y -= 0.5f;
                                    // // center.x *= -1;
                                    // GameObject.FindGameObjectWithTag("lcenter").transform.localPosition = center;
                                    break;
                                default:
                                    break;
                            }
                        }
                        // Do stuff
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
                    case "confidence":
                        if (countDown < 0)
                        {
                            var confidence = PupilTools.FloatFromDictionary(dictionary, item.Key);
                            rconf.text = "lConf\n" + (confidence * 100) + "%";
                            countDown = refreshTime;
                        }
                        break;
                    case "norm_pos":
                        var positionForKey = PupilTools.VectorFromDictionary(dictionary, item.Key);
                        // print("norm_pos_x : " + positionForKey.x + " / norm_pos_y : " + positionForKey.y);
                        if (positionForKey.x != 0 && positionForKey.y != 1 && rightPupil != null)
                        {
                            positionForKey.x -= 0.5f;
                            positionForKey.y -= 0.5f;
                            positionForKey.y *= -1;
                            rightPupil.transform.localPosition = positionForKey;
                        }
                        break;
                    case "ellipse":
                        var dictionaryForKey = PupilTools.DictionaryFromDictionary(dictionary, item.Key);
                        foreach (var pupilEllipse in dictionaryForKey)
                        {
                            switch (pupilEllipse.Key.ToString())
                            {
                                case "center":
                                    // var center = PupilTools.ObjectToVector(pupilEllipse.Value);
                                    // // center.x -= 0.5f;
                                    // // center.y -= 0.5f;
                                    // // center.x *= -1;
                                    // GameObject.FindGameObjectWithTag("rcenter").transform.localPosition = center;
                                    break;
                                default:
                                    break;
                            }
                        }
                        // Do stuff
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
