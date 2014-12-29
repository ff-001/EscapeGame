using UnityEngine;
using System.Collections;

public class SwitchUnit : MonoBehaviour {

	public GameObject laser;
	public Material unlockMat;
	public GameObject screen;

	void OnTriggerStay(Collider other){
		if(other.tag == Tags.player){
			if(Input.GetKeyDown(KeyCode.Z)){
					audio.Play();
					laser.SetActive(false);
					screen.renderer.material = unlockMat;
			}
		}
	}
}
