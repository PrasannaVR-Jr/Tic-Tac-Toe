using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonState
{
    None,
    X,
    O
}
public class StateHolder : MonoBehaviour
{
    Button currButton;
    TMP_Text currButtonText;
    public ButtonState currButtonState;
    public UIManager UIManagerReference;
    // Start is called before the first frame update
    void Start()
    {
        currButton = GetComponent<Button>();
        ButtonInit();
        currButton.onClick.AddListener(OnButtonClick);
    }

    private void ButtonInit()
    {
        currButtonText = currButton.GetComponentInChildren<TMP_Text>();
        currButtonText.text = "";
        currButtonState = ButtonState.None;
    }

    public void OnButtonClick()
    {
        currButton.interactable = false;
        switch (GameManager.Instance.currentTurn)
        {
            case "X":
                currButtonState = ButtonState.X;
                break;
            case "O":
                currButtonState = ButtonState.O;
                break;
            default:
                currButtonState = ButtonState.None;
                break;
        }
        currButtonText.text = GameManager.Instance.currentTurn;
        if (GameManager.Instance.CheckWinCondition())
            UIManagerReference.CurrentTurnText.text += " Won";
        else if (GameManager.Instance.CheckDrawCondition())
            UIManagerReference.CurrentTurnText.text = "Draw";
        else
        {
            GameManager.Instance.TurnOver();
            UIManagerReference.CurrentTurnText.text = GameManager.Instance.currentTurn;
        }
    }
    public void ChangeColor()
    {
        currButton.gameObject.GetComponent<Image>().color = Color.red;
    }
    
}
