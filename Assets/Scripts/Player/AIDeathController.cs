using UnityEngine;
using System.Collections;

public class AIDeathController : MonoBehaviour {

	public bool isBoss = false;

	public delegate void botDiedEventHandler();
	public static event botDiedEventHandler botDiedEvent;

	private void Die() {

		if(!isBoss) {
			PhotonNetwork.Destroy(gameObject);

			if(botDiedEvent != null) {
				botDiedEvent();
			}
		} else {
			print ("Boss killed rotate");
			iTween.RotateTo(gameObject, new Hashtable() { 
				{"time", 5f},
				{"x", -90},
				{"onComplete", "BossKilled"},
				{"onCompleteTarget", gameObject},
				{"easeType", iTween.EaseType.easeOutBounce}
			});
		}
	}

	private IEnumerator BossKilled() {
		yield return new WaitForSeconds(1);
		PhotonNetwork.Destroy(gameObject);

		if(botDiedEvent != null) {
			botDiedEvent();
		}
	}
}
