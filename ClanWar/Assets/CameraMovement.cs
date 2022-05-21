using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	private Vector3 origin;
	private Vector3 difference;
	private Vector3 resetCamera;

	public bool cameraDrag = false;
	public bool canDrag = true;

	[SerializeField]
	private PlayerStats playerStats;
	public PlayerStats PlayerStats
	{
		get { return playerStats; }
		set { playerStats = value; }
	}



	private void Start()
	{
		resetCamera = Camera.main.transform.position;
	}

	private void Update()
	{
		//canDrag = playerStats.OnDragging;
		if (Input.GetMouseButton(0) && canDrag == true)
		{
			difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);
			if (cameraDrag == false)
			{
				cameraDrag = true;
				origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		}
		else
		{
			cameraDrag = false;
		}

		if (cameraDrag)
		{
			Camera.main.transform.position = origin - difference;
		}

		if (Input.GetMouseButton(1))
		{
			Camera.main.transform.position = resetCamera;
		}
	}
}
