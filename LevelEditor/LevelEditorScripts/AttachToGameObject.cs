using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToGameObject : MonoBehaviour {

    public Transform Target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	

        
  
  
    void Update()
    {
        this.transform.position = new Vector3(Target.position.x, Target.position.y + 3f, Target.position.z);
       
    }
}

