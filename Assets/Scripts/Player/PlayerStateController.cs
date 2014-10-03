using UnityEngine;
using System.Collections;

public enum CHARACTER_STATE {
	Idle,
	Attacking,
	Jumping,
	MoveForward,
	MoveBackward,
	MoveLeft,
	MoveRight,
	MoveTopLeftDiagonal,
	MoveTopRightDiagonal,
	MoveBotLeftDiagonal,
	MoveBotRightDiagonal,
	NumberOfStates
};



public class PlayerStateController : Photon.MonoBehaviour {

	public delegate void playerStateHandler(CHARACTER_STATE newState);
	public event playerStateHandler playerStateHandlerEvent;

	public float[] stateDelayTimer = new float[(int)CHARACTER_STATE.NumberOfStates];

	private bool attacking = false;
	private CHARACTER_STATE currentWalkDir = CHARACTER_STATE.Idle;
	private bool walking = false;
	private bool jumping = false;

	void Awake() {
		if(!photonView.isMine) {
			this.enabled = false;
		}
	}

	void LateUpdate() {

		#if UNITY_EDITOR || UNITY_STANDALONE
		if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) {
			fireStateChangeEvent(CHARACTER_STATE.MoveTopLeftDiagonal);
		} else if( Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) {
			fireStateChangeEvent(CHARACTER_STATE.MoveTopRightDiagonal);
		} else if( Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
			fireStateChangeEvent(CHARACTER_STATE.MoveBotLeftDiagonal);
		} else if( Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
			fireStateChangeEvent(CHARACTER_STATE.MoveBotRightDiagonal);
		} else if(Input.GetKey(KeyCode.A)) {
			fireStateChangeEvent(CHARACTER_STATE.MoveLeft);
		} else if( Input.GetKey(KeyCode.D)) {
			fireStateChangeEvent(CHARACTER_STATE.MoveRight);
		} else if(Input.GetKey(KeyCode.W)){
			fireStateChangeEvent(CHARACTER_STATE.MoveForward);
		} else if(Input.GetKey(KeyCode.S)) {
			fireStateChangeEvent(CHARACTER_STATE.MoveBackward);
		} else {
			fireStateChangeEvent(CHARACTER_STATE.Idle);
		}

		if(Input.GetMouseButton(0)) {
			fireStateChangeEvent(CHARACTER_STATE.Attacking);
		}
		#else
		if(walking) {
			fireStateChangeEvent(currentWalkDir);
		} else {
			fireStateChangeEvent(CHARACTER_STATE.Idle);
		}

		if(attacking) {
			fireStateChangeEvent(CHARACTER_STATE.Attacking);
		}
		#endif
	}

	public void Attack()
	{
		attacking = true;
	}

	public void Move(string direction)
	{
		walking = true;

		switch(direction) {
		case "left":
			currentWalkDir = CHARACTER_STATE.MoveLeft;
			break;
		case "right":
			currentWalkDir = CHARACTER_STATE.MoveRight;
			break;
		case "up":
			currentWalkDir = CHARACTER_STATE.MoveForward;
			break;
		case "down":
			currentWalkDir = CHARACTER_STATE.MoveBackward;
			break;
		default:
			walking = false;
			break;
		}
	}

	public void StopAttack()
	{
		attacking = false;
	}

	public void StopMoving()
	{
		walking = false;
	}

	private void fireStateChangeEvent(CHARACTER_STATE newState) {
		if(playerStateHandlerEvent != null) {
			playerStateHandlerEvent(newState);
		}
	}
}

