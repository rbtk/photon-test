using UnityEngine;
using System.Collections;

public enum AI_STATE {
	Idle,
	Attacking,
	Jumping,
	Chasing,
	StopChasing,
	MoveForward,
	MoveBackward,
	MoveLeft,
	MoveRight,
	MoveTopLeftDiagonal,
	MoveTopRightDiagonal,
	MoveBotLeftDiagonal,
	MoveBotRightDiagonal,
	Dead,
	NumberOfStates
};

public class AIStateController : MonoBehaviour {
	
	public delegate void playerStateHandler(AI_STATE newState);
	public event playerStateHandler playerStateHandlerEvent;

	public delegate void playerDetectedHandler(AI_STATE newState, GameObject target);
	public event playerDetectedHandler playerDetectedHandlerEvent;
	
	public float[] stateDelayTimer = new float[(int)AI_STATE.NumberOfStates];

	private GameObject player;

	void LateUpdate() {
		if(player == null) {
			if(Random.Range(0, 10) % 10 == 1) {
				fireStateChangeEvent(AI_STATE.MoveForward);
			} else {
				fireStateChangeEvent(AI_STATE.Idle);
			}
			TryDetectPlayer();
		} else {
			float dist = Vector3.Distance(transform.position, player.transform.position);
			if(dist > 10) {
				player = null;
				fireStateChangeEvent(AI_STATE.StopChasing);
				return;
			} else if(dist < 20 && dist > 1) {
				firePlayerDetectedEvent(AI_STATE.Chasing, player);
			}else {
				firePlayerDetectedEvent(AI_STATE.Attacking, player);
			}
		}
	}

	private void TryDetectPlayer() {
		Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

		foreach(Collider col in colliders) {
			if(col.tag == "Player") {
				player = col.gameObject;
				break;
			}
		}
	}
	
	private void fireStateChangeEvent(AI_STATE newState) {
		if(playerStateHandlerEvent != null) {
			playerStateHandlerEvent(newState);
		}
	}

	private void firePlayerDetectedEvent(AI_STATE newState, GameObject go) {
		if(playerDetectedHandlerEvent != null) {
			playerDetectedHandlerEvent(newState, go);
		}
	}
}

