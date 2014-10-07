using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {

	public string destination = "ArenaScene";

	void OnTriggerEnter(Collider col)
	{
		Debug.Log("the fuck?");
		
		if(col.gameObject.GetComponent<PhotonView>().isMine) {
			PhotonNetwork.Destroy(col.gameObject);

			if(PhotonNetwork.inRoom) {
				PhotonNetwork.LeaveRoom();
			}

			PhotonNetwork.LoadLevel(destination);
		}
	}
}
