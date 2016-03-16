using UnityEngine;
using System.Collections;


namespace peppar
{
	public class EyeGUIController : MonoBehaviour
	{
		[SerializeField]
		private EyeInteractionController _eyeInteractionController;

		[SerializeField]
		private GameObject _buttonHolder;

		[SerializeField]
		private int _maxExpressions;
		private void Awake()
		{
			UnityEngine.Assertions.Assert.IsNotNull(_eyeInteractionController);

			_buttonHolder.SetActive(false);
		}
		public void OpenMenu()
		{
			_buttonHolder.SetActive(true);
		}
		public void CloseMenu()
		{
			_buttonHolder.SetActive(false);
		}

		//public void ChangeExpresion()
		//{
		//	int exp = _eyeInteractionController.EyeAnimator.GetInteger("Expression");
		//	if (exp < _maxExpressions)
		//	{
		//		exp++;
		//		_eyeInteractionController.EyeAnimator.SetInteger("Expression", exp);
		//	}
		//	else
		//	{
		//		exp = 0;
		//		_eyeInteractionController.EyeAnimator.SetInteger("Expression", exp);
		//	}
		//}

		public void ResetExpression()
		{
			_eyeInteractionController.EyeAnimator.SetInteger("Expression", -1);
		}

		public void ResetScene()
		{
			_eyeInteractionController.ResetScene();
		}

		public void NextExpression()
		{
			int exp = _eyeInteractionController.EyeAnimator.GetInteger("Expression");
			if (exp < _maxExpressions)
			{
				exp++;
				_eyeInteractionController.EyeAnimator.SetInteger("Expression", exp);
			}
			else
			{
				exp = _maxExpressions;
			}
		}
		public void PrevExpression()
		{
			int exp = _eyeInteractionController.EyeAnimator.GetInteger("Expression");
			if (exp > 0)
			{
				exp--;
				_eyeInteractionController.EyeAnimator.SetInteger("Expression", exp);
			}
			else
			{
				exp = 0;
			}
		}
		public void NextScene()
		{
			_eyeInteractionController.ChangeScene(true);
		}
		public void PrevScene()
		{
			_eyeInteractionController.ChangeScene(false);
		}


	}
}