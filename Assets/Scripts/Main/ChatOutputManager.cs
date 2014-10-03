using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatOutputManager : MonoBehaviour {

	public ChatOutputField[] outputFields;

	private int fieldsCount = 0;

	// Use this for initialization
	void Start () 
	{
		fieldsCount = outputFields.Length;
	}

	public void AddText(string text) 
	{
		int i = fieldsCount - 1;
		while( i > 0 ) 
		{
			outputFields[i].UpdateText(outputFields[--i].GetText());
			outputFields[i+1].lastUpdateTime -= Time.time - outputFields[i].lastUpdateTime;
		}

		outputFields[0].UpdateText(text);

	}
}
