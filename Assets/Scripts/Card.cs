using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public AudioClip musicForCard;

	void OnTriggerEnter(Collider other){
		if(other.tag == Tags.player){
			Player player = other.GetComponent<Player>();
			player.hasKey = true;
			AudioSource.PlayClipAtPoint(musicForCard, transform.position,40);
			Destroy(this.gameObject);
		}
	}
}
