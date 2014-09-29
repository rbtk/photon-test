using UnityEngine;
using System.Collections;

public class TouchHandler : MonoBehaviour {
	
	public GameObject selectedCharacter;
	
	void Update () {
		
		//handle touches
		int i = 0;
		while (i < Input.touchCount)
		{
			if (Input.GetTouch(i).phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit))
				{
					selectedCharacter = hit.collider.gameObject;
				}
			}
			
			++i;	
		}
		
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))
			{
				selectedCharacter = hit.collider.gameObject;
			}
		}
	}
	
	
}
