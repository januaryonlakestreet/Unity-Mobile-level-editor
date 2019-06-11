using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class levelEditorUIControls : MonoBehaviour {
	public WorldObject objectref;
	public GameObject selectedObject;



	public Text x, y, z;

	public Text detaillsID;

	public float multiplyer;

	public GameObject[] transformEditButtons;
	public string selected;
	public string test;
	public Button AddWaypointButton;
    public Button DeleteButton;
    public GameObject waypointEditor;



    public void Awake(){
		multiplyer = 2.0f;
		x.text = "";
		y.text = "";
		z.text = "";
		selectedObject = GameObject.FindGameObjectWithTag ("startFloor");
        transformEditButtons = GameObject.FindGameObjectsWithTag("TransformButton");

	}
	public void Start(){
		
	}
	public void Update(){
		if (selectedObject != null) {
			
			detaillsID.text = selectedObject.GetComponent<WorldObject> ().ObjectData.objectName + " " +selected;
		}
       
	}
    public void AddWayPoint()
    {
        if(selectedObject.GetComponent<Bot_editor>() == null)
        {
            return;
        }

        GameObject NewWaypoint = new GameObject();
        NewWaypoint = Instantiate(waypointEditor, selectedObject.gameObject.transform.position, Quaternion.identity);
        int WayPointId = selectedObject.GetComponent<Bot_editor>().waypointsLocations.Count;
       
        selectedObject.GetComponent<Bot_editor>().AddPoint(WayPointId, NewWaypoint.transform.position);
    }
	public void UpdateUI(string select){
		Debug.Log (select);
		if (y.gameObject.activeInHierarchy == false) {
			ReEnableSetting ();
		}
		switch (select) {

		case "position":
			selected = select;
			x.text = selectedObject.GetComponent<WorldObject> ().ObjectData.transform.position.x.ToString();
			y.text = selectedObject.GetComponent<WorldObject> ().ObjectData.transform.position.y.ToString();
			z.text = selectedObject.GetComponent<WorldObject> ().ObjectData.transform.position.z.ToString();
		break;

		case "rotation":
			selected = select;
			x.text = selectedObject.GetComponent<WorldObject> ().ObjectData.transform.rotation.x.ToString();
			y.text = selectedObject.GetComponent<WorldObject> ().ObjectData.transform.rotation.y.ToString();
			z.text = selectedObject.GetComponent<WorldObject> ().ObjectData.transform.rotation.z.ToString();
		break;

		case "scale":
			selected = select;
			x.text = selectedObject.GetComponent<WorldObject> ().ObjectData.transform.localScale.x.ToString();
			y.text = selectedObject.GetComponent<WorldObject> ().ObjectData.transform.localScale.y.ToString();
			z.text = selectedObject.GetComponent<WorldObject> ().ObjectData.transform.localScale.z.ToString();
		break;

            case "Delete":
                GameObject.Destroy(selectedObject.gameObject);
            break;

        case "other":
			
			selected = select;
			x.gameObject.SetActive (false);
			y.gameObject.SetActive (false);
			z.gameObject.SetActive (false);
			for (int a = 0; a < transformEditButtons.Length; a++) {
				transformEditButtons [a].gameObject.SetActive (false);
			}


			if (selectedObject.GetComponent<Bot_editor> () != null) {
				AddWaypointButton.gameObject.SetActive (true);
			}
               

                break;

		
		}
	}
	void showOtherSettings(){

	}
	void ReEnableSetting(){
		x.gameObject.SetActive (true);
		y.gameObject.SetActive (true);
		z.gameObject.SetActive (true);
		for (int a = 0; a < transformEditButtons.Length; a++) {
			transformEditButtons [a].gameObject.SetActive (true);
		}
		AddWaypointButton.gameObject.SetActive (false);
	}





	/// <summary>
	/// below are the functions to change the selected objects transform
	/// </summary>
	public void IncreaseX(){
		switch (selected) {
		case "scale":
			selectedObject.transform.localScale += new Vector3 (multiplyer,0,0);
		break;
		case "position":
			selectedObject.transform.position += new Vector3 (multiplyer,0,0);
			break;
		case "rotation":
			selectedObject.transform.Rotate (new Vector3 (multiplyer, 0, 0));
			break;
		}
		UpdateUI (selected);
	}
	public void IncreaseY(){
		switch (selected) {
		case "scale":
			selectedObject.transform.localScale += new Vector3 (0,multiplyer,0);
			break;
		case "position":
			selectedObject.transform.position += new Vector3 (0,multiplyer,0);
		break;
		case "rotation":
			selectedObject.transform.Rotate (new Vector3 (0, multiplyer, 0));
		break;
		}
		UpdateUI (selected);
	}
	public void IncreaseZ(){
		switch (selected) {
		case "scale":
			selectedObject.transform.localScale += new Vector3 (0,0,multiplyer);
			break;
		case "position":
			selectedObject.transform.position += new Vector3 (0,0,multiplyer);
		break;
		case "rotation":
			selectedObject.transform.Rotate (new Vector3 (0, 0,multiplyer));
		break;
		}
		UpdateUI (selected);
	}
	public void DecreaseX(){
		switch (selected) {
		case "scale":
			selectedObject.transform.localScale -= new Vector3 (multiplyer,0,0);
		break;
		case "position":
			selectedObject.transform.position -= new Vector3 (multiplyer,0,0);
		break;
		case "rotation":
			selectedObject.transform.Rotate (new Vector3 (-multiplyer, 0,0));
		break;
		}
		UpdateUI (selected);
	}
	public void DecreaseY(){
		switch (selected) {
		case "scale":
			selectedObject.transform.localScale -= new Vector3 (0,multiplyer,0);
		break;
		case "position":
			selectedObject.transform.position -= new Vector3 (0,multiplyer,0);
		break;
		case "rotation":
			selectedObject.transform.Rotate (new Vector3 (0, -multiplyer,0));
		break;
		}
		UpdateUI (selected);
	}
	public void DecreaseZ(){
		switch (selected) {
		case "scale":
			selectedObject.transform.localScale -= new Vector3 (0,0,multiplyer);
		break;
		case "position":
			selectedObject.transform.position -= new Vector3 (0,0,multiplyer);
		break;
		case "rotation":
			selectedObject.transform.Rotate (new Vector3 (0,0, -multiplyer));
		break;
		}
		UpdateUI (selected);
	}












}
