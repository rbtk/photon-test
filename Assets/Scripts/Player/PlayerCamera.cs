using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public Transform playerTransform;
	public Vector3 cameraPosition;

	public float lookSpeed = 300;
	public float minVerticalAngle = -25;
	public float maxVerticalAngle = 25;
	public float minHorizontalAngle = -5;
	public float maxHorizontalAngle = 5;

	private float H = 0;
	private float V = 0;
	private float angleH = 0;
	private float angleV = 0;

	private bool rotating = false;
	private int direction = 0;

	// Use this for initialization
	void Start () {
		transform.position = playerTransform.position + cameraPosition;
//		transform.LookAt(playerTransform.position + Vector3.up);
	}

	void LateUpdate() {
		if (Time.deltaTime == 0 || Time.timeScale == 0 || playerTransform == null || !rotating) 
			return;

//		H = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1) * lookSpeed * Time.deltaTime;
//		V = Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1) * lookSpeed * Time.deltaTime;
		H = direction;
		H *= lookSpeed * Time.deltaTime;
		V *= lookSpeed * Time.deltaTime;

		angleH += H;
		angleV += V;
		angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);
		angleH = Mathf.Clamp(angleH, minHorizontalAngle, maxHorizontalAngle);

		Quaternion rotH = Quaternion.Euler(-angleV, angleH, 0);
		Quaternion rotV = Quaternion.Euler(0, H, 0);
		transform.localRotation = rotH;
		playerTransform.rotation *= rotV;

	}

	public void RotateCamera(int dir)
	{
		direction = dir;
		rotating = true;
	}

	public void StopRotating()
	{
		direction = 0;
		rotating = false;
	}


	private void RotateAround(Vector3 center, float angle1, float angle2, Vector3 axis1, Vector3 axis2) {
		Vector3 pos = transform.position;
		Quaternion rotation = Quaternion.AngleAxis(angle1, axis1) * Quaternion.AngleAxis(angle2, axis2);
		Vector3 dir = pos - center;
		dir = rotation * dir;
		transform.position = center + dir;
		transform.rotation *= Quaternion.Inverse(transform.rotation) * rotation * transform.rotation;
	}

}
