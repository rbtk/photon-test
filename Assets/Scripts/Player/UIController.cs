using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	public UnityEngine.UI.Slider healthBar;
	public UnityEngine.UI.Slider manaBar;

	public GameObject controlsUI;

	public PlayerStateController playerStateController;

	void Start() {
		healthBar.maxValue = 1;
		healthBar.minValue = 0;

		manaBar.maxValue = 1;
		manaBar.minValue = 0;

	}
	
	public void UpdateHealthAndMana(float health, float mana) {
		healthBar.value = health;
		manaBar.value = mana;
	}

	public void EnableControls() {
		controlsUI.SetActive(true);
	}

	public void DisableControls() {
		controlsUI.SetActive(false);
	}

	public void Attack(bool attack) {
		if(attack) {
			playerStateController.Attack();
		} else {
			playerStateController.StopAttack();
		}
	}

	public void Move(string direction) {
		if(direction != "") {
			playerStateController.Move(direction);
		} else {
			playerStateController.StopMoving();
		}
	}

}
