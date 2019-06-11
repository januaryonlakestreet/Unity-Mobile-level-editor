using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicBlock : WorldObject {


	// Use this for initialization
	void Start () {
		this.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        ObjectData.objectName = _worldObject.objectName_.Block;
        dragable = true;
        selectable = true;

	}
	
	// Update is called once per frame
	void Update () {
		ObjectData.transform = this.transform;
	}


}
