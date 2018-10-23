using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayTest : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Transform camera = Camera.main.transform;

        Ray ray = new Ray(camera.position, camera.rotation * new Vector2(5, 5) * 10);
        Debug.DrawRay(camera.position, camera.rotation * new Vector3(1.4f, 2.58f, 5f) * 10, Color.red);

        //data 2D
        //Ray ray = new Ray(camera.position, camera.rotation * new Vector3(gazeLoc.x, gazeLoc.y, Marker2dDistance) * 10);
        //Debug.DrawRay(camera.position, camera.rotation * new Vector3(gazeLoc.x, gazeLoc.y, Marker2dDistance) * 10, Color.green);

        RaycastHit hit;
        GameObject hitObject = null;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "hitBall")
            {
                hitObject = hit.transform.gameObject;
                hitObject.transform.position =
                new Vector3(Random.Range(-5, 5), Random.Range(0, 5), Random.Range(-5, 5));
            }
            else if (hit.transform.gameObject.tag == "hitCube")
            {
                hitObject = hit.transform.gameObject;
                hitObject.transform.localScale =
                new Vector3(Random.Range(0.5f, 2), Random.Range(0.5f, 2), Random.Range(0.5f, 2));
            }
        }
    }
}
