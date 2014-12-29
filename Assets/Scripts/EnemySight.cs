
using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	public bool playerInSight = false;
	
	public float fieldOfView = 110;
	
	public Vector3 alertPosition = Vector3.zero;
	
	private Animator playerAnim;
	
	private NavMeshAgent navAgent;
	
	private SphereCollider collider;

	private Vector3 prevPlayerPosition = Vector3.zero;
	
	void Awake(){
		
		playerAnim =  GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		
		navAgent = this.GetComponent<NavMeshAgent>();
		
		collider = this.GetComponent<SphereCollider>();
		
	}

	void Start(){
		prevPlayerPosition = GameController._instance.lastPlayerPosition;
	}

	void Update(){
		if(GameController._instance.lastPlayerPosition != prevPlayerPosition){
			alertPosition = GameController._instance.lastPlayerPosition;
			prevPlayerPosition = GameController._instance.lastPlayerPosition;
		}
	}

	
	//robot listening
	
	public void OnTriggerStay(Collider other){
		
		if(other.tag == Tags.player){
			
			Vector3 forward = transform.forward;
			
			Vector3 playerDir = other.transform.position - transform.position;
			
			float temp = Vector3.Angle(forward, playerDir);  //get angle based on two vector
			
			if(temp < 0.5f * fieldOfView){

				RaycastHit hitInfo;
				
				bool res = Physics.Raycast(transform.position + Vector3.up, other.transform.position - transform.position, out hitInfo);
				
				if(res == false || hitInfo.collider.tag == Tags.player){
				
					playerInSight = true;
				
					alertPosition = other.transform.position;
				}
				
			}else{
				
				playerInSight = false;
				
			}      
			
			if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Locamotion")){
				
				NavMeshPath path = new NavMeshPath();
				
				if(navAgent.CalculatePath(other.transform.position,path)){
					
					Vector3[] wayPoints = new Vector3[path.corners.Length + 2];
					
					wayPoints[0] = transform.position;
					
					wayPoints[wayPoints.Length - 1] =  other.transform.position;
					
					for(int i=0; i<path.corners.Length; i++) {
						
						wayPoints[i+1] = path.corners[i];
						
					} 
					
					float length = 0;
					
					for(int i=1; i<wayPoints.Length; i++){
						
						length += (wayPoints[i] - wayPoints[i-1]).magnitude;
						
					}
					
					if(length < collider.radius){
						
						alertPosition = other.transform.position;
						
					}
					
				}
				
			}
			
		}
		
	}
	
	void OnTriggerExit(Collider other){
		
		if(other.tag == Tags.player){
			
			playerInSight =  false;
			
		}
		
	}
	// end of listening
	

}
