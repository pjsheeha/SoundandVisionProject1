using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationActions : MonoBehaviour {

	Animator m_animator;

	// Use this for initialization
	void Start () {
		m_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		bool isEatingPressed = Input.GetKeyDown ("e");
		m_animator.SetBool ("isEating", isEatingPressed);
	}
}
