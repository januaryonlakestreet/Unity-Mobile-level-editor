using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class WorldObject: MonoBehaviour {
	public Rigidbody rb;
	// Use this for initialization
	private Vector3 screenPoint;
	private Vector3 offset;
   
	[System.Serializable]
	public struct _worldObject
	{
		public	 Transform transform;
        public enum objectName_ {Block, PlayerStart, PlayerEnd, Bot, Coin,HidingSpot,waypoint,Floor,checkpoint,Pickup_Blank,Pickup_smoke,Pickup_ghost,Pickup_noisemaker,Pickup_Drone};
        public objectName_ objectName;

	
	}

		

	public _worldObject ObjectData;
	public levelEditorUIControls uicontrols;
	public bool dragable = true;
	public bool OtherSettings = false;
    public bool npc = false;
    public bool selectable = false;
	

	void Awake(){
		uicontrols = GameObject.Find ("levelEditorControls").GetComponent<levelEditorUIControls> ();
		rb = GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;

	}

	void OnMouseDown(){
		if (dragable == true || selectable == true) {
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            uicontrols.selectedObject = this.gameObject;
        }
		
    rb.isKinematic = false;
	}


	void OnMouseDrag(){
		if (dragable == true) {
			Vector3 cursorPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (cursorPoint) + offset;
			transform.position = cursorPosition;
            
            if(this.transform.position.y < 1)
            {
                this.transform.position = new Vector3(this.transform.position.x, 1f, this.transform.position.z);
            }
			uicontrols.UpdateUI (uicontrols.selected);
		}
	}

	void OnMouseUp() {
		if (dragable == true || selectable == true) {
			transform.position = new Vector3 (this.transform.position.x, 1f, this.transform.position.z);
         
        }
        rb.isKinematic = true;
    }
  
    void Start(){
		
	}
}
