using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// Makes the camera follow the player

public class mouseCameraController : MonoBehaviour {

	public Transform target;	// Target to follow (player)

	public Vector3 offset;			// Offset from the player
	public float zoomSpeed = 4f;	// How quickly we zoom
	public float minZoom = 5f;		// Min zoom amount
	public float maxZoom = 15f;		// Max zoom amount

	public float pitch = 2f;		// Pitch up the camera to look at head
	public float yawSpeed = 200f;   // How quickly we rotate
	public float pitchSpeed = 200f;


	// In these variables we store input from Update
	private float currentZoom = 10f;
	private float currentYaw = 0f;
	private float currentPitch = 0f;

	void Update ()
	{
		// Adjust our zoom based on the scrollwheel
		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

		// Adjust our camera's rotation around the player
		currentYaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
		currentPitch += Input.GetAxis("Mouse Y") * pitchSpeed * Time.deltaTime;
	}

	void LateUpdate ()
	{
		// Set our cameras position based on offset and zoom
		transform.position = target.position - offset * currentZoom;

		// Look at the player's head
		transform.LookAt(target.position + Vector3.up * pitch);

		// Rotate around the player
		transform.RotateAround(target.position, target.up, currentYaw);
		transform.RotateAround(target.position, target.right, currentPitch);
		transform.RotateAround(target.position, target.forward, target.rotation.z);
		//transform.RotateAround(transform.position, transform.up, currentYaw);
		//transform.eulerAngles = new Vector3(currentPitch, currentYaw, 0f);
		//transform.Rotate(0, target.up, 0, Space.Self);
	}

}
