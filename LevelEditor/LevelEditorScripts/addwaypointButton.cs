using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addwaypointButton : MonoBehaviour {
	levelEditorUIControls editorControls;
	public GameObject waypointObject;
	public void OnPress(){
		GameObject x;
		editorControls = GameObject.Find ("levelEditorControls").GetComponent<levelEditorUIControls> ();
		if (editorControls.selectedObject.GetComponent<Bot_editor> () != null) {


			x = Instantiate (waypointObject, editorControls.selectedObject.transform.position, Quaternion.identity) as GameObject;

			editorControls.selectedObject.GetComponent<Bot_editor>().waypointObjects.Add(x);
			editorControls.selectedObject.GetComponent<Bot_editor> ().UpdatePath ();
		}

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
