using UnityEngine;
using System.Collections;

public class ArenaSpawner : Photon.MonoBehaviour {

	public static ArenaSpawner instance;

	public GameObject botPrefab;
	public GameObject bossPrefab;
	public Transform[] spawnPoints;

	private int botsKilled = 0;

	void Awake() {
		if(instance){
			return;
		}

		instance = this;
	}

	void Start() {
		//SpawnBots(3);
	}

	public void SpawnBots(int numOfBots) {
		for(int i = 0; i < numOfBots; i++) {
			PhotonNetwork.Instantiate(botPrefab.name, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity, 0);
		}
	}

	public void BotKilled() {
		botsKilled ++;
	}
}
