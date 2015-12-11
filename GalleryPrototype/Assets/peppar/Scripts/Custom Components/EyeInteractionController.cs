using UnityEngine;
using System.Collections;

namespace peppar
{
    public class EyeInteractionController : MonoBehaviour
    {
        [SerializeField]
        private EyeGUIController _eyeGUIController;

        [SerializeField]
        private Animator _eyeAnimator;

        public Animator EyeAnimator
        {
            get { return _eyeAnimator; }
            set { _eyeAnimator = value; }
        }

        [SerializeField]
        private float _maxIdleTime;

        private float _blinkTime;

        private void Blink()
        {
            Debug.Log("DO BLINK!!!!!!!!");
            _eyeAnimator.SetTrigger("Blink");
        }

        private void Awake()
        {
            UnityEngine.Assertions.Assert.IsNotNull(_eyeGUIController);
            UnityEngine.Assertions.Assert.IsNotNull(_eyeAnimator);
            _maxIdleTime = 3;
            _blinkTime = _maxIdleTime;
        }

        private void Update()
        {
            //toggle gui on hit

            //get raycast from touch (android)       
            for (var i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                    //utlDebugPrint.Inst.print(Input.GetTouch(i).position.ToString());

                    if (Physics.Raycast(ray))
                    {
                        _eyeGUIController.OpenMenu();
                    }
                }
            }

            //mouse interaction for PC debugging
            if (Input.GetMouseButtonDown(0))
            {
                Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                Debug.Log(Input.mousePosition);

                if (Physics.Raycast(mRay))
                {
                    _eyeGUIController.OpenMenu();
                }
            }

            _maxIdleTime -= Time.deltaTime;
            if (_maxIdleTime < 0)
            {
                _maxIdleTime = Random.RandomRange(.5f, _blinkTime);
                Blink();
            }


        }
    }
}