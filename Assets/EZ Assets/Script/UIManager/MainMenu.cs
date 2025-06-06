using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class MainMenu : MonoBehaviour
{
    public GameObject GameGUI;
    public GameObject MenuGUI;


    void Start()
    {
        
    }

    public void Play()
    {   
        GameGUI.SetActive(true);
        MenuGUI.SetActive(false);
        LevelManager.Instance.LoadLevel(1);
    }
}
