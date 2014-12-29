using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	private Animator anim;
	public float hp = 100;
	public AudioClip end;
	void Awake(){
		anim = this.GetComponent<Animator>();
	}

	public void TakeDamage(float damage){
		hp -= damage;
		if(hp <= 0){
			anim.SetBool("Dead", true);
			AudioSource.PlayClipAtPoint(end, transform.position);
			StartCoroutine(ReloadScene());
		}

	}

	IEnumerator ReloadScene(){
		yield return new WaitForSeconds(5);
		Application.LoadLevel(0);
	}

}
