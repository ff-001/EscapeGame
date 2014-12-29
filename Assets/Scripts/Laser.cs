using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public bool isFliker;
	public float onTime = 1.0f;
	public float offTimer = 1.0f;

	private float timer;

	void Update(){
		if(isFliker){
			timer += Time.deltaTime;
			if(renderer.enabled){
				if(timer >= onTime){
					renderer.enabled = false;
					timer = 0;
				}
			}else{
				if(timer >= offTimer){
					renderer.enabled = true;
					timer = 0;
				}
			}
		}
	}

	void OnTriggerStay(Collider other){
		if(other.tag == Tags.player){
			GameController._instance.SeePlayer(other.transform);
		}
	}
}
