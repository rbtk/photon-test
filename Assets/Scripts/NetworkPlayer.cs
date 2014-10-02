using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	float lastUpdateTime;
	Animation animation;

	//character public animations
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpAnimation;
	public AnimationClip attackAnimation;
	public PlayerAnimation playerAnim;

	Vector3 worldPosition;
	Vector3 screenPosition;
	Transform myTransform;

	Camera myCamera = null;
	string playerName = null;

	enum CharacterState {
		Idle = 0,
		Walking = 1,
		Trotting = 2,
		Running = 3,
		Jumping = 4,
		Attacking = 5,
	}


	CharacterState _state;

	private CHARACTER_STATE currentState = CHARACTER_STATE.Idle;
	private PlayerController contr;

//	Camera myCam = GameObject.Find("_cameraMain");

	// Use this for initialization
	void Awake () {
		animation = GetComponent<Animation> ();
		contr = GetComponent<PlayerController>();
		_state = CharacterState.Idle;
//		myCamera = GameObject.Find ("_cameraMain").camera;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!photonView.isMine) {
			rigidbody.position = Vector3.Lerp(rigidbody.position, realPosition, 0.1f);
			rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, realRotation, 0.1f);
			
			switch(currentState) {
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
				break;
			case CHARACTER_STATE.Jumping:
				playerAnim.playJumpAnimation();
				break;
			default:
				break;
			}
		}
	}


	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) 
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext((int)contr.currentState);
			stream.SendNext("Nasko");
		} 
		else 
		{
			realPosition = (Vector3)(stream.ReceiveNext());
			realRotation = (Quaternion)(stream.ReceiveNext());
			CHARACTER_STATE st = (CHARACTER_STATE)stream.ReceiveNext();
			if (!photonView.isMine)
			{
				print ("Animation state: " + st.ToString());
				currentState = st;
			}

			playerName = (string)stream.ReceiveNext();

		}
	}

//	void OnGUI()
//	{
//		if (playerName != null)
//		{
//
//			Debug.Log ("Player name not null!");
//			RaycastHit hit;
//
//			Debug.Log("transoform "+ transform.position.x + " " + transform.position.y+ " " +transform.position.z);
//
//			if (!photonView.isMine) 
//			{
//				Ray ray = new Ray(myCamera.transform.position, Vector3.forward);
//
//				if (Physics.Raycast(ray))
//				{
//					Debug.Log("WOW");
//				}
//			}
//		}
//	}

	void SetName (string name)
	{
		playerName = name;
	}

}