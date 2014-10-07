using UnityEngine;
using System.Collections;

public class CameraMovementScript : MonoBehaviour {

	public GameObject[] cameraPoints;
	public bool isFollowing = true;
	public GameObject player = null;
	public float smoothLook = 1;
	public float smoothTransition = 1;
	
	private Vector3 currentCameraPoint;
	
	// Use this for initialization
	void Start () {
		
		Debug.Log("Starting Camera Movement...");
		
		if(cameraPoints.Length == 0)
			Debug.Log("Add at least one camera point!");
			
		currentCameraPoint = transform.position;
	}
	
	
	void FixedUpdate()
	{
		if (player == null || cameraPoints.Length == 0 || isFollowing == false)
			return;
		
		Vector3 playerPosition = player.transform.position;
		
		float[] distances = new float[cameraPoints.Length];
		 
		for (int i = 0; i < cameraPoints.Length; i++)
		{
			float distance = Vector3.Distance(playerPosition, cameraPoints[i].transform.position);
			distances[i] = distance; 
		}
		
		float distanceMin = distances[0];
		int minIndex = 0;
		
		for (int i = 0; i < distances.Length; i++)
		{
			if (distanceMin > distances[i])
			{
				distanceMin = distances[i];
				minIndex = i;
			}
		}
		
		
		if (currentCameraPoint != cameraPoints[minIndex].transform.position)
			currentCameraPoint = cameraPoints[minIndex].transform.position;
			
		transform.position = Vector3.Slerp(transform.position, currentCameraPoint, smoothTransition * Time.deltaTime);
		
	}
	
	void Update () {
	
		if (player == null)
			return;
			
		SmoothLookAt();
	}
	
	void SmoothLookAt ()
	{
		Vector3 relPlayerPosition = player.transform.position - transform.position;
		
		Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
		
		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smoothLook * Time.deltaTime);
	}
	
}
