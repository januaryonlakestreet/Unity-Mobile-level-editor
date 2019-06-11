using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editorButton : MonoBehaviour {
	
	public LevelEditorController EditorControls;



	public void Awake(){
		EditorControls = GameObject.Find ("levelEditorControls").GetComponent<LevelEditorController> ();
	}

}
