using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PupilInfo : MonoBehaviour
{

    public Text lconf;
    public Text rconf;
    public float refreshTime;
    private float countDown;

    // Use this for initialization
    void Start()
    {
        countDown = refreshTime;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown < 0)
        {
            lconf.text = "lConf\n" + (PupilTools.Connection.confidence1 * 100) + "%";
            rconf.text = "rConf\n" + (PupilTools.Connection.confidence0 * 100) + "%";
            countDown = refreshTime;
        }
    }
}
