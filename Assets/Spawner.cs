using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {


	public Camera standbyCamera;
	public GameObject player;


	//spawn points on the area
	public Transform[] spawnPoints;
	
	//room name
	public string roomName = "main";
	
	//max players in room
	public int playersMax = 10;
	
	//determines if the room name is visible in the loby
	public bool visible = true;
	
	//determines if the room is open
	public bool open = true;
	
	//determines if the room is private
	public bool isPrivate;

	private Respawner respawner;

	
	void Start () {
		respawner = FindObjectOfType<Respawner>();
		//connect
		//PhotonNetwork.ConnectUsingSettings(Globals.version);
		if(PhotonNetwork.insideLobby) {
			print ("Try to connect: " + roomName);
			RoomOptions options = new RoomOptions();
			options.isOpen = open;
			options.isVisible = visible;
			options.maxPlayers = playersMax;
			options.customRoomProperties = new ExitGames.Client.Photon.Hashtable() {
				{"type", roomName}
			};

			PhotonNetwork.JoinOrCreateRoom(roomName, options, null);
		}
	}
	
	//Photon network delegate methods
	
	void OnJoinedLobby()
	{
		Debug.Log("Joined Lobby...");
		
		Debug.Log("Joining room '" + roomName + "'");

		print ("Try to connect: " + roomName);
		RoomOptions options = new RoomOptions();
		options.isOpen = open;
		options.isVisible = visible;
		options.maxPlayers = playersMax;
		options.customRoomProperties = new ExitGames.Client.Photon.Hashtable() {
			{"type", roomName}
		};
		
		PhotonNetwork.JoinOrCreateRoom(roomName, options, null);
		
//		RoomOptions roomOptions = new RoomOptions() { isVisible = visible, maxPlayers = playersMax };
		
//		PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, LobbyType.Default);
	}
	
	void OnPhotonJoinRoomFailed()
	{
		Debug.Log("Failed to join room '" + roomName + "'!");
	}
	
	void OnPhotonCreateRoomFailed()
	{
		Debug.Log("Failed to create room '" + roomName + "'!");
	}
	
	void OnJoinedRoom()
	{


		Debug.Log("Joined room '" + roomName + "'!");
		SpawnPlayer();
	}

	void SpawnPlayer ()
	{
		if (spawnPoints.Length > 0)
		{
			int index = Random.Range (0, spawnPoints.Length);
	
			GameObject myPlayer = PhotonNetwork.Instantiate (player.name, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0);
			
			CameraMovementScript cameraScript = standbyCamera.GetComponent<CameraMovementScript>();
			
			cameraScript.player = myPlayer;
			
			if(respawner != null)
				respawner.SetPlayer(myPlayer);
			
		} else 
		{
			Debug.Log("Add 1 spawn point at least!");
		}
		
	}
}
