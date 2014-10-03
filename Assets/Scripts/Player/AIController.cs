using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

	public float health;
	public float stamina;
	public float aimSpeed;
	public float moveSpeed;
	public float fireRate;

	public PlayerAnimation playerAnim;
	public AIStateController currentStateController;

	public ParticleSystem deathParticles;
	public PhotonView currentView;

	private AI_STATE currentState = AI_STATE.Idle;

	private GameObject player;
	private Vector3 lastWaypoint = Vector3.zero;
	private bool reachedPoint = true;
	private bool dead = false;

	// Use this for initialization
	void Start () {
		currentStateController.stateDelayTimer[(int)AI_STATE.Attacking] = 1.0f;
		currentStateController.stateDelayTimer[(int)AI_STATE.Jumping] = 1.0f;
		currentStateController.stateDelayTimer[(int)AI_STATE.MoveForward] = 1.0f;
		
		currentStateController.playerStateHandlerEvent += onStateChanged;
		currentStateController.playerDetectedHandlerEvent += onPlayerDetected;
	}
	
	// Update is called once per frame
	void Update () {
		if(dead)
			return;

		onStateCycle();
	}

	private void ChooseNewPoint() {
		print ("Choose Point");
		lastWaypoint = new Vector3(Random.Range(-40, 40), rigidbody.position.y, Random.Range(-40, 40));
		reachedPoint = false;
	}
	
	private void MoveToPoint(Vector3 point) {
		transform.LookAt(point);
		rigidbody.MovePosition(rigidbody.position + transform.forward * moveSpeed * Time.deltaTime);

		if(Vector3.Distance(rigidbody.position, point) <= 1) {
			reachedPoint = true;
		}
	}

	private void ChasePlayer() {
		MoveToPoint(player.transform.position);
	}

	private void onStateCycle() {

		switch(currentState) {
		case AI_STATE.Idle:
			break;
		case AI_STATE.MoveForward:
			if(reachedPoint) {
				ChooseNewPoint();
			} else {
				MoveToPoint(lastWaypoint);
			}
			break;
		case AI_STATE.MoveBackward:
			break;
		case AI_STATE.MoveLeft:
			break;
		case AI_STATE.MoveRight:
			break;
		case AI_STATE.MoveTopLeftDiagonal:
			break;
		case AI_STATE.MoveTopRightDiagonal:
			break;
		case AI_STATE.MoveBotLeftDiagonal:
			break;
		case AI_STATE.MoveBotRightDiagonal:
			break;
		case AI_STATE.Attacking:
			transform.LookAt(player.transform.position);
			if(currentStateController.stateDelayTimer[(int)AI_STATE.Attacking] < Time.time) {
				playerAnim.playAttackAnimation();
				currentStateController.stateDelayTimer[(int)AI_STATE.Attacking] = Time.time + fireRate;
			}
			break;
		case AI_STATE.Jumping:
			break;
		case AI_STATE.Chasing:
			ChasePlayer();
			break;
		case AI_STATE.StopChasing:
			break;
		default:
			break;
			
		}
	}
	
	private bool checkStateChangeValidity(AI_STATE newState) {
		bool result = false;
		
		switch(newState) {
		case AI_STATE.Idle:
			result = reachedPoint;
			break;
		case AI_STATE.MoveForward:
			if(reachedPoint && currentStateController.stateDelayTimer[(int)AI_STATE.MoveForward] < Time.time) {
				result = true;
			} 
			break;
		case AI_STATE.MoveBackward:
			result = true;
			break;
		case AI_STATE.MoveLeft:
			result = true;
			break;
		case AI_STATE.MoveRight:
			result = true;
			break;
		case AI_STATE.MoveTopLeftDiagonal:
			result = true;
			break;
		case AI_STATE.MoveTopRightDiagonal:
			result = true;
			break;
		case AI_STATE.MoveBotLeftDiagonal:
			result = true;
			break;
		case AI_STATE.MoveBotRightDiagonal:
			result = true;
			break;
		case AI_STATE.Attacking:
			if(currentStateController.stateDelayTimer[(int)AI_STATE.Attacking] < Time.time) {
				result = true;
			} 
			break;
		case AI_STATE.Jumping:
			result = true;
			break;
		case AI_STATE.Chasing:
			//if(player == null) {
				result = true;
			//}
			break;
		case AI_STATE.StopChasing:
			result = true;
			break;
		default:
			break;
		}
		
		return result;
	}
	
	private bool checkStateTransitionValidity(AI_STATE newState) {
		bool result = false;
		
		switch(currentState) {
		case AI_STATE.Idle:
			result = true;
			break;
		case AI_STATE.MoveForward:
			result = true;
			break;
		case AI_STATE.MoveBackward:
			result = true;
			break;
		case AI_STATE.MoveLeft:
			result = true;
			break;
		case AI_STATE.MoveRight:
			result = true;
			break;
		case AI_STATE.MoveTopLeftDiagonal:
			result = true;
			break;
		case AI_STATE.MoveTopRightDiagonal:
			result = true;
			break;
		case AI_STATE.MoveBotLeftDiagonal:
			result = true;
			break;
		case AI_STATE.MoveBotRightDiagonal:
			result = true;
			break;
		case AI_STATE.Attacking:
			result = true;
			break;
		case AI_STATE.Jumping:
			result = true;
			break;
		case AI_STATE.Chasing:
			result = true;
			break;
		case AI_STATE.StopChasing:
			result = true;
			break;
		default:
			break;
		}
		
		return result;
	}
	
	private void onStateChanged(AI_STATE newState) {

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
		case AI_STATE.Idle:
			playerAnim.playIdleAnimation();
			break;
		case AI_STATE.MoveForward:
			playerAnim.playMoveForwardAnimation();
			currentStateController.stateDelayTimer[(int)AI_STATE.MoveForward] = Time.time + 10.0f;
			break;
		case AI_STATE.MoveBackward:
			playerAnim.playMoveBackwardAnimation();
			break;
		case AI_STATE.MoveLeft:
			playerAnim.playMoveForwardAnimation();
			break;
		case AI_STATE.MoveRight:
			playerAnim.playMoveForwardAnimation();
			break;
		case AI_STATE.MoveTopLeftDiagonal:
			playerAnim.playMoveForwardAnimation();
			break;
		case AI_STATE.MoveTopRightDiagonal:
			playerAnim.playMoveForwardAnimation();
			break;
		case AI_STATE.MoveBotLeftDiagonal:
			playerAnim.playMoveBackwardAnimation();
			break;
		case AI_STATE.MoveBotRightDiagonal:
			playerAnim.playMoveBackwardAnimation();
			break;
		case AI_STATE.Attacking:
			playerAnim.playIdleAnimation();
			playerAnim.playAttackAnimation();
			currentStateController.stateDelayTimer[(int)AI_STATE.Attacking] = Time.time + fireRate;
			break;
		case AI_STATE.Jumping:
			playerAnim.playJumpAnimation();
			currentStateController.stateDelayTimer[(int)AI_STATE.Jumping] = Time.time + 1.0f;
			break;
		case AI_STATE.Chasing:
			playerAnim.playMoveForwardAnimation();
			break;
		case AI_STATE.StopChasing:
			playerAnim.playIdleAnimation();
			player = null;
			reachedPoint = true;
			break;
		default:
			break;
		}
		
		currentState = newState;
	}

	private void onPlayerDetected(AI_STATE newState, GameObject go) {
		if(player != go) {
			player = go;
		}

		onStateChanged(newState);
	}

	private void TakeDamage(int damage) {
		currentView.RPC("TakeDamage_RPC", PhotonTargets.AllBuffered, damage);
	}

	[RPC]
	private void TakeDamage_RPC(int damage) {
		health -= damage;
		if( health <= 0 && !dead) {
			if(deathParticles != null) {
				deathParticles.enableEmission = true;
				deathParticles.Play();
			}
			dead = true;
			playerAnim.StopAnimation();
			SendMessageUpwards("Die");
		}
	}

	void OnDestroy() {
		currentStateController.playerStateHandlerEvent -= onStateChanged;
		currentStateController.playerDetectedHandlerEvent -= onPlayerDetected;
	}

}
