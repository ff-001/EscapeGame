using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

	public static AlarmLight _intensity;

	public bool AlarmOn = false;

	public float speed = 1f;

	private float lowInstensity = 0;
	private float highInstensity = 0.5f;
	private float targetInstensity;


	// Use this for initialization
	void Awake () {
		targetInstensity = highInstensity;
		AlarmOn = false;
		_intensity = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(AlarmOn){
			light.intensity = Mathf.Lerp(light.intensity, targetInstensity, Time.deltaTime * speed);
			if(Mathf.Abs(targetInstensity - light.intensity) < 0.05f){
				if(targetInstensity == highInstensity){
					targetInstensity = lowInstensity;
				}else{
					targetInstensity = highInstensity;
				}
			}
		} else {
			light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime * speed);
		}
	}
}
