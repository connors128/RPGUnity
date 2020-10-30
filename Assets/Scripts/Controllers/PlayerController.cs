using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls the player. Here we choose our "focus" and where to move. */

public class PlayerController : MonoBehaviour {

	public Interactable focus;	// Our current focus: Item, Enemy etc.

	public LayerMask movementMask;	// Filter out everything not walkable

	private Collider _collider;
	
	
	Camera cam;			// Reference to our camera

	// Get references
	void Start () {
		cam = Camera.main;
		_collider = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		// If we press left mouse
		if (Input.GetMouseButtonDown(0))
		{
			// We create a ray
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			// If the ray hits
			if (Physics.Raycast(ray, out hit, 100, movementMask))
			{
				//motor.MoveToPoint(hit.point);   // Move to where we hit
				RemoveFocus();
			}
		}

		// needs to be capsule collider component
		if (Input.GetMouseButtonDown(1))
		{
			Interactable interactable = _collider.GetComponent<Interactable>();
			if (interactable != null)
			{
				SetFocus(interactable);
			}
		}
	}

	// Set our focus to a new focus
	void SetFocus (Interactable newFocus)
	{
		// If our focus has changed
		if (newFocus != focus)
		{
			// Defocus the old one
			if (focus != null)
				focus.OnDefocused();

			focus = newFocus;	// Set our new focus
			//motor.FollowTarget(newFocus);	// Follow the new focus
		}
		
		newFocus.OnFocused(transform);
	}

	// Remove our current focus
	void RemoveFocus ()
	{
		if (focus != null)
			focus.OnDefocused();

		focus = null;
		//motor.StopFollowingTarget();
	}
}
