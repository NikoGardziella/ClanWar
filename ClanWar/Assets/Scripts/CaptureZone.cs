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

    public Image LeftZone;
    public Image RightZone;
    public bool leftzoneBool;
    private string team;

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
            if (team == "blue")
			{
                Debug.Log("Player entered capture zone");
                if (leftzoneBool)
                    LeftZone.enabled = false;
                else
                    RightZone.enabled = false;
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                gameObject.tag = "Player";
                gameObject.transform.GetChild(0).tag = "Player";
            }
            if (team == "red")
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                if (leftzoneBool)
                    LeftZone.enabled = true;
                else
                    RightZone.enabled = false;
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
        else if (_RedInZone.Count > 0)
            team = "red";
        else if (_BlueInZone.Count > 0)
            team = "blue";

    }
}
