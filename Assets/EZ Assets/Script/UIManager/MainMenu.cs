using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject GameGUI;
    public GameObject MenuGUI;

    public void Play()
    {   
        GameGUI.SetActive(true);
        MenuGUI.SetActive(false);
        LevelManager.Instance.LoadLevel(1);
    }
}
