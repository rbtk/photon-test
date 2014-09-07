using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {
	// Update is called once per frame
	
	public GameObject[] respawnObjects;
	public GameObject player;

	void Update () {

	}

	void OnTriggerExit (Collider other)
	{
		if (respawnObjects.Length > 0)
		{
			int index = Random.Range(0,respawnObjects.Length);

			player.transform.position = respawnObjects[index].transform.position;
		}
	}
}
