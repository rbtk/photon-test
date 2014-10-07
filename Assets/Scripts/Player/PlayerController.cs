using UnityEngine;
using System.Collections;
using System;

public class PlayerController : Photon.MonoBehaviour, ICharacter {

	public delegate void playerDiedEventHandler(GameObject player);
	public static event playerDiedEventHandler playerDiedEvent;
	
	public float health;
	public float mana;
	public float aimSpeed;
	public float moveSpeed;
	public float fireRate;
	
	public GameObject uiPrefab;

	public UIController ui;
	public PlayerCamera playerCamera;
	public PlayerAnimation playerAnim;
	public PlayerStateController playerStateController;
	public ParticleSystem fireworks;

	private float maxHealth;
	private float maxMana;

	private bool dead = false;

	public CHARACTER_STATE currentState = CHARACTER_STATE.Idle;
	private Rigidbody selfRigidbody;

	void Awake() {
		if(photonView.isMine) {
			GameObject go = GameObject.Instantiate(uiPrefab) as GameObject;
			ui = go.GetComponent<UIController>();
			ui.playerCamera = playerCamera;
			ui.playerStateController = playerStateController;
		} else {
			this.enabled = false;
		}
	}

	void Start() {
		maxHealth = health;
		maxMana = mana;

		playerStateController.stateDelayTimer[(int)CHARACTER_STATE.Attacking] = 1.0f;
		playerStateController.stateDelayTimer[(int)CHARACTER_STATE.Jumping] = 1.0f;

		playerStateController.playerStateHandlerEvent += onStateChanged;
		selfRigidbody = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if(dead) return;

		onStateCycle();
	}

	private void onStateCycle() {
		Vector3 newPosition = Vector3.zero;
		switch(currentState) {
		case CHARACTER_STATE.Idle:
			break;
		case CHARACTER_STATE.MoveForward:
			selfRigidbody.MovePosition(selfRigidbody.position + transform.forward * moveSpeed * Time.deltaTime);
			break;
		case CHARACTER_STATE.MoveBackward:
			selfRigidbody.MovePosition(selfRigidbody.position - transform.forward * moveSpeed * Time.deltaTime);
			break;
		case CHARACTER_STATE.MoveLeft:
			selfRigidbody.MovePosition(selfRigidbody.position  - transform.right * moveSpeed * Time.deltaTime);
			break;
		case CHARACTER_STATE.MoveRight:
			selfRigidbody.MovePosition(selfRigidbody.position + transform.right * moveSpeed * Time.deltaTime);
			break;
		case CHARACTER_STATE.MoveTopLeftDiagonal:
			selfRigidbody.MovePosition(selfRigidbody.position + ((transform.right * -1 + transform.forward) * moveSpeed * Time.deltaTime));
			break;
		case CHARACTER_STATE.MoveTopRightDiagonal:
			selfRigidbody.MovePosition(selfRigidbody.position + ((transform.right + transform.forward) * moveSpeed * Time.deltaTime));
			break;
		case CHARACTER_STATE.MoveBotLeftDiagonal:
			selfRigidbody.MovePosition(selfRigidbody.position - ((transform.right + transform.forward) * moveSpeed * Time.deltaTime));
			break;
		case CHARACTER_STATE.MoveBotRightDiagonal:
			selfRigidbody.MovePosition(selfRigidbody.position - ((transform.right * -1 + transform.forward) * moveSpeed * Time.deltaTime));
			break;
		case CHARACTER_STATE.Attacking:
			break;
		case CHARACTER_STATE.Jumping:
			//Jump();
			break;
		default:
			break;

		}


	}

	private bool checkStateChangeValidity(CHARACTER_STATE newState) {
		bool result = false;

		switch(newState) {
		case CHARACTER_STATE.Idle:
			result = true;
			break;
		case CHARACTER_STATE.MoveForward:
			result = true;
			break;
		case CHARACTER_STATE.MoveBackward:
			result = true;
			break;
		case CHARACTER_STATE.MoveLeft:
			result = true;
			break;
		case CHARACTER_STATE.MoveRight:
			result = true;
			break;
		case CHARACTER_STATE.MoveTopLeftDiagonal:
			result = true;
			break;
		case CHARACTER_STATE.MoveTopRightDiagonal:
			result = true;
			break;
		case CHARACTER_STATE.MoveBotLeftDiagonal:
			result = true;
			break;
		case CHARACTER_STATE.MoveBotRightDiagonal:
			result = true;
			break;
		case CHARACTER_STATE.Attacking:
			//if(playerStateController.stateDelayTimer[(int)CHARACTER_STATE.Attacking] < Time.time) {
				result = true;
			//} 
			break;
		case CHARACTER_STATE.Jumping:
			result = true;
			break;
		default:
			break;
		}

		return result;
	}

