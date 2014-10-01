using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public static SceneManager instance;

	// Use this for initialization
	void Awake () {
		if(instance) {
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
	public void ChangeRoom(string name)
	{
		RoomOptions options = new RoomOptions();
		options.isOpen = true;
		options.isVisible = true;
		options.customRoomProperties = new ExitGames.Client.Photon.Hashtable() {
															{"type", name}
														};
		PhotonNetwork.JoinOrCreateRoom(name, options, null);
	}

	void OnLevelWasLoaded(int level) {
		//ChangeRoom("Arena");
	}
}
