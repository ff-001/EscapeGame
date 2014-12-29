using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	public Transform[] patrolPosition;
	private NavMeshAgent navAgent;
	private int index = 0;
	private float patrolTimer = 0f;
	public float patrolTime = 3f;
	private float chaseTimer = 0f;
	public float chaseTime = 3f;
	EnemySight enemySight;
	PlayerHealth health;

	void Awake(){
		navAgent = this.GetComponent<NavMeshAgent>();
		enemySight = this.GetComponent<EnemySight>();
		navAgent.destination = patrolPosition[index].position;
		navAgent.updatePosition = false;
		navAgent.updateRotation = false;
		health = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		if(enemySight.playerInSight && health.hp > 0){
			Shooting();
		}else if(enemySight.alertPosition != Vector3.zero && health.hp > 0){
			Chasing();
		}else{
			Patrolling();
		}

	}

	void Shooting(){
		navAgent.Stop();
	}

	void Patrolling(){
		navAgent.speed = 3f;
		navAgent.destination = patrolPosition[index].position;
		navAgent.updatePosition = false;
		navAgent.updateRotation = false;
		if(navAgent.remainingDistance < 0.5f){
			navAgent.Stop();
			patrolTimer += Time.deltaTime;
			if(patrolTimer > patrolTime){
				index++;
				index %= 4;
				navAgent.destination = patrolPosition[index].position;
				navAgent.updatePosition = false;
				navAgent.updateRotation = false;
				patrolTimer = 0;
			}
		}
	}

	void Chasing(){
		navAgent.speed = 6f;
		navAgent.destination = enemySight.alertPosition;
		navAgent.updatePosition = false;
		navAgent.updateRotation = false;
		if(navAgent.remainingDistance < 2.0f){
			navAgent.Stop();
			chaseTimer += Time.deltaTime;
			if(chaseTimer > chaseTime){
				enemySight.alertPosition = Vector3.zero;
				GameController._instance.alarmOn = false;
				GameController._instance.lastPlayerPosition = Vector3.zero;
				chaseTimer = 0;
			}
		}
	}
}
