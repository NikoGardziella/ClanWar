using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureZone : MonoBehaviour
{
    private float StartingTime;
    public float realStartingtime;
    public static float totalTime;
    public static bool stopTextTimer { get; set; }
    public bool timerOn = false;

    public Text text;

    private float seconds;

    private HashSet<GameObject> _BlueInZone = new HashSet<GameObject>();
    private HashSet<GameObject> _RedInZone = new HashSet<GameObject>();

    public bool LeftZone;
    public bool RightZone;
    public bool leftzoneBool;
    private string team;

    [SerializeField]
    public GameManager gameManager;


    public GameManager GameManager
    {
        get { return gameManager; }
        set { gameManager = value; }
    }

    void Start()
	{
        totalTime = realStartingtime;
	}

	private void Update()
	{
		if (!stopTextTimer)
		{
            totalTime -= Time.deltaTime;

            seconds = (int)(totalTime % 60);
            text.text = seconds.ToString();
		}



        if(seconds <= 0 && timerOn)
		{
            CheckTags();
            Debug.Log("team is;" + team);
            if (team == "blue")
			{
                /*  Debug.Log("Player entered capture zone");
                  if (leftzoneBool)
                      LeftZone = false;
                  else
                      RightZone = false; */
                GameManager.updateZones(team, leftzoneBool);
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                gameObject.tag = "Player";
                gameObject.transform.GetChild(0).tag = "Player";
            }
            if (team == "red")
            {
                /*  gameObject.GetComponent<Renderer>().material.color = Color.red;
                  if (leftzoneBool)
                      LeftZone = true;
                  else
                     RightZone = false; */
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                GameManager.updateZones(team, leftzoneBool);
                gameObject.transform.GetChild(0).tag = "Enemy";
                gameObject.tag = "Enemy";
                Debug.Log("Enemy entered capture zone");
            }

        }
	}

    void ResetTextTimer()
	{
        totalTime = 11;
        seconds = (int)(totalTime % 60);
        text.text = seconds.ToString();
	}        

	public void OnTriggerEnter(Collider other)
    {



        //CheckTags();
        if (other.transform.parent.parent.parent.CompareTag("Player"))
        {
           _BlueInZone.Add(other.gameObject);
            timerOn = true;
            stopTextTimer = false;
            ResetTextTimer();

            Debug.Log("Player hit capture zone");
        }
        else if (other.transform.parent.parent.parent.CompareTag("Enemy"))
        {
            _RedInZone.Add(other.gameObject);
            timerOn = true;
            stopTextTimer = false;
            ResetTextTimer();

            Debug.Log("Target hit capture zone");
        }
    }

	private void OnTriggerExit(Collider other)
	{

        if (other.transform.parent.parent.parent.CompareTag("Player"))
        {
            _BlueInZone.Remove(other.gameObject);
            timerOn = false; ;
            stopTextTimer = false;
        }

        if (other.transform.parent.parent.parent.CompareTag("Enemy"))
        {
            _RedInZone.Remove(other.gameObject);
            timerOn = false;
            stopTextTimer = false;
        }
    }

	void CheckTags()
	{
        if (_RedInZone.Count > 0 && _BlueInZone.Count > 0)
		{
            ResetTextTimer();
		}
        else if (_RedInZone.Count != 0)
		{
            Debug.Log("team set red");
            team = "red";

		}
        if (_BlueInZone.Count != 0)
		{
            Debug.Log("team set blue");
            team = "blue";
		}
        Debug.Log("reds:" + _RedInZone.Count + "blues:" + _BlueInZone);

    }
}
