using UnityEngine;
using System.Collections;

public class BIGWGUIController : MonoBehaviour
{
	[SerializeField]
	private Animator GUIStateMachine;

	public void SetMenuIndex(int index)
	{
		GUIStateMachine.SetInteger("MenuIndex", index);
	}

	public void SetSubMenuIndex(int index)
	{
		GUIStateMachine.SetInteger("SubMenuIndex", index);
	}

	public void SetSubMenuState(bool isInSubMenu)
	{
		GUIStateMachine.SetBool("IsInSubMenu", isInSubMenu);
	}
	
	public void FinishCharacterCreation()
	{
		/// TODO instantiate character and kill the temporary one
	}



}
