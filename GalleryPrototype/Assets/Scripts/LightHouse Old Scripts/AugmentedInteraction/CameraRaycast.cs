using UnityEngine;
using System.Collections;

public class CameraRaycast : MonoBehaviour
{
    Ray _ray;
    RaycastHit hit;
    float distanceToGround = 0;

    [SerializeField]
    public PlayAnimation _playAnimation;

    [SerializeField]
    public GameObject hitGO;

    [SerializeField]
    public WorldObjectManager WOM;

    [SerializeField]
    float _startTime;

    float _time;

    [SerializeField]
    GameObject _crosshair;

    private bool _justSelected = false;

    // Use this for initialization
    void Awake()
    {
        if (_startTime == 0)
        {
            _startTime = 3;
        }
        _time = _startTime;

        if (_crosshair != null)
            _crosshair.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _ray.origin = transform.position;
        _ray.direction = transform.forward;



        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
        //    Debug.Log(" mouse Hit:" + hit.transform.gameObject);
        //}

        ScaleCursor();
    }

    void ScaleCursor()
    {
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (_time <= 0)
            {
                _crosshair.transform.localScale = Vector3.one;

                _crosshair.gameObject.SetActive(false);
                //RotateObject();
                if (!_justSelected)
                {
                    Debug.Log("play anim");
                    StartAnimation();
                }
                _justSelected = true;

            }
            else
            {
                _crosshair.gameObject.SetActive(true);

                _crosshair.GetComponentInChildren<SpriteRenderer>().color = Color.white;

                _time -= Time.deltaTime;

                var scale = _crosshair.transform.localScale;

                float scaleValue = _time / _startTime;

                scale.x = scale.y = scale.z = scaleValue;

                scale = new Vector3(scale.x, scale.y, scale.z);

                _crosshair.transform.localScale = scale;
            }
        }
        else
        {
            if (_justSelected && _time >= -_startTime)
            {
                _crosshair.gameObject.SetActive(true);
                _crosshair.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                _time -= Time.deltaTime;

                var scale = _crosshair.transform.localScale;

                float scaleValue = _time / _startTime;

                scale.x = scale.y = scale.z = scaleValue;

                scale = new Vector3(scale.x, scale.y, scale.z);

                _crosshair.transform.localScale = scale;
            }
            else if (_justSelected && _time < -_startTime)
            {
                _justSelected = false;
            }
            else
            {
                if (_crosshair.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                    _crosshair.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                _crosshair.gameObject.SetActive(false);
                _time = _startTime;
                _crosshair.transform.localScale = Vector3.one;
                WOM.GotHit = false;
            }
        }
    }

    void RotateObject()
    {
        //if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        //{
        Debug.Log("rotating");
        hitGO = hit.transform.gameObject;
        WOM = hitGO.transform.parent.parent.GetComponent<WorldObjectManager>();

        Debug.Log("eye hit:" + hit.transform.gameObject);
        WOM.GotHit = true;
    }

    public void StartAnimation()
    {
        _playAnimation.SetAnimationState(2);
    }
}
