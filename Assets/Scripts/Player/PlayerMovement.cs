using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;
	Vector3 movement;
	Animator anim;
	Rigidbody player;
	int floorMask;
	float camRayLength = 100f;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		player = GetComponent<Rigidbody> ();
	}

//	void FixedUpdate() 
//	{
//		float h = Input.GetAxisRaw ("Horizontal");
//		float v = Input.GetAxisRaw ("Vertical");
//
//		Move (h, v);
//		Turning ();
//		Animating (h, v);
//	}
		
	public void action(float h, float v){
		Move (h, v);
		Animating (h, v);
	}

	public void ortate(float h, float v) {
		Turn (h, v);
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		player.MovePosition (transform.position + movement);
	}

	void Turn(float h, float v){
		if (h == 0 && v == 0) {
			return;
		}
		Vector3 plyerToMouse = new Vector3 (h, 0f, v);
		Quaternion newRotation = Quaternion.LookRotation (plyerToMouse); 
		player.MoveRotation (newRotation);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)){
			Vector3 plyerToMouse = floorHit.point - transform.position;
			plyerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (plyerToMouse); 
			player.MoveRotation (newRotation);
		}
		Debug.Log (Input.mousePosition+"=="+camRay+"&&"+floorHit);
	}

	void Animating(float h, float v) {
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}
