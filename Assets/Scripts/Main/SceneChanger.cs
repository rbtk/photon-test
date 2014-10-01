using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public string destination = "ArenaScene";

	void OnTriggerEnter(Collider col)
	{
		if(PhotonNetwork.inRoom) {
			PhotonNetwork.LeaveRoom();
		}
		PhotonNetwork.Destroy(col.gameObject);

		Application.LoadLevel(destination);
	}
}
