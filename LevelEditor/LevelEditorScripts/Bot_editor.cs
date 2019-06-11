using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bot_editor : NPC {
	public int ID;
	public List <Vector3> waypointsLocations;
	public Vector3 startLocation;
    public List<GameObject> waypointObjects;


    public LineRenderer botPath;

	// Use this for initialization
	void Start () {

        ObjectData.objectName = _worldObject.objectName_.Bot;
		this.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        botPath = GetComponent<LineRenderer>();
        dragable = true;
        selectable = true;
        npc = true;
        botPath.loop = true;
        ID = Mathf.RoundToInt(Random.Range(0,1000) / Random.value);
        waypointObjects.Add(this.gameObject);
       
	}
	public void UpdatePath()
    {
        botPath.positionCount = waypointObjects.Count;
        for(int a = 0; a < waypointObjects.Count;  a++)
        {
            
            botPath.SetPosition(a, waypointObjects[a].transform.position);
        }
    }


	// Update is called once per frame
	void Update () {
		startLocation = this.transform.position;
		ObjectData.transform = this.transform;
        UpdatePath();



    }

    public void AddPoint(int waypointID, Vector3 location)
    {
        waypointsLocations[waypointID] = location;
    }
}
