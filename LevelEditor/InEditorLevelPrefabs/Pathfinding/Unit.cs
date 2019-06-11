using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public List<Vector3> path;
    public float speed;
    public int SelectionID;
    private Vector3 startMarker, endMarker;
    float startTime;
    float journeyLength;
    // Use this for initialization
    void Start () {
        speed = 10f;
        SelectionID = 0;
	}
    void SetPoints()
    {
        startMarker = path[SelectionID];
        endMarker = path[SelectionID + 1];
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }
 IEnumerator MoveTo()
    {
        if(path.Count != 0) { 
        Vector3 currentwaypoint = path[0];

        while(true)
        {
            if(transform.position == currentwaypoint)
            {
                SelectionID++;
                if(SelectionID >= path.Count)
                {
                    yield break;
                }
                currentwaypoint = path[SelectionID];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentwaypoint, speed);
                SelectionID = 0;
                yield return null;
        }

        }

    }
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                GameObject.Find("a*").GetComponent<PathFinding>().FindPath(this.transform.position, hit.point);

                path = GameObject.Find("a*").GetComponent<PathFinding>().ReturnPath();

                StopCoroutine("MoveTo");
                StartCoroutine("MoveTo");
            }
            
            
           
        }
	}
}
