using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlOrtate : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler{
	public Image insideObject;
	public float outRadius;
	public float insideRadius;
	private PlayerMovement move;
	private GameObject player;
	private Vector2 center;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		move = player.GetComponent <PlayerMovement> ();
		center = new Vector2 (transform.position.x-outRadius, transform.position.y+outRadius);
	}

	void FixedUpdate() 
	{
		Vector2 insideObjectPostion = new Vector2 (insideObject.transform.position.x, insideObject.transform.position.y);
		Vector2 diff = insideObjectPostion - center;
		move.ortate (diff.x, diff.y);
	}

	public void OnPointerDown (PointerEventData eventData) {
	}

	public void OnDrag (PointerEventData data) {
//		Debug.Log (transform.position+"=="+data.position);
		Vector2 diff = data.position - center;
		float mod = Mathf.Sqrt (Mathf.Pow (diff.x, 2) + Mathf.Pow (diff.y, 2));
		if (mod > (outRadius-insideRadius)) {
			float sine = diff.y / mod;
			float cose = diff.x / mod;
			float overLength = mod - (outRadius-insideRadius);
			float overx = overLength * cose;
			float overy = overLength * sine;
			insideObject.transform.position = new Vector3(data.position.x - overx, data.position.y - overy, 0);
		} else {
			insideObject.transform.position = new Vector3(data.position.x, data.position.y, 0);
		}

	}

	public void OnPointerUp (PointerEventData data) {
		Debug.Log ("center="+center);
		insideObject.transform.position = new Vector3(center.x, center.y, 0);
	}
}

