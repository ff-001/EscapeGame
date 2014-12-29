using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public static Timer _instance;
	private float timer;
	public Text result;
	Text text;
	bool start;
	string niceTime;

	public void RestartTimer(){
		timer = 0;
	}

	public void StopTimer(){
		start = false;
	}

	void Awake(){
		_instance = this;
		text = GetComponent <Text> ();

	}
	// Use this for initialization
	void Start () {
		timer = 0;
		start = true;
	}

	public void ShowResult(){
		result.text = niceTime;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(start == true){
			timer += Time.deltaTime;
			int minutes = Mathf.FloorToInt(timer / 60F);
			int seconds = Mathf.FloorToInt(timer - minutes * 60);
			int millseconds = Mathf.FloorToInt((timer - seconds)*100);
			niceTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, millseconds);
			text.text = niceTime;
		}
	}
	
}
