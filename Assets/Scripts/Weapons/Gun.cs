using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Magazine magazine;

	private Bullet currentBulletInBarrel;
	private bool isReloading = false;

	void Start() {
		LoadBarrel();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			if(magazine.isEmpty && !isReloading) {
				Magazine.magazineReloadedEvent += OnMagazineReloaded;
				StartCoroutine(magazine.Reload());
				isReloading = true;
				return;
			} else if(isReloading) {
				print ("Reloading");
				return;
			}

			currentBulletInBarrel.gameObject.SetActive(true);
			currentBulletInBarrel.FireBullet(transform.forward);
			currentBulletInBarrel = null;
			LoadBarrel();
		}
	}

	private void LoadBarrel() {
		if(currentBulletInBarrel != null) {
			return;
		}

		currentBulletInBarrel = magazine.LoadBullet(transform);
	}

	private void OnMagazineReloaded() {
		isReloading = false;
		LoadBarrel();
		Magazine.magazineReloadedEvent -= OnMagazineReloaded;
	}
}
