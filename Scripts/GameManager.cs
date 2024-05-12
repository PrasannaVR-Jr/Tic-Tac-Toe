using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string Turn;
    string[] Turns = { "X", "O" };
    public bool hasGameStarted;
    public StateHolder[,] Symbols;
    public string currentTurn
    {
        get => Turn;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        hasGameStarted = false;
        Turn = Turns[Random.Range(0, 2)];
        Debug.Log(currentTurn);
    }

    public void TurnOver()
    {
        switch(currentTurn)
        {
            case "X":Turn = "O";
                break;
            case "O":Turn = "X";
                break;
        }
    }

    public bool CheckWinCondition()
    {
        bool isSymbolEqual = false;
        if (GameManager.Instance.hasGameStarted)
        {
            isSymbolEqual = CheckRowEqual() || CheckColEqual() || CheckDiagEqual();

            if (isSymbolEqual)
                FindObjectOfType<UIManager>().ActivateButtons(false);
        }
        return isSymbolEqual;
    }

    public bool CheckDrawCondition()
    {
        bool isFilled = true;
        for(int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                isFilled = isFilled && (Symbols[i,j].currButtonState==ButtonState.O|| Symbols[i, j].currButtonState == ButtonState.X);
                if (!isFilled)
                    break;
            }
        }
        return isFilled;
    }

    bool CheckRowEqual()
    {
        bool isRowEqual = false;
        for (int i = 0; i < 3; i++)
        {
            isRowEqual = isRowEqual || Symbols[i, 0].currButtonState == ButtonState.X && Symbols[i, 1].currButtonState == ButtonState.X && Symbols[i, 2].currButtonState == ButtonState.X
            || Symbols[i, 0].currButtonState == ButtonState.O && Symbols[i, 1].currButtonState == ButtonState.O && Symbols[i, 2].currButtonState == ButtonState.O;

            if (isRowEqual)
            {
                for (int j = 0; j < 3; j++)
                    Symbols[i, j].ChangeColor();
                return true;
            }
        }
        return false;
    }
    bool CheckColEqual()
    {
        bool isEqual = false;

        for (int i = 0; i < 3; i++)
        {
            isEqual = isEqual || Symbols[0, i].currButtonState == ButtonState.X && Symbols[1, i].currButtonState == ButtonState.X && Symbols[2, i].currButtonState == ButtonState.X
            || Symbols[0, i].currButtonState == ButtonState.O && Symbols[1, i].currButtonState == ButtonState.O && Symbols[2, i].currButtonState == ButtonState.O;

            if (isEqual)
            {
                for (int j = 0; j < 3; j++)
                    Symbols[j, i].ChangeColor();
                return true;
            }
        }

        return false;
    }

    bool CheckDiagEqual()
    {
        for (int i = 1; i <= 2; i++)
        {
            bool isDiagEqual = (int)Symbols[0, 0].currButtonState == i && (int)Symbols[1, 1].currButtonState == i && (int)Symbols[2, 2].currButtonState == i
                || (int)Symbols[0, 2].currButtonState == i && (int)Symbols[1, 1].currButtonState == i && (int)Symbols[2, 0].currButtonState == i;

            if (isDiagEqual)
            {
                if ((int)Symbols[0, 0].currButtonState == i && (int)Symbols[1, 1].currButtonState == i && (int)Symbols[2, 2].currButtonState == i)
                    for (int j = 0; j < 3; j++)
                        Symbols[j, j].ChangeColor();
                if((int)Symbols[0, 2].currButtonState == i && (int)Symbols[1, 1].currButtonState == i && (int)Symbols[2, 0].currButtonState == i)
                    for (int j = 0; j < 3; j++)
                        Symbols[j, 2-j].ChangeColor();
                return true;
            }
        }
        return false;
    }
}
