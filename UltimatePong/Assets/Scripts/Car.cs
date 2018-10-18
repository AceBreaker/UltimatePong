using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    Rigidbody myrigidBody = null;
    float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
        myrigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetAxis("Vertical") > 0.5f)
        {
            myrigidBody.velocity += transform.forward * moveSpeed * Input.GetAxisRaw("Vertical");
        }
	}
}
