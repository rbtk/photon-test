using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatOutputField : MonoBehaviour {

	public InputField textField;
	public float timeUntilFade = 10f;

	public float lastUpdateTime = 0;
	private float fadeTime = 0;
	private Color modifiedColor;
	private bool hasText = false;

	// Use this for initialization
	void Start () {
		modifiedColor = textField.text.color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float timeDiff = Time.time - lastUpdateTime;

		if(hasText && timeDiff > timeUntilFade && modifiedColor.a > 0)
		{
			modifiedColor.a = Mathf.Lerp(1, 0, fadeTime);
			textField.text.color = modifiedColor;
			fadeTime += Time.deltaTime;
		}
		else if(modifiedColor.a == 0 && hasText) 
		{
			hasText = false;
			textField.text.text = "";
		}
	}

	public void UpdateText(string text) 
	{
		textField.value = text;
		lastUpdateTime = Time.time;

		modifiedColor.a = 1;
		textField.text.color = modifiedColor;

		hasText = true;
		fadeTime = 0;
	}

	public string GetText()
	{
		return textField.text.text;
	}
}
