using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
public class LoadEditorLevel : MonoBehaviour {

	public List <LevelObject> LevelObjects;
    string playerprefsID = "LevelName";
    string path = "Assets/CustomLevels";
    string Levelname;

  [System.Serializable]
	public struct LevelObject{
		public string ObjectName;
		public string objectPosition;
		public Vector3 positionVector;
		public string ObjectScale;
		public Vector3 ScaleVector;
		public string ObjectRotation;
		public Quaternion objectQuaternion;
        public bool bot;
        public int NumberOfPatrolPoints;
        public int BotID;
        public List<Vector3> BotPatrolRoute;        
        public List<string> botpatrollocationstring;
	}

    LevelObject Lobject;
	StreamReader Reader;
    public List<string> test;
	public List <string> linesFromFile;

	public GameObject FloorObject;
	public GameObject BasicBlock;
	public GameObject PlayerStart;
    public GameObject playerEnd;
    public GameObject PlayerObject;
    public GameObject BotObject;
    public GameObject CoinObject;
    public GameObject CheckPointObject;
    public GameObject PlayerControls;
    public GameObject LevelEndObj;
    public string levelfile;
    public string npcfile;
    public struct LevelDetails
    {
        public string MainLevelText;
        public string NpcText;
    }
 
    public void Start()
    {
       

        npcfile = path + "/" + PlayerPrefs.GetString(playerprefsID) + "/" + "npc" + ".Txt";
        levelfile = path + "/" + PlayerPrefs.GetString(playerprefsID) + "/" + PlayerPrefs.GetString(playerprefsID) + ".Txt";
        Process();
    }
    void Process()
    {
        Reader = new StreamReader(levelfile);
        string[] readText = File.ReadAllLines(levelfile);


        foreach (string s in readText)
        {
            linesFromFile.Add(s);
        }
        for (int a = 0; a < linesFromFile.Count; a++)
        {
            if (linesFromFile[a].Contains("#") == true)
            {

                Lobject.ObjectName = linesFromFile[a + 1];

                string temp;
                string[] temp1;

                temp = linesFromFile[a + 2].Trim('(', ')');
                temp1 = temp.Split(',');
                Lobject.positionVector.x = float.Parse(temp1[0]);
                Lobject.positionVector.y = float.Parse(temp1[1]);
                Lobject.positionVector.z = float.Parse(temp1[2]);
                Lobject.objectPosition = temp;


                temp = linesFromFile[a + 3].Trim('(', ')');
                temp1 = temp.Split(',');
                Lobject.ScaleVector.x = float.Parse(temp1[0]);
                Lobject.ScaleVector.y = float.Parse(temp1[1]);
                Lobject.ScaleVector.z = float.Parse(temp1[2]);
                Lobject.ObjectScale = temp;


                temp = linesFromFile[a + 4].Trim('(', ')');
                temp1 = temp.Split(',');
                Lobject.objectQuaternion.x = float.Parse(temp1[0]);
                Lobject.objectQuaternion.y = float.Parse(temp1[1]);
                Lobject.objectQuaternion.z = float.Parse(temp1[2]);
                Lobject.objectQuaternion.w = float.Parse(temp1[3]);
                Lobject.ObjectRotation = temp;

                if(Lobject.ObjectName == "Bot")
                {
                    Lobject.botpatrollocationstring = new List<string>();
                    Lobject.BotPatrolRoute = new List<Vector3>();
                    Lobject.bot = true;
                    Lobject.BotID = int.Parse(linesFromFile[a + 5]);
                    Lobject.NumberOfPatrolPoints = int.Parse(linesFromFile[a + 6]);
                }
                
              
                LevelObjects.Add(Lobject);
            }
        }

        LoadLevel();
    }
    void AttachBotPatrolRoute(LevelObject botObject)
    {
       
      
      
        Reader = new StreamReader(npcfile);
        string[] readText = File.ReadAllLines(npcfile);


        foreach (string s in readText)
        {
            if (s.Contains(botObject.BotID.ToString())){ 
                for(int a = 2; a < botObject.NumberOfPatrolPoints+2; a++)
                {
                    botObject.botpatrollocationstring.Add(readText[a]);
                }
            }

        }
        Reader.Close();

        for(int a = 0; a <botObject.botpatrollocationstring.Count; a++)
        {
            botObject.BotPatrolRoute.Add(ProcessStringToVector3(botObject.botpatrollocationstring[a]));
        }


    }

