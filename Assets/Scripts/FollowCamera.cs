using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

	public GameObject target;
	public float targetOffset = -1.5f;

	private Transform targetT;
	private Transform cameraT;

	void Start () {
		targetT = target.transform;
		cameraT = transform;
	}

	void LateUpdate () {
		if (!GameManager.Instance.gameOver) {
			StartFollowing ();
			cameraT.position = new Vector3 (targetT.position.x - targetOffset, cameraT.position.y, cameraT.position.z);
		} else {
			StopFollowing ();
		}
	}

	public void StartFollowing () {
		this.enabled = true;
	}
		
	public void StopFollowing () {
		this.enabled = false;
	}

}
