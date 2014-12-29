using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour {

	public Transform inner_left;
	public Transform outer_left;
	public Transform inner_right;
	public Transform outer_right;
	public Transform lift;

	float moveSpeed = 3f;
	float UpTime = 3f;
	float UpTimer = 0;
	bool isIn = false;
	float gameWinTimer = 0;
	float liftstop = 5.5f;
	
	// Update is called once per frame
	void Update () {
		if(isIn){
			UpTimer += Time.deltaTime;
			if(UpTimer > UpTime){
				lift.Translate (Vector3.up * Time.deltaTime);
				gameWinTimer += Time.deltaTime;
				GameController._instance.musicPanic.volume = 0f;
				GameController._instance.musicNormal.volume = 0f;
				if(gameWinTimer > 1f){
					GameController._instance.GameOver(); 
					StartCoroutine(ReloadScene());
				}
			}
		}
		float leftPosition = Mathf.Lerp(inner_left.position.x, outer_left.position.x, Time.deltaTime * moveSpeed);
		inner_left.position = new Vector3(leftPosition,inner_left.position.y,inner_left.position.z);
		float rightPosition = Mathf.Lerp(inner_right.position.x, outer_right.position.x, Time.deltaTime * moveSpeed);
		inner_right.position = new Vector3(rightPosition,inner_right.position.y,inner_right.position.z);
	}

	IEnumerator ReloadScene(){
		yield return new WaitForSeconds(6);
		Application.LoadLevel(0);
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == Tags.player){
			Timer._instance.StopTimer();
		isIn = true;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == Tags.player){
		isIn = false;
		UpTimer = 0;
		}
	}
}
