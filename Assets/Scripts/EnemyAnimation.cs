using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {

	NavMeshAgent navAgent;
	Animator anim;
	public float speedDampTime = 0.3f;
	public float angleSpeedDampTime = 0.3f;
	EnemySight enemySight;

	void Awake(){
		navAgent = this.GetComponent<NavMeshAgent>();
		anim = this.GetComponent<Animator>();
		enemySight = this.GetComponent<EnemySight>();
	}

	void Update(){
		float angleRad;
		if(navAgent.desiredVelocity == Vector3.zero){
			anim.SetFloat("Speed", 0 , speedDampTime, Time.deltaTime);
			anim.SetFloat("AnglarSpeed", 0 , angleSpeedDampTime, Time.deltaTime);
		}else{
			float angle = Vector3.Angle(transform.forward, navAgent.desiredVelocity);
			if(angle > 20){
				anim.SetFloat("Speed", 0 , speedDampTime, Time.deltaTime);
			}else{
				Vector3 projection = Vector3.Project(navAgent.desiredVelocity, transform.forward);
				anim.SetFloat("Speed", projection.magnitude, speedDampTime, Time.deltaTime);
			}
			angleRad = angle * Mathf.Deg2Rad;
			Vector3 crossRes = Vector3.Cross(transform.forward, navAgent.desiredVelocity);
			if(crossRes.y < 0){
				angleRad = -angleRad;
			}
			anim.SetFloat("AnglarSpeed", angleRad , angleSpeedDampTime, Time.deltaTime);
		}

		anim.SetBool("PlayerInSight", enemySight.playerInSight);
	}

}
