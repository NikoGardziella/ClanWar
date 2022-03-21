using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> menus = new List <GameObject>();

	[SerializeField]
	private InputField loginUsername;
	[SerializeField]
	private InputField password;

	[SerializeField]
	private InputField registerUsername;
	[SerializeField]
	private InputField registerEmail;
	[SerializeField]
	private InputField registerPassword;
	[SerializeField]
	private InputField registerConfirmPassword;


	public void Login()
	{

	}

	public void Register()
	{

	}

	public void ChangeMenu(int i)
	{

	}
}
