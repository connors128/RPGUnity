using System;
using UnityEngine;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : MonoBehaviour {

	public float radius = 1f;				// How close do we need to be to interact?
	public Transform interactionTransform;	// The transform from where we interact in case you want to offset it

	private bool _isFocus = false;	// Is this interactable currently being focused?
	[SerializeField] private Transform _player;		// Reference to the player transform

	private bool _hasInteracted = false;	// Have we already interacted with the object?

	public virtual void Interact ()
	{
		// This method is meant to be overwritten
		//Debug.Log("Interacting with " + transform.name);
	}

	void Update ()
	{
		// If we are currently being focused
		// and we haven't already interacted with the object
		if (!_hasInteracted) //&& _isFocus)
		{
			// If we are close enough
			float distance = Vector3.Distance(_player.position, interactionTransform.position);
			if (distance <= radius)
			{
				// Interact with the object
				Interact();
				_hasInteracted = true;
			}
		}
	}

	// Called when the object starts being focused
	public void OnFocused (Transform playerTransform)
	{
		_isFocus = true;
		_player = playerTransform;
		_hasInteracted = false;
	}

	// Called when the object is no longer focused
	public void OnDefocused ()
	{
		_isFocus = false;
		_player = null;
		_hasInteracted = false;
	}

	// Draw our radius in the editor
	void OnDrawGizmosSelected ()
	{
		if (interactionTransform == null)
			interactionTransform = transform;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}

}