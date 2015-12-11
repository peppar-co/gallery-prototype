using UnityEngine;
using System.Collections;
using UnityEngine.UI;


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

        public void ChangeExpresion()
        {
            int exp = _eyeInteractionController.EyeAnimator.GetInteger("Expression");
            if (exp < _maxExpressions)
            {
                exp++;
                _eyeInteractionController.EyeAnimator.SetInteger("Expression", exp);
            }
            else
            {
                exp = 0;
                _eyeInteractionController.EyeAnimator.SetInteger("Expression", exp);
            }

            Debug.Log("EXPRESSION CHANGED!!!!!!!!:::" + exp);

        }

        public void ChangeScene()
        {
            Debug.Log("SCENE CHANGED!!!!!!!!");
        }
    }
}