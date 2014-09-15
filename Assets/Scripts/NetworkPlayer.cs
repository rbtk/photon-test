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
//	Camera myCam = GameObject.Find("_cameraMain");

	
	// Use this for initialization
	void Start () {
		animation = GetComponent<Animation> ();
		_state = CharacterState.Idle;
		myCamera = GameObject.Find ("_cameraMain").camera;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!photonView.isMine) 
		{
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);

			switch (_state)
			{
			case CharacterState.Idle:
				animation.Play("idle");
				break;
			case CharacterState.Walking:
				animation.Play("walk");
				break;
			case CharacterState.Running:
				animation.Play("run");
				break;
			case CharacterState.Jumping:
				animation.Play("jump");
				break;
			case CharacterState.Attacking:
				animation.Play("attack");
				break;
			case CharacterState.Trotting:
				animation.Play("walk");
				break;
			default:
				animation.Play("idle");
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
			ThirdPersonController mc = GetComponent<ThirdPersonController>();
			stream.SendNext((int)mc._characterState);
			stream.SendNext("Nasko");
		} 
		else 
		{
			realPosition = (Vector3)(stream.ReceiveNext());
			realRotation = (Quaternion)(stream.ReceiveNext());
			CharacterState st = (CharacterState)stream.ReceiveNext();
			if (!photonView.isMine)
			{
				_state = st;
			}

			playerName = (string)stream.ReceiveNext();

		}
	}

	void OnGUI()
	{
		if (playerName != null)
		{

			Debug.Log ("Player name not null!");
			RaycastHit hit;

			Debug.Log("transoform "+ transform.position.x + " " + transform.position.y+ " " +transform.position.z);

			if (!photonView.isMine) 
			{
				Ray ray = new Ray(myCamera.transform.position, Vector3.forward);

				if (Physics.Raycast(ray))
				{
					Debug.Log("WOW");
				}
			}
		}
	}

	void SetName (string name)
	{
		playerName = name;
	}
}