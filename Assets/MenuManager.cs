using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	private Animator menuAnimator;
	public GameObject rotator;
	private float rotationAngle = 0;
	
	void Awake()
	{
		menuAnimator = GetComponent<Animator>();
	}
	
	void Start () 
	{
		menuAnimator.SetTrigger("FadeInLogo");
	}
	
	public void ShowRegistrationMenu()
	{
		menuAnimator.SetTrigger("ShowRegistrationMenu");
	}
	
	public void ShowRegistrationForm()
	{
		Debug.Log("Show Registration Form");
		menuAnimator.SetTrigger("ShowRegistrationForm");
	}
	
	public void Login()
	{
		Application.LoadLevel("MainScene");
	}
	
	public void Register()
	{
		Application.LoadLevel("MainScene");
	}
	
	public void NextCharacter()
	{
		rotationAngle+=90;
		
		if(rotationAngle>360)
			rotationAngle = 0;
			
		iTween.RotateTo(rotator, iTween.Hash("z", rotationAngle));
	}
	
	public void PrevCharacter()
	{
		rotationAngle-=90;
		
		if(rotationAngle < 0)
			rotationAngle = 360;
		
		iTween.RotateTo(rotator, iTween.Hash("z", rotationAngle));
	}
}
