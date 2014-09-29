using UnityEngine;
using System.Collections;

public class UsernameInput : MonoBehaviour {

	public GUIStyle fieldStyle;
	public GameObject[] characters;
	public GameObject[] characterPoints;
	
	private GameObject[] bla = new GameObject[5];
	public AnimationClip clip;
	private TouchHandler handler = null;
	
	string guitext = "Enter username";
	
	void Start()
	{
		Debug.Log("start");
		
		int i = 0;
		foreach (GameObject go in characters)
		{
			GameObject test = GameObject.Instantiate(go, characterPoints[i].transform.position, characterPoints[i].transform.rotation) as GameObject;
			
			bla[i] = test as GameObject;

			i++;
		}
		
		handler = GetComponent<TouchHandler>();
	}
	
	void OnGUI()
	{
		GUI.TextField (new Rect (Screen.width/2 - 100, Screen.height - 100, 200, 50), guitext, fieldStyle);
		
		if(GUI.Button (new Rect (Screen.width/2 - 100, Screen.height/7, 200, 50), "Play"))
			PlayGame();
	}
	
	void Update()
	{
		foreach (GameObject go in bla)
		{
			if (go)
			{
				go.transform.Rotate(Vector3.up);
				
				if (!go.animation)
					continue;
				
				go.animation.Play(clip.name);
				
			}
		}
		
		foreach (char c in Input.inputString)
		{
			if (c == "\b"[0])
			{
				if (guitext.Length != 0)
				{
					guitext = guitext.Substring(0, guitext.Length - 1);
				}
			}
			else if (c == "\n"[0] || c == "\r"[0])
			{
			
			}
			else
			{
				guitext += c;
			}
		}
		
	}
	
	void PlayGame()
	{
		Debug.Log("bla");
		if(guitext.Length > 0 && guitext != "Enter username" && handler.selectedCharacter != null)
		{
			PlayerPrefs.SetString("character", handler.selectedCharacter.name);
			Application.LoadLevel("MainScene");
		}
	}
}
