using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PupilInfo : MonoBehaviour
{

    public Text lconf;
    public Text rconf;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lconf.text = "lConf\n" + (PupilTools.Connection.confidence1 * 100) + "%";
        rconf.text = "rConf\n" + (PupilTools.Connection.confidence0 * 100) + "%";
    }
}
