using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ExitGames.Client.Photon.Chat;

public class ChatManager : MonoBehaviour, IChatClientListener{

	private const string APP_ID = "9e0e600e-708c-4285-9009-ecb167d9b3ff";

	public string chatUsername;
	public string defaultChannel = "General";

	public InputField userInputField;
	public ChatOutputManager outputManager;

	public ChatClient chatClient;

	private ChatChannel selectedChannel;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(gameObject);
		userInputField.onSubmit.AddListener(OnSubmit);
		chatUsername = "Player_" + PhotonNetwork.playerList.Length;

		chatClient = new ChatClient(this);
		chatClient.Connect(APP_ID, "0.1", chatUsername, null);
	}


	public void OnSubmit(string data) 
	{
		print ("Submit event: " + data);
		userInputField.text.text = "";
		userInputField.value = "";

		if(chatClient != null) 
		{
			chatClient.PublishMessage(selectedChannel.Name, data);
		}
	}

	#region Interface Callbacks

	public void OnConnected()
	{
		Debug.Log("Connected to chat!");

		chatClient.Subscribe(new string[]{defaultChannel});
	}

	public void OnDisconnected()
	{
		Debug.Log("Disconnected from chat!");
	}

	public void OnSubscribed(string[] channels, bool[] results)
	{
		chatClient.TryGetChannel(defaultChannel, false, out selectedChannel);
		for(int i = 0; i < channels.Length; i++) 
		{
			print ("Channel [" + channels[i] + "] : " + results[i]);
		}
	}

	public void OnUnsubscribed(string[] channels)
	{

	}

	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		print ("Got messages for channel: " + channelName);
		for(int i = 0; i < senders.Length; i++) 
		{
			print ("[" + senders[i] + "]: " + messages[i]);
			outputManager.AddText("[" + senders[i] + "]: " + messages[i]);
		}
	}

	public void OnChatStateChange(ChatState state)
	{
	}

	public void OnPrivateMessage(string sender, object message, string channelName)
	{

	}

	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		print ("Status update: " + user + " " + status);
	}

	#endregion

	void Update() 
	{
		if(chatClient != null)
		{
			chatClient.Service();
		}
	}
	
	void OnDestroy() 
	{
		if(chatClient != null) 
		{
			chatClient.Disconnect();
		}
	}
}
