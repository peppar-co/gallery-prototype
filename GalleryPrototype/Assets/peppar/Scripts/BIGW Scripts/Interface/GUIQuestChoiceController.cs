using UnityEngine;
using System.Collections;

public class GUIQuestChoiceController : MonoBehaviour
{

    [SerializeField]
    private GUIQuestChoiceButton _choiceButtonA, _choiceButtonB;

    public void SetChoiceButtonLabels(string textA, string textB)
    {
        _choiceButtonA.ButtonLabelText = textA;
        _choiceButtonB.ButtonLabelText = textB;
    }
    


}
