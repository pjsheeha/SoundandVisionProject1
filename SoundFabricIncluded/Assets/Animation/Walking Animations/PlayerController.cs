using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float jump = Input.GetAxis ("Jump");
		Vector3 movement = new Vector3 (0f, 3 * jump, 0f);
		rb.AddForce (movement * speed);
	}
}
