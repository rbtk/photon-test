using UnityEngine;
using System.Collections;

public enum ARENA_EVENT {
	WAVE_1,
	BOSS
};

public class ArenaManager : Photon.MonoBehaviour {

	public delegate void arenaEventHandler(ARENA_EVENT e);
	public static event arenaEventHandler arenaEvent;

	public static ArenaManager instance;

	public GameObject botPrefab;
	public GameObject bossPrefab;

	public GameObject teleporter;

	public Transform[] spawnPoints;
	public Transform playerRespawnPoint;
	public int botsToKill = 5;

	private int botsKilled = 0;
	private int playersConnected = 0;
	private bool arenaDidBegin = false;

	void Awake() {
		if(instance){
			return;
		}

		instance = this;
	}

	void Start() {
		teleporter.SetActive(false);
		AIDeathController.botDiedEvent += BotKilled;
		PlayerController.playerDiedEvent += OnPlayerDeath;
	}

	void OnJoinedRoom() {

		playersConnected = PhotonNetwork.playerList.Length;

		if(playersConnected >= 1 && !arenaDidBegin) {
			if(arenaEvent != null) {
				arenaEvent(ARENA_EVENT.WAVE_1);
			}
			arenaDidBegin = true;
			//Temporary
			SpawnBots(botsToKill);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) 
		{
			stream.SendNext(arenaDidBegin);
		} 
		else 
		{
			arenaDidBegin = (bool)stream.ReceiveNext();
		}
	}

	public void SpawnBots(int numOfBots) {
		for(int i = 0; i < numOfBots; i++) {
			PhotonNetwork.Instantiate(botPrefab.name, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity, 0);
		}
	}

	public void BotKilled() {
		botsKilled ++;

		if(botsKilled == botsToKill) {
			if(arenaEvent != null) {
				arenaEvent(ARENA_EVENT.BOSS);
			}

			PhotonNetwork.Instantiate(bossPrefab.name, spawnPoints[0].position, Quaternion.identity, 0);
			AIDeathController.botDiedEvent -= BotKilled;
			AIDeathController.botDiedEvent += BossKilled;
		}
	}

	public void BossKilled() {
		teleporter.SetActive(true);
		iTween.MoveTo(teleporter, new Hashtable() { 
			{"time", 3},
			{"y", -1f},
			{"easeType", "linear"}
		});
	}

	private void OnPlayerDeath(GameObject player) {
		player.rigidbody.position = playerRespawnPoint.position;
	}

	void OnDestroy() {
		AIDeathController.botDiedEvent -= BotKilled;
		AIDeathController.botDiedEvent -= BossKilled;
		PlayerController.playerDiedEvent -= OnPlayerDeath;
	}
}
