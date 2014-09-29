using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public string destination = null;
	
	// Use this for initialization
	void Start () {
		
		if (destination == null)
		{
			Debug.Log(name + " destination not set!");
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter()
	{
		Debug.Log("enter");
	}
}
