using UnityEngine;
using System.Collections;

public class cctvCam : MonoBehaviour {

	void OnTriggerStay(Collider other){
		if(other.tag == Tags.player){
			GameController._instance.SeePlayer(other.transform);
		}
	}
}
