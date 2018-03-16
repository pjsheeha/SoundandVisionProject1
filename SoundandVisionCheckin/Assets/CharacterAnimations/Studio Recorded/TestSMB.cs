using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Cameras
{
public class TestSMB : StateMachineBehaviour {

	public float m_Damping = 0.15f;
	public float speed = 1;
	GameObject freeLook;
	GameObject player;
	private readonly int m_HashHorizontalPara = Animator.StringToHash ("Horizontal");
	private readonly int m_HashVerticalPara = Animator.StringToHash ("Vertical");
	//private readonly int m_HashJumpPara = Animator.StringToHash ("Jump");
	 

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
	void OnEnable() {
	 	player = GameObject.FindWithTag("Player");
		freeLook = GameObject.FindWithTag ("FreeLook");
	}
//	 OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
		float horizontal = Input.GetAxis ("Horizontal")*speed;
		float vertical = Input.GetAxis ("Vertical")*speed;
		//float jump = Input.GetAxis ("Jump");

		//Vector3 input = new Vector3 (horizontal, jump, vertical).normalized;

		Vector2 input = new Vector2 (horizontal, vertical).normalized;
		if (!player.GetComponent<PlayerMoving>().obj) {
			animator.SetFloat (m_HashHorizontalPara, input.x, m_Damping, Time.deltaTime);
			animator.SetFloat (m_HashVerticalPara, input.y, m_Damping, Time.deltaTime);
				if ((vertical > 0)) {
					Quaternion rotation = Quaternion.Euler (0f, freeLook.GetComponent<FreeLookCam> ().m_LookAngle, 0f);
					animator.transform.rotation = rotation;
				}
				if (vertical < 0){
					Quaternion rotation = Quaternion.Euler (0f, 180+freeLook.GetComponent<FreeLookCam> ().m_LookAngle, 0f);
					animator.transform.rotation = rotation;
				}
		} else {
			animator.SetFloat (m_HashHorizontalPara, 0, m_Damping, Time.deltaTime);
			animator.SetFloat (m_HashVerticalPara, 0, m_Damping, Time.deltaTime);
			Vector3 relativePos = player.GetComponent<PlayerMoving> ().focus.transform.position - animator.transform.position;
			relativePos.y = 0;
			Quaternion rotation = Quaternion.LookRotation (relativePos);
			animator.transform.rotation = rotation;

		}
		//animator.SetFloat (m_HashJumpPara, input.y, m_Damping, Time.deltaTime);

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
}