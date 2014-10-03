using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public GameObject LogoGroup;
	public GameObject MainMenuGroup;
	public GameObject LoginAndSignUpGroup;
	
	public Camera HUDCamera;
	
	void Start()
	{
//		LoginAndSignUpGroup.transform.RotateAround(HUDCamera.transform.position, Vector3.up, 180);
	}
	
	public void ShowPlayMenu()
	{
		
	}
	
	void Update()
	{
//		LoginAndSignUpGroup.transform.RotateAround(HUDCamera.transform.position, Vector3.up, Time.deltaTime*5);	
	}
}
