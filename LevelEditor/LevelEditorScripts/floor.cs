using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor : WorldObject {




	// Use this for initialization
	void Start () {
        selectable = false;

        ObjectData.objectName = _worldObject.objectName_.Floor;
		dragable = false;


	
	}
	
	// Update is called once per frame
	void Update () {
		ObjectData.transform = this.transform;
	}
}
