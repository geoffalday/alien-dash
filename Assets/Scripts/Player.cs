using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

	public float speed = 6.5f;
	public float jumpSpeed = 15f;
	public float jumpDurationThreshold = 0.25f;
	public GameObject deathParticles;
	public Transform groundTrigger;
	public float groundTriggerRadius;
	public LayerMask groundLayers;

	private Vector2 input;
	private Rigidbody2D rb;
	private Animator playerAnimator;
	private bool isJumping = false;
	private float jumpDuration;
	private bool isFalling = false;
	private bool isSinking = false;
	private bool isGrounded = false;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponent<Animator> ();
	}

	void Start () {
		input.x = 1;
	}
		
	void Update () {
		input.y = Input.GetAxis("Fire1");

		if (input.y >= 1f) {
			jumpDuration += Time.deltaTime;
		} else {
			isJumping = false;
			jumpDuration = 0f;
		}

		if (IsGrounded () && !isJumping) {
			if (input.y > 0f) {
				isJumping = true;
			}
		}

		if (jumpDuration > jumpDurationThreshold) {
			input.y = 0f;
		}

		playerAnimator.SetFloat ("Speed", speed);
		playerAnimator.SetBool ("Grounded", IsGrounded ());
	}

	void FixedUpdate() {
		isFalling = rb.velocity.y < -0.1;

		rb.velocity = new Vector2 (input.x * speed, rb.velocity.y);

		if (isJumping && jumpDuration < jumpDurationThreshold) {
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
		}
			
		if (!isSinking || !isFalling) {
			rb.velocity = new Vector2 (speed, rb.velocity.y);
		} else {
			rb.velocity = new Vector2 (0, -1);
		}

		if (GameManager.instance.levelComplete) {
			speed = 0;
			rb.isKinematic = true;
			rb.velocity = Vector2.zero;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Death")) {
			Die ();
		}

		if (other.CompareTag ("QuickSand")) {
			isSinking = true;
			GameManager.instance.PlayerDied ();
		}
	}

	public bool IsGrounded() {
		isGrounded = Physics2D.OverlapCircle (groundTrigger.position, groundTriggerRadius, groundLayers);
		return isGrounded;
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (groundTrigger.position, groundTriggerRadius);
	}

	void Die () {
		GameManager.instance.PlayerDied ();

		Vector3 deathPos;

		if (!isSinking) {
			deathPos = transform.position;
		} else {
			deathPos = new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z);
		}

		Instantiate (deathParticles, deathPos, Quaternion.identity);

		gameObject.SetActive (false);
	}

}
