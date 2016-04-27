using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleToggleGUIClass : MonoBehaviour
{
    [SerializeField]
    private Text _referenceLabel;
    [SerializeField]
    private Toggle _referenceToggle;
    // TODO add texture/ icon gui 

    private string _toggleText;
    private Texture _toggleIcon;
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

    public Texture ToggleIcon
    {
        get
        {
            return _toggleIcon;
        }
        set
        {
            _toggleIcon = value;
            // TODO change gui icon
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