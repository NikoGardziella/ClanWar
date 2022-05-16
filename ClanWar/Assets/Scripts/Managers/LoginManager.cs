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
	private InputField loginPassword;

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
		AccountInfo.Login(loginUsername.text, loginPassword.text);
	}

	public void QuickLogin()
	{
		AccountInfo.Login("Helsinki", "Helsinki");
	}
	public void QuickLogin1()
	{
		AccountInfo.Login("Stockholm", "Stockholm");
	}


	public void Register()
	{
		if (registerConfirmPassword.text == registerConfirmPassword.text)
			AccountInfo.Register(registerUsername.text, registerEmail.text, registerPassword.text);
		else
			Debug.LogError("password do not match");
	}

	public void ChangeMenu(int i)
	{
		GameFunctions.ChangeMenu(menus.ToArray(), i);
	}
}
