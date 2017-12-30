using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FireControl: MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

	private bool istouched;

	void Awake () {
		istouched = false;
	}
		
	public void OnPointerDown (PointerEventData data) {
		istouched = true;
	}

	public void OnPointerUp (PointerEventData data) {
		istouched = false;
	}

	public bool getFireStatus() {
		return istouched;
	}
}
