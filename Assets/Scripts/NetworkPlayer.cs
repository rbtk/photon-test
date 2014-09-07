using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	float lastUpdateTime;
//	private ThirdPersonController controller;

	// Use this for initialization
	void Start () {
//		controller = GetComponent<ThirdPersonController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!photonView.isMine) 
		{
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) 
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
//			stream.SendNext(controller.GetCurrentAnimationState());
//			Debug.Log(controller.GetCurrentAnimationState());
		} 
		else 
		{
			realPosition = (Vector3)(stream.ReceiveNext());
			realRotation = (Quaternion)(stream.ReceiveNext());
//			controller.SetAnimationState((CharacterState)stream.ReceiveNext());
		}
	}
}
