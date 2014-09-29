﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

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
	
	void Start () {
		
		//connect
		PhotonNetwork.ConnectUsingSettings(Globals.version);
		
	}
	
	void Update () {
	
	}
	
	//Photon network delegate methods
	
	void OnJoinedLoby()
	{
		Debug.Log("Joined Lobby...");
		
		Debug.Log("Joining room '" + roomName + "'");
		
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
	}
}