	private bool checkStateTransitionValidity(CHARACTER_STATE newState) {
		bool result = false;
		
		switch(currentState) {
		case CHARACTER_STATE.Idle:
			result = true;
			break;
		case CHARACTER_STATE.MoveForward:
			result = true;
			break;
		case CHARACTER_STATE.MoveBackward:
			result = true;
			break;
		case CHARACTER_STATE.MoveLeft:
			result = true;
			break;
		case CHARACTER_STATE.MoveRight:
			result = true;
			break;
		case CHARACTER_STATE.MoveTopLeftDiagonal:
			result = true;
			break;
		case CHARACTER_STATE.MoveTopRightDiagonal:
			result = true;
			break;
		case CHARACTER_STATE.MoveBotLeftDiagonal:
			result = true;
			break;
		case CHARACTER_STATE.MoveBotRightDiagonal:
			result = true;
			break;
		case CHARACTER_STATE.Attacking:
			result = true;
			break;
		case CHARACTER_STATE.Jumping:
			result = true;
			break;
		default:
			break;
		}
		
		return result;
	}

	private void onStateChanged(CHARACTER_STATE newState) {
		if(dead) return;

		if(currentState == newState) {
			return;
		}

		if(!checkStateChangeValidity(newState)) {
			return;
		}

		if(!checkStateTransitionValidity(newState)) {
			return;
		}

		switch(newState) {
		case CHARACTER_STATE.Idle:
			playerAnim.playIdleAnimation();
			break;
		case CHARACTER_STATE.MoveForward:
			playerAnim.playMoveForwardAnimation();
			break;
		case CHARACTER_STATE.MoveBackward:
			playerAnim.playMoveBackwardAnimation();
			break;
		case CHARACTER_STATE.MoveLeft:
			playerAnim.playMoveForwardAnimation();
			break;
		case CHARACTER_STATE.MoveRight:
			playerAnim.playMoveForwardAnimation();
			break;
		case CHARACTER_STATE.MoveTopLeftDiagonal:
			playerAnim.playMoveForwardAnimation();
			break;
		case CHARACTER_STATE.MoveTopRightDiagonal:
			playerAnim.playMoveForwardAnimation();
			break;
		case CHARACTER_STATE.MoveBotLeftDiagonal:
			playerAnim.playMoveBackwardAnimation();
			break;
		case CHARACTER_STATE.MoveBotRightDiagonal:
			playerAnim.playMoveBackwardAnimation();
			break;
		case CHARACTER_STATE.Attacking:
			playerAnim.playAttackAnimation();
			playerStateController.stateDelayTimer[(int)CHARACTER_STATE.Attacking] = Time.time + fireRate;
			break;
		case CHARACTER_STATE.Jumping:
			playerAnim.playJumpAnimation();
			playerStateController.stateDelayTimer[(int)CHARACTER_STATE.Jumping] = Time.time + 1.0f;
			break;
		default:
			break;
		}

		currentState = newState;
	}
	
	public void TakeDamage(int damage) {

		if(photonView.isMine) {
			health -= damage;
			if( health <= 0 ) {
				if(playerDiedEvent != null) {
					playerDiedEvent(gameObject);
				}
				health = maxHealth;
				PlayFireworks();
			}

			ui.UpdateHealthAndMana(health / maxHealth, 1);
		}
	}

	[RPC]
	public void Fireworks() {
		fireworks.Play();
	}
	
	private void PlayFireworks() {
		photonView.RPC("Fireworks", PhotonTargets.All, null);
	}

	public void Rotate() {
		//Quaternion rotation = Quaternion.Euler(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * aimSpeed * Time.deltaTime) * rigidbody.rotation;
		//rigidbody.MoveRotation(Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, 0));
	}
	
	public void Sprint() {
		
	}
	
	public void Shoot() {
		
	}
	
	public void Jump() {
		
	}

	public void Move(){
	}

	void OnDestroy() {
		playerStateController.playerStateHandlerEvent -= onStateChanged;
	}
}
