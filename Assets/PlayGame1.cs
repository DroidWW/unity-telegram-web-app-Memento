using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayGame1 : MonoBehaviour
{
    public GameObject[] playGameButtons;
    void Start()
    {
        SetOnButtonClick();
    }

    void SetOnButtonClick()
    {
        foreach(var button in playGameButtons)
            button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick());
    }
    void OnButtonClick()
    {

        Button clickedButton = EventSystem.current.currentSelectedGameObject?.GetComponent<Button>();
        if (clickedButton == null)
        {
            Debug.LogWarning("Нажатый объект не является кнопкой!");
            return;
        }
        int buttonIndex = System.Array.IndexOf(playGameButtons, clickedButton.gameObject);
        
        LoadingGame(buttonIndex);
        //GameObject.Find("Game1").SetActive(true);
    }

    void LoadingGame(int indexGame)
    {
        GameObject.Find("MainPage");
        //SceneManager.LoadScene("SoloGamePages");
        //GameObject.Find("SoloPlayPage").SetActive(false);
        //GameObject.Find("MainPage").SetActive(true);
        //GameObject.Find("Game1").SetActive(false);
        Debug.LogWarning("ALL GOOD");
    }

}
