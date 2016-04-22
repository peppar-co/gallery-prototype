using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleToggleGUIClass : MonoBehaviour
{
    [SerializeField]
    private Text _referenceLabel;
    [SerializeField]
    private Toggle _referenceToggle;

    private string _toggleText;
    private bool _toggleActive;

    public string ToggleText
    {
        get
        {
            return _toggleText;
        }

        set
        {
            _toggleText = value;
            _referenceLabel.text = value;
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
            _referenceToggle.isOn = value;
        }
    }
}