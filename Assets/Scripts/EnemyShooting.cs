using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

	private Animator anim;
	private bool isShot = false;
	PlayerHealth health;
	public float minDamage = 30;
	public AudioClip shotClip;
	private LineRenderer laserShotLine;              
	private Light laserShotLight;
	private Transform player; 
	EnemySight enemySight;


	void Awake(){
		anim = this.GetComponent<Animator>();
		laserShotLine = GetComponentInChildren<LineRenderer>();
		laserShotLight = laserShotLine.gameObject.light;
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		health = player.gameObject.GetComponent<PlayerHealth>();
		enemySight = this.GetComponent<EnemySight>();
		laserShotLine.enabled = false;
		laserShotLight.intensity = 0f;
	}
	void Update(){
		if((anim.GetFloat("Shot") > 0.5f) && (enemySight.playerInSight == true)){
			Shooting();
		}else{
			isShot = false;
			laserShotLine.enabled = false;
		}
		laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0f, 10f * Time.deltaTime);
	}
	void Shooting(){
		if(isShot == false){
			float damage = minDamage + 80 - 8*(transform.position - health.transform.position).magnitude;
			health.TakeDamage(damage);
			ShotEffects();
			isShot = true;
		}
	}

	void ShotEffects ()
	{
		laserShotLine.SetPosition(0, laserShotLine.transform.position);
		laserShotLine.SetPosition(1, player.position + Vector3.up * 1.5f);
		laserShotLine.enabled = true;
		laserShotLight.intensity = 5f;
		audio.PlayOneShot(shotClip);
	}
}
