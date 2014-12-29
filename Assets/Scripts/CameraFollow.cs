using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Vector3 offset;
	Transform player;
	public float moveSpeed;
	public float rotationSpeed;

	void Awake(){
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		offset = transform.position - player.position;
		offset = new Vector3(0, offset.y, offset.z);
	}

	// Update is called once per frame
	void Update () {

		Vector3 beginPos = player.position + offset;
		Vector3 endPos = player.position + offset.magnitude * Vector3.up;
		Vector3 pos1 = Vector3.Lerp(beginPos,endPos,0.25f);
		Vector3 pos2 = Vector3.Lerp(beginPos,endPos,0.5f);
		Vector3 pos3 = Vector3.Lerp(beginPos,endPos,0.75f);

		Vector3[] posArray = new Vector3[]{beginPos, pos1, pos2, pos3, endPos};

		Vector3 targetPos = beginPos;

		for(int i=0; i<5; i++){
			RaycastHit hitInfo;
			if(Physics.Raycast(posArray[i], player.position - posArray[i], out hitInfo)){
				if(hitInfo.collider.tag != Tags.player){
					continue;
				}else{
					targetPos = posArray[i];
					break;
				}
			}else{
				targetPos = posArray[i];
				break;
			}

		}
//		this.transform.position = targetPos;
		this.transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime * moveSpeed);
		Quaternion nowRotation = transform.rotation;
		transform.LookAt(player.position);
		transform.rotation = Quaternion.Lerp(nowRotation,transform.rotation,Time.deltaTime * rotationSpeed);
	}
}
