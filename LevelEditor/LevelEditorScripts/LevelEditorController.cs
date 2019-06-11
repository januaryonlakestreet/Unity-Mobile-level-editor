using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class LevelEditorController : MonoBehaviour {
	public GameObject BasicBlock;
	public GameObject BasicFloor;


    string path;
	string npc;
	string CustomLevelDirectory = "Assets/CustomLevels/";

	string LevelName = "DefaultName";
    public GameObject JumpToLevel;

    private List<GameObject> botList;
    private List<WorldObject> LevelObjects;
   
    public struct LevelDataFileEntry{

		public string ObjectName;
		public string objectPosition;
		public string ObjectScale;
		public string ObjectRotation;

	}
    private void Start()
    {
       
    }
    LevelDataFileEntry item;
	public List <LevelDataFileEntry> fileEntries;

    private WorldObject CurrentObjectForAssessment;
	public void SaveLevel ()
	{
        LevelName = GameObject.Find("LevelName").GetComponent<InputField>().text;

        path = CustomLevelDirectory + LevelName + "/" + LevelName + ".txt";

        Debug.Log (path);
		CustomLevelDirectory = CustomLevelDirectory + LevelName;
		Directory.CreateDirectory (CustomLevelDirectory);
		StreamWriter writer = new StreamWriter (path, false);


		LevelObjects.Clear ();


		for (int a = 0; a < Object.FindObjectsOfType<WorldObject> ().Length; a++) {
			LevelObjects.Add (Object.FindObjectsOfType<WorldObject> () [a]);
		}
        for (int a = 0; a < LevelObjects.Count; a++)
        {
            if(LevelObjects[a].npc == true)
            {
                botList.Add(LevelObjects[a].gameObject);
            }
        }

		if (LevelName != "") {
            PlayerPrefs.SetString("LevelName", LevelName);
			for (int a = 0; a < LevelObjects.Count; a++) {
                CurrentObjectForAssessment = LevelObjects[a];
				writer.Write ("####################");
				writer.Write (writer.NewLine);
				writer.Write (LevelObjects [a].ObjectData.objectName);
				writer.Write (writer.NewLine);
				writer.Write (LevelObjects [a].ObjectData.transform.position.ToString ());
				writer.Write (writer.NewLine);
				writer.Write (LevelObjects [a].ObjectData.transform.localScale.ToString ());
				writer.Write (writer.NewLine);
				writer.Write (LevelObjects [a].ObjectData.transform.rotation.ToString ());
				writer.Write (writer.NewLine);
                if(LevelObjects[a].npc == true)
                {
                    int id;
                    int numberofwaypoints = CurrentObjectForAssessment.GetComponent<Bot_editor>().waypointObjects.Count;
                    id = LevelObjects[a].GetComponent<Bot_editor>().ID;
                    writer.Write(LevelObjects[a].GetComponent<Bot_editor>().ID.ToString());
                    writer.Write(writer.NewLine);
                    writer.Write(numberofwaypoints.ToString());
                    writer.Write(writer.NewLine);
                   
                    RecordBot(LevelObjects[a], id);
                }

			}
          
			GameObject.Find ("LevelName").GetComponent<InputField> ().text = "";
			writer.Close ();
		}
        JumpToLevel.SetActive(true);
        LevelName = "";
    }
    public void JumpToLevelButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CustomLevelLoad");
    }
    void RecordBot(WorldObject obj,int id)
    {
        //int id;
        
        npc = CustomLevelDirectory  + "/" +  "npc" + ".txt";
        Debug.Log(npc);
        StreamWriter Botwriter = new StreamWriter(npc, false);
        for (int b = 0; b < botList.Count; b++)
        {
            Botwriter.Write("####################");
            Botwriter.Write(Botwriter.NewLine);
           // id = System.Array.IndexOf(botList.ToArray(), botList[b].gameObject);
            Botwriter.Write(botList[b].GetComponent<Bot_editor>().ID);
            Botwriter.Write(Botwriter.NewLine);

            int totalWaypoints = CurrentObjectForAssessment.GetComponent<Bot_editor>().waypointObjects.Count;

            for(int a = 0; a < totalWaypoints; a++)
            {
                Botwriter.Write(botList[b].GetComponent<Bot_editor>().waypointObjects[a].transform.position);
                Botwriter.Write(Botwriter.NewLine);
            }
            
          

        }
        Botwriter.Close();
    }

}


/*
 * 
*/