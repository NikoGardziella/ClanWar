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
            if (gameObject.CompareTag("Player"))
			{
                Debug.Log("Player entered capture zone");
                //set spawnzone inactive;
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            if (gameObject.CompareTag("Enemy"))
            {
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                //set spawn zone active
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
     /*   var allUnits = Physics.OverlapSphere(transform.position, 4);
        Debug.Log("number of units" + allUnits.Length);
		for (int i = 0; i < allUnits.Length; i++)
		{
            if(!allUnits[i].CompareTag(allUnits[i + 1].tag))
			{
                Debug.Log("Different tags in area");
                ResetTextTimer();
    		}
		} */

        Debug.Log(other + "Entered capture zone");

        CheckTags();
        if (other.transform.parent.parent.parent.CompareTag("Player"))
        {
           _BlueInZone.Add(other.gameObject);
            timerOn = true;
            stopTextTimer = false;
            ResetTextTimer();


            gameObject.tag = "Player";
            Debug.Log("Player hit capture zone");
        }
        if (other.transform.parent.parent.parent.CompareTag("Enemy"))
        {
            _RedInZone.Add(other.gameObject);
            timerOn = true;
            stopTextTimer = false;
            ResetTextTimer();
            gameObject.tag = "Enemy";
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
            ResetTextTimer();
        else
            return ;
       
	}
}
