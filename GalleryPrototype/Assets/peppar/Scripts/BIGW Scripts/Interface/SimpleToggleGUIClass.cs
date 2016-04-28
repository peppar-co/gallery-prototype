using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleToggleGUIClass : MonoBehaviour
{
    //[SerializeField]
    //private Text _referenceLabel;
    //[SerializeField]
    //private Toggle _referenceToggle;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Transform _toggleObject;

    //private string _toggleText;
    private Sprite _toggleIcon;
    private bool _toggleActive;

    public string ToggleText
    {
        get; set;
        //get
        //{
        //    return _toggleText;
        //}

        //set
        //{
        //    _toggleText = value;
        //    _referenceLabel.text = value;
        //}
    }

    public Sprite ToggleIcon
    {
        get
        {
            return _toggleIcon;
        }
        set
        {
            _toggleIcon = value;
            _icon.sprite = value;
        }
    }

    public bool ToggleActive
    {
        get
        {
            return _toggleActive;
        }

        set
        {
            _toggleActive = value;
            //_referenceToggle.isOn = value;
            _toggleObject.gameObject.SetActive(value);
        }
    }
}