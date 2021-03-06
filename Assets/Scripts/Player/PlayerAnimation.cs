﻿using UnityEngine;
using System.Collections;

public class PlayerAnimation : Photon.MonoBehaviour {

	public AnimationClip idleAnimation;
	public AnimationClip movingAnimation;
	public AnimationClip jumpingAnimation;
	public AnimationClip attackAnimation;

	public float walkSpeed = 1.5f;

	private Animation selfAnimation;

	// Use this for initialization
	void Start () {
		selfAnimation = GetComponent<Animation>();
	}

	public void playIdleAnimation() {
		playAnimation(selfAnimation, idleAnimation);
	}

	public void playMoveForwardAnimation() {
		selfAnimation[movingAnimation.name].speed = walkSpeed;
		playAnimation(selfAnimation, movingAnimation);
	}

	public void playMoveBackwardAnimation() {
		selfAnimation[movingAnimation.name].speed = -walkSpeed;
		playAnimation(selfAnimation, movingAnimation);
	}

	public void playJumpAnimation() {
		playAnimation(selfAnimation, jumpingAnimation);
	}

	public void playAttackAnimation() {
		selfAnimation[attackAnimation.name].speed = 2f;
		blendAnimation(selfAnimation, attackAnimation);
	}

	public void StopAnimation() {
		selfAnimation.Stop();
	}

	private void blendAnimation(Animation animation, AnimationClip clip) {
		if(!animation.IsPlaying(clip.name)) {
			animation.Blend(clip.name, 4f);
		}
	}

	private void playAnimation(Animation animation, AnimationClip clip) {
		if(!animation.IsPlaying(clip.name)) {
			animation.Play(clip.name);
		}
	}
}
