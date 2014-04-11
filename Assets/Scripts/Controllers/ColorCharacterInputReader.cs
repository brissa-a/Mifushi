﻿using UnityEngine;
using System.Collections;


public class ColorCharacterInputReader : MonoBehaviour {
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	private	ColorCharacterController	_gameController;
	
	void Awake() {
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		_gameController = GetComponent<ColorCharacterController>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}


	#region Event Listeners

	void onControllerCollider(RaycastHit2D hit) {
		// bail out on plain old ground hits cause they arent very interesting
		//Debug.Log(hit.collider.name);
		if (hit.normal.y == 1f) {
			return;
		}

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent(Collider2D col) {
//		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent(Collider2D col) {
//		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update() {
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if (_controller.isGrounded) {
			_velocity.y = 0;
		}

		if (Input.GetAxis("Horizontal") > 0.0f) {
			normalizedHorizontalSpeed = 1;
			if (transform.localScale.x < 0f) {
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}

			if (_controller.isGrounded) {
				_animator.Play(Animator.StringToHash("Run"));
			}
		} else if (Input.GetAxis("Horizontal") < 0.0f) {
			normalizedHorizontalSpeed = -1;
			if (transform.localScale.x > 0f) {
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}

			if (_controller.isGrounded) {
				_animator.Play(Animator.StringToHash("Run"));
			}
		} else {
			normalizedHorizontalSpeed = 0;

			if (_controller.isGrounded) {
				_animator.Play(Animator.StringToHash("Idle"));
			}
		}


		// we can only jump whilst grounded
		if (_controller.isGrounded && (Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0.0f)) {
			_velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
			_animator.Play(Animator.StringToHash("Jump"));
		}


		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		if (Input.GetButton("Red")) {
			_gameController.ChangeColor(GameLevel.GameColor.Red);
		} else if (Input.GetButton("Green")) {
			_gameController.ChangeColor(GameLevel.GameColor.Green);

		} else if (Input.GetButton("Blue")) {
			_gameController.ChangeColor(GameLevel.GameColor.Blue);
		} else if (Input.GetButton("Default")) {
			_gameController.ChangeColor(GameLevel.GameColor.Default);
		}

		_controller.move(_velocity * Time.deltaTime);
	}

}