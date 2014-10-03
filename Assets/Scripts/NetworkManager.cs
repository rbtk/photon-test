using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		Connect ();
	}

	void Connect () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width - 50, 0, 50, 50), PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby()
	{
		Debug.Log("On Joined Lobby");
		//PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Failed random join");
		PhotonNetwork.CreateRoom (null);
	}
}
