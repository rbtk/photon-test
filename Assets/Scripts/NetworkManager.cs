using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public Camera standbyCamera;
	public GameObject[] respawnObjects;

	// Use this for initialization
	void Start () {
		Connect ();
	}

	void Connect () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby()
	{
		Debug.Log("On Joined Lobby");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Failed random join");
		PhotonNetwork.CreateRoom (null);
	}

	void OnJoinedRoom()
	{
		Debug.Log ("Joined Room!");
		SpawnPlayer ();
	}


	void SpawnPlayer ()
	{
		if (respawnObjects.Length > 0) {
			int index = Random.Range (0, respawnObjects.Length);
		
			GameObject myPlayer = PhotonNetwork.Instantiate ("player", respawnObjects[index].transform.position, respawnObjects[index].transform.rotation, 0);

			standbyCamera.active = false;
//			standbyCamera.GetComponent("AudioListener").enabled = false;

//			((MonoBehaviour)standbyCamera.GetComponent("AudioListener")).enabled = false;

			((MonoBehaviour)myPlayer.GetComponent("ThirdPersonController")).enabled = true;
			((MonoBehaviour)myPlayer.GetComponent("ThirdPersonCamera")).enabled = true;
//			((MonoBehaviour)myPlayer.GetComponent("CharacterController")).enabled = true;

			myPlayer.transform.FindChild("_cameraMain").gameObject.SetActive(true);
//			((MonoBehaviour)myPlayer.transform.FindChild("warrior2swords").gameObject.GetComponent("CharacterController")).enabled = true;


		} else 
		{
			Debug.Log("Add 1 spawn point at least!");
		}

	}
}
