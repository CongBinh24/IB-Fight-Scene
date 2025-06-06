using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject winDialog;
    public GameObject loseDialog;

    private MainMenu menu;

    private void Start()
    {
        menu = FindObjectOfType<MainMenu>();
    }
    public void ShowWin()
    {
        winDialog.SetActive(true);

    }

    public void CloseWin()
    {
        winDialog.SetActive(false);
    }

    public void ShowLose()
    {
        loseDialog.SetActive(true);

    }

    public void CloseLose()
    {
        loseDialog.SetActive(false);
    }

    public void OnclickNextLv()
    {
        FindObjectOfType<PlayerController>().ResetPlayer();
        LevelManager.Instance.LoadLevel(LevelManager.Instance.GetCurrentLevel() + 1);
        CloseWin();
    }

    public void ReplayWin()
    {
        menu.GameGUI.SetActive(false);
        menu.MenuGUI.SetActive(true);
        CloseWin();
    } 
    public void ReplayLose()
    {
        menu.GameGUI.SetActive(false);
        menu.MenuGUI.SetActive(true);
        CloseLose();
    }
}
