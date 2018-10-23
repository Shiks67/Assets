using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastF : MonoBehaviour {

	Transform camera;
	public Ray ray;
	// Use this for initialization
	void Start () {
		camera = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		ray = new Ray(camera.position, camera.rotation * Vector3.forward * 15);
	}
}
