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
	private SpriteRenderer sr;
	private Animator playerAnimator;
	private bool isJumping = false;
	private float jumpDuration;
	private bool isFalling = false;
	private bool isSinking = false;
	private bool isGrounded = false;
	private bool isDead = false;
	private AudioSource[] audioSources;
	private AudioSource jumpAudio;
	private AudioSource quickSandAudio;
	private AudioSource dieAudio;
	private bool canPlayJumpSound;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		playerAnimator = GetComponent<Animator> ();
		audioSources = GetComponents<AudioSource> ();
		jumpAudio = audioSources [0];
		quickSandAudio = audioSources [1];
		dieAudio = audioSources [2];
	}

	void Start () {
		input.x = 1;
		canPlayJumpSound = true;
	}
		
	void Update () {
		input.y = Input.GetAxis("Fire1");

		if (input.y >= 1f) {
			jumpDuration += Time.deltaTime;
		} else {
			isJumping = false;
			canPlayJumpSound = true;
			jumpDuration = 0f;
		}

		if (IsGrounded () && !isJumping && !isDead) {
			if (input.y > 0f) {
				isJumping = true;
			}
		}

		if (isJumping && jumpDuration < jumpDurationThreshold && canPlayJumpSound && !GameManager.Instance.levelComplete) {
			jumpAudio.PlayOneShot (SoundManager.Instance.jump);
			canPlayJumpSound = false;
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

		if (GameManager.Instance.levelComplete || isDead) {
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
			quickSandAudio.PlayOneShot (SoundManager.Instance.quicksand);
			GameManager.Instance.PlayerDied ();
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
		isDead = true;
		GameManager.Instance.PlayerDied ();
		sr.enabled = false;

		dieAudio.PlayOneShot (SoundManager.Instance.dieBySpike);

		Vector3 deathPos;
		if (!isSinking) {
			deathPos = transform.position;
		} else {
			deathPos = new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z);
		}

		Instantiate (deathParticles, deathPos, Quaternion.identity);
		StartCoroutine(DisablePlayerWhenSoundFinished(SoundManager.Instance.dieBySpike));
	}

	IEnumerator DisablePlayerWhenSoundFinished(AudioClip clip) {
		yield return new WaitForSeconds (clip.length);
		gameObject.SetActive (false);
	}

}
