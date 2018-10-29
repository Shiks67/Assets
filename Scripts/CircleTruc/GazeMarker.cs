using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMarker : MonoBehaviour
{

    private RayCastF da;

    void Start()
    {
        da = GameObject.FindGameObjectWithTag("EditorOnly").
        GetComponent<RayCastF>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(da.ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            gameObject.transform.position = hit.point;
        }
    }
}
