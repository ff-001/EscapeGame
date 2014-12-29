using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 3;
	public float rotateSpeed = 8;
	public bool hasKey;

	private Animator anim;

	void Awake(){
		anim = this.GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if(Input.GetKey(KeyCode.LeftShift)){
			Debug.Log("sneak");
			anim.SetBool("Sneak", true);
		}else{
			anim.SetBool("Sneak", false);
		}

		if((Mathf.Abs(h) > 0.1) || Mathf.Abs(v) > 0.1){
			float newSpeed = Mathf.Lerp(anim.GetFloat("Speed"), 5.6f,moveSpeed*Time.deltaTime);
			anim.SetFloat("Speed",newSpeed);

			Vector3 targetDir = new Vector3(h,0,v);
			Vector3 nowDir = transform.forward;

			float angle = Vector3.Angle(targetDir,nowDir);

			Quaternion newRotation = Quaternion.LookRotation(targetDir,Vector3.up);
			transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed*Time.deltaTime);

		}else{
			anim.SetFloat("Speed",0);
		}

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Locamotion")){
			
			PlayFootSound();
			
		}else{
			
			StopFootSount();
			
		}
	}

	void PlayFootSound(){
		if(!audio.isPlaying){
			audio.Play();
		}
	}

	void StopFootSount(){
		if(audio.isPlaying){
			audio.Stop();
		}
	}

}
