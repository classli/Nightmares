using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Control : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler{
	public Image cirleChild;
	public float radius;
	private PlayerMovement move;
	private GameObject player;
	public Vector2 circle = new Vector2 (290, 290);

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		move = player.GetComponent <PlayerMovement> ();
	}

	void FixedUpdate() 
	{
		Vector2 childCirclePostion = new Vector2 (cirleChild.transform.position.x, cirleChild.transform.position.y);
		Vector2 diff = childCirclePostion - circle;
		move.action (diff.x, diff.y);
	}

	public void OnPointerDown (PointerEventData eventData) {
	}

	public void OnDrag (PointerEventData data) {
		Debug.Log (data.position);
		Vector2 diff = data.position - circle;
		float mod = Mathf.Sqrt (Mathf.Pow (diff.x, 2) + Mathf.Pow (diff.y, 2));
		if (mod > radius) {
			float sine = diff.y / mod;
			float cose = diff.x / mod;
			float overLength = mod - radius;
			float overx = overLength * cose;
			float overy = overLength * sine;
			cirleChild.transform.position = new Vector3(data.position.x - overx, data.position.y - overy, 0);
		} else {
			cirleChild.transform.position = new Vector3(data.position.x, data.position.y, 0);
		}
	
	}

	public void OnPointerUp (PointerEventData data) {
		cirleChild.transform.position = new Vector3(circle.x, circle.y, 0);
	}
}