    Vector3 ProcessStringToVector3(string inputline)
    {
        Vector3 x = new Vector3();
        string temp;
        string[] tempSplit;
        temp = inputline.Trim('(', ')');
        tempSplit = temp.Split(',');
        x.x = float.Parse(tempSplit[0]);
        x.y = float.Parse(tempSplit[1]);
        x.z = float.Parse(tempSplit[2]);
       
       

        return x;
    }

    void SetUpPlayer(GameObject player)
    {
        GameObject ControlsObject;
       
        // points the camera at the player
        Camera.main.gameObject.GetComponent<followcam>().Target = player.transform;
        Camera.main.gameObject.GetComponent<followcam>().enabled = true;
       
       //SpawnAndroidControls
       ControlsObject = Instantiate(PlayerControls, this.transform.position, Quaternion.identity);
        ControlsObject.transform.parent = GameObject.Find("Canvas").transform;
        ControlsObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        Camera.main.GetComponent<BackButton>().UserControls = ControlsObject;

       

    }
    void SetupLevelEnd(GameObject End)
    {
    
    GameObject LevelEndObject;

        LevelEndObject = Instantiate(LevelEndObj, this.transform.position, Quaternion.identity);
        End.GetComponent<EndLevel>().LevelEndUi = LevelEndObject;
        LevelEndObject.transform.parent = GameObject.Find("Canvas").transform;
        Vector3 screencenter;
        screencenter = new Vector3(Screen.height / 2, Screen.width / 2, 0);
        LevelEndObject.GetComponent<RectTransform>().localPosition = new Vector3(00, 00, 0);

        LevelEndObject.GetComponentInChildren<Button>().onClick.AddListener(() => EndLevel());
    }

    void EndLevel()
    {
        SceneManager.LoadScene("LevelEditorSelectLevel");
    }
    public void LoadLevel(){
		
		

		for (int a = 0; a < LevelObjects.Count; a++){
            switch (LevelObjects[a].ObjectName)
            {
                case "Block":
                    GameObject objecta;
                    objecta = Instantiate(BasicBlock, LevelObjects[a].positionVector, Quaternion.identity);
                    objecta.transform.localScale = LevelObjects[a].ScaleVector;
                    break;
                case "Floor":
                    GameObject b;
                    b = UnityEngine.GameObject.Instantiate(FloorObject, LevelObjects[a].positionVector, Quaternion.identity);
                    b.transform.localScale = LevelObjects[a].ScaleVector;
                    break;
                case "PlayerStart":
                    GameObject c;
                    Debug.Log("spawning player");
                    c = UnityEngine.GameObject.Instantiate(PlayerObject, LevelObjects[a].positionVector, Quaternion.identity);
                    c.GetComponent<PlayerInventory>().enabled = false;
                    c.transform.localScale = LevelObjects[a].ScaleVector;
                    SetUpPlayer(c);
                    break;
                case "PlayerEnd":
                    GameObject i;
                   i = UnityEngine.GameObject.Instantiate(playerEnd, LevelObjects[a].positionVector, Quaternion.identity);
                    i.transform.localScale = LevelObjects[a].ScaleVector;

                    SetupLevelEnd(i);
                    break;
        
                case "Bot":
                    GameObject f;
                    f = UnityEngine.GameObject.Instantiate(BotObject, LevelObjects[a].positionVector, Quaternion.identity);
                    f.transform.localScale = LevelObjects[a].ScaleVector;


                    AttachBotPatrolRoute(LevelObjects[a]);
                   f.GetComponent<botCustomLevel>().PatrolRoute = new Transform[LevelObjects[a].BotPatrolRoute.Count];
                    for (int p = 0; p < LevelObjects[a].BotPatrolRoute.Count; p++)
                    {

                        Transform tmp = new GameObject().transform;

                        tmp.position = LevelObjects[a].BotPatrolRoute[p];
                        f.GetComponent<botCustomLevel>().PatrolRoute[p] = tmp;
                    }
                    break;
                case "Coin":
                    GameObject g;
                   g = UnityEngine.GameObject.Instantiate(CoinObject, LevelObjects[a].positionVector, Quaternion.identity);
                    g.transform.localScale = LevelObjects[a].ScaleVector;
                    break;
                case "CheckPoint":
                    GameObject h;
                    h = UnityEngine.GameObject.Instantiate(CoinObject, LevelObjects[a].positionVector, Quaternion.identity);
                    h.transform.localScale = LevelObjects[a].ScaleVector;
                    break;

            }				
			}
		}







}