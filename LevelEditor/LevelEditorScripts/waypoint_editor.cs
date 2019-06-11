using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint_editor : WorldObject {
    
	// Use this for initialization
	void Start () {
        ObjectData.objectName = _worldObject.objectName_.waypoint;
		this.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        dragable = true;
	}
	
	// Update is called once per frame
	void Update () {
		ObjectData.transform = this.transform;
      
	}
}
