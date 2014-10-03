using UnityEngine;
using System.Collections;

public class PlayerAnimationBasedActions : MonoBehaviour {

	public int minDamage = 10;
	public int maxDamage = 50;
	public float hitRange = 1f;
	public bool isPlayer = false;

	public void TryHitTarget() {
		RaycastHit hit = new RaycastHit();
		Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), transform.forward);


		if(Physics.Raycast(ray, out hit, hitRange)) {
			Transform hitTransform = hit.transform;
			if((hitTransform.tag == "AI" && isPlayer) || (hitTransform.tag == "Player" && !isPlayer)) {
				hitTransform.SendMessage("TakeDamage", Random.Range(minDamage, maxDamage));
			}
		}
	}
}
