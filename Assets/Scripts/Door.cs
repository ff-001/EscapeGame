using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	int count;
	Animator anim;
	public bool needKey;
	public AudioSource accessDeny;

	void Awake(){
		anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("Close",count<=0);
		if(anim.IsInTransition(0)){
			audio.Play();
		}
	}

	void OnTriggerEnter(Collider other){
		if(needKey){
			if(other.tag == Tags.player){
				Player player = other.GetComponent<Player>();
				if(player.hasKey){
					count ++;
				}else{
					accessDeny.Play();
				}
			}
				
		}else{
			if(other.tag == Tags.player){
				count ++;
			}else if(other.tag == Tags.enemy && other.collider is CapsuleCollider){
				count ++;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if(needKey){
			if(other.tag == Tags.player){
				Player player = other.GetComponent<Player>();
				if(player.hasKey){
					count --;
				}
			}
			
		}else{
			if(other.tag == Tags.player){
				count --;
			}else if(other.tag == Tags.enemy && other.collider is CapsuleCollider){
				count --;
			}
		}
	}


}
