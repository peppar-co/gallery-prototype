using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIQuestChoiceButton : MonoBehaviour
{
    private string _buttonLabelText;
    [SerializeField]
    private Text _buttonLabel;

    public string ButtonLabelText
    {
        get
        {
            return _buttonLabelText;
        }

        set
        {
            _buttonLabelText = value;
            _buttonLabel.text = value;
        }
    }
}
