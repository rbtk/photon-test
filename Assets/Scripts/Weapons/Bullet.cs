using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float fireForce = 300f;
	public LayerMask mask;
	public bool isFired = false;
	public bool isInMagazine = false;

	private Transform startTransform;
	private float timeToStayAlive = 0f;

	void Awake() {
		startTransform = transform;
	}

	public void FireBullet(Vector3 direction) {
		isFired = true;
		isInMagazine = false;
		transform.parent = null;
		timeToStayAlive = Time.time + 1.0f;
		GetComponent<Rigidbody>().velocity = direction * fireForce;
		//rigidbody.AddForce();
	}

	void LateUpdate() {
		Ray ray = new Ray(GetComponent<Rigidbody>().position, transform.forward);
		RaycastHit hit = new RaycastHit();
		if(Physics.Raycast(ray, out hit, 1, mask) || timeToStayAlive < Time.time) {
			//print ("HitTarget: " + hit.transform.name);
			isFired = false;
			gameObject.SetActive(false);
			//Destroy(gameObject);
		}
	}

	public void PutInBarrel(Transform barrel) {
		transform.parent = barrel;
		transform.position = barrel.position;
		transform.rotation = barrel.rotation;
	}
}
