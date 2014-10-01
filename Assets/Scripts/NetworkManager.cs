using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public Camera standbyCamera;
	public GameObject[] respawnObjects;
	
	Respawner respawner;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		Connect ();
		respawner = FindObjectOfType<Respawner>();
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

		if(Application.loadedLevelName != "ArenaScene")
		SpawnPlayer ();
	}


	void SpawnPlayer ()
	{
		if (respawnObjects.Length > 0) {
			int index = Random.Range (0, respawnObjects.Length);
	
			
			GameObject myPlayer = PhotonNetwork.Instantiate ("player", respawnObjects[index].transform.position, respawnObjects[index].transform.rotation, 0);

			standbyCamera.active = false;

			//((MonoBehaviour)myPlayer.GetComponent("ThirdPersonController")).enabled = true;

//			ThirdPersonController bla = myPlayer.GetComponent<ThirdPersonController>();
//			bla.isMe = true;

//			((MonoBehaviour)myPlayer.GetComponent("ThirdPersonCamera")).enabled = true;
			myPlayer.transform.Find("_cameraMain").gameObject.SetActive(true);


			respawner.SetPlayer(myPlayer);

		} else 
		{
			Debug.Log("Add 1 spawn point at least!");
		}

	}
}
