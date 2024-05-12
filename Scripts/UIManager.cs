using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Transform Grid;
    public bool isSymbolEqual;
    StateHolder[,] Symbols = new StateHolder[3, 3];
    public TMP_Text CurrentTurnText;
    // Start is called before the first frame update
    void Start()
    {
        int i=1, j=1;
        foreach(Transform Button in Grid)
        {
            Symbols[i - 1, j - 1] = Button.gameObject.AddComponent<StateHolder>();
            Symbols[i - 1, j - 1].UIManagerReference = this;
            if(j%3==0)
            {
                i++;
                j = 0;
            }
            j++;
        }
        GameManager.Instance.Symbols = Symbols;
        ActivateButtons(false);
    }
   
    public void ActivateButtons(bool isActive)
    {
        for(int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                Symbols[i, j].gameObject.GetComponent<Button>().interactable = isActive;
            }
        }
    }

    

    public void StartGame()
    {
        GameManager.Instance.hasGameStarted = true;
        ActivateButtons(true);
        CurrentTurnText.text = GameManager.Instance.currentTurn;
    }
}
