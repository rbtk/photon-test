using UnityEngine;
using System.Collections;

public class Magazine : MonoBehaviour {

	public static System.Action magazineReloadedEvent;

	public GameObject bulletPrefab;
	public int ammoCapacity = 30;
	public float bulletReloadTime = 0.5f;
	
	public bool isEmpty = false;
	
	private GameObject[] bullets;
	private Bullet[] bulletScripts;

	// Use this for initialization
	void Start () {
		bullets = new GameObject[ammoCapacity];
		bulletScripts = new Bullet[ammoCapacity];

		for(int i = 0; i < ammoCapacity; i++) {
			bullets[i] = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
			//bullets[i].transform.parent = transform;

			bulletScripts[i] = bullets[i].GetComponent<Bullet>();
			bulletScripts[i].isInMagazine = true;

			bullets[i].SetActive(false);
		}

		isEmpty = false;
	}



	public Bullet LoadBullet(Transform barrelPosition) {
		for(int i = 0; i < ammoCapacity; i++) {
			if(!bulletScripts[i].isFired && bulletScripts[i].isInMagazine) {
				bulletScripts[i].PutInBarrel(barrelPosition);
				return bulletScripts[i];
			}
		}
		print ("Magazine is empty!");
		isEmpty = true;
		return null;
	}

	public IEnumerator Reload() {
		print ("Reloading...");
		for(int i = 0; i < ammoCapacity; i++) {

			//bullets[i].transform.parent = transform;
			bulletScripts[i].isInMagazine = true;
			yield return new WaitForSeconds(bulletReloadTime);
		}

		isEmpty = false;
		print ("Reloading finished.");

		FireMagazineReloadedEvent();
	}

	private void FireMagazineReloadedEvent() {
		if(magazineReloadedEvent != null) {
			magazineReloadedEvent();
		}
	}
}
