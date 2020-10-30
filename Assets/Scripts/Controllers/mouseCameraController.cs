using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// Makes the camera follow the player

public class mouseCameraController : MonoBehaviour {

	public Transform target;

	public Vector3 offset;
	public float zoomSpeed = 4f;
	public float minZoom = 5f;
	public float maxZoom = 15f;

	public float pitch = 2f;		// Pitch up the camera to look at head
	public float yawSpeed = 200f;   // horizontal rotate speed
	public float pitchSpeed = 200f; // vertical rotate speed

	// In these variables we store input from Update
	private float currentZoom = 10f; // starting zoom
	private float currentYaw = 0f;
	private float currentPitch = 0f;
	

	void Update ()
	{
		// Adjust our zoom based on the scrollwheel
		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

		// Adjust our camera's rotation around the player
		currentYaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;

		while (currentPitch > 360 || currentPitch < -360)
		{
			currentPitch /= 360;
			Debug.Log(currentPitch);
		}

		if (currentPitch <= 90 && currentPitch >= -90)
		{
			currentPitch += Input.GetAxis("Mouse Y") * pitchSpeed * Time.deltaTime;
			if (currentPitch > 90)
				currentPitch = 90f;
			else if (currentPitch < -90)
				currentPitch = -90f;
		}
	}

	void LateUpdate ()
	{
		// Set our cameras position based on offset and zoom
		transform.position = target.position - offset * currentZoom;

		// Look at the player's head
		transform.LookAt(target.position + (Vector3.up + Vector3.up) * pitch);

		// Rotate around the player
		transform.RotateAround(target.position, transform.up, currentYaw); //x
		//clamp x between -90 and 90
		
		transform.RotateAround(target.position, transform.right, currentPitch); //y
		
		//clamp z between -90 and 90
		transform.RotateAround(target.position, transform.forward, transform.rotation.z); //z
	}

}
