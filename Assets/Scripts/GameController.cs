using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController _instance;

	public AudioSource musicNormal;
	public AudioSource musicPanic;
	float musicSpeed = 2;

	public GameObject GameOverShow;

	public bool alarmOn = false;
	public Vector3 lastPlayerPosition = Vector3.zero;
//	SignalRUnityController _signalr;

	private GameObject[] sirens;
	// Use this for initialization
	void Awake () {
		_instance = this;
		sirens = GameObject.FindGameObjectsWithTag(Tags.siren);
//		_signalr = (SignalRUnityController)GameObject.Find("SignalRObject")
//			.GetComponent(typeof(SignalRUnityController));
	}
	
	// Update is called once per frame
	void Update () {
		if(alarmOn){

//			_signalr.Send("hello");
//			if(_signalr.GetMag){
			AlarmLight._intensity.AlarmOn = true;
//			}
			PlaySiren();
		}else{
			AlarmLight._intensity.AlarmOn = false;
			StopSiren();
		}
	}

	void PlaySiren(){
		foreach (GameObject go in sirens){
			if(!go.audio.isPlaying){
				go.audio.Play();
			}
		}
		musicNormal.volume = Mathf.Lerp(musicNormal.volume,0,Time.deltaTime*musicSpeed);
		musicPanic.volume = Mathf.Lerp(musicPanic.volume,0.2f,Time.deltaTime*musicSpeed);
	}

	void StopSiren(){
		foreach (GameObject go in sirens){
			go.audio.Stop();
		}
		musicNormal.volume = Mathf.Lerp(musicNormal.volume,0.3f,Time.deltaTime*musicSpeed);
		musicPanic.volume = Mathf.Lerp(musicPanic.volume,0,Time.deltaTime*musicSpeed);
	}

	public void SeePlayer(Transform player){
		alarmOn = true;
		lastPlayerPosition = player.position;
	}

	public void GameOver(){
		GameOverShow.SetActive(true);
		Timer._instance.ShowResult();
	}

}
