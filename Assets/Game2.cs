using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class Game2 : MonoBehaviour
{
    public GameObject[] gameObjects;
    public GameObject[] gameLoadingButtons;
    private int _number, _userMoveIndex = 0;
    private Color _greenColor, _redColor, _blueColor, _blueColor2;
    private bool _gameOn = true, _usersTurn = false;
    private const int NumberTimer = 4;

    void Start()
    {
        Initialization();
        StartCoroutine(GameBegins());
    }

    void Initialization()
    {
        SetGlobalColors();
        SetOnButtonClick();
    }

    void SetGlobalColors()
    {
        UnityEngine.ColorUtility.TryParseHtmlString("#C5DD91", out _greenColor);
        UnityEngine.ColorUtility.TryParseHtmlString("#FF8A84", out _redColor);
        UnityEngine.ColorUtility.TryParseHtmlString("#2B3656", out _blueColor);
        UnityEngine.ColorUtility.TryParseHtmlString("#323F65", out _blueColor2);
    }

    void SetOnButtonClick()
    {
        gameObjects[1].GetComponent<Button>().onClick.AddListener(() => OnButtonClick());
    }

    void OnButtonClick()
    {
        if (!_usersTurn)
            return;

        Button clickedButton = EventSystem.current.currentSelectedGameObject?.GetComponent<Button>();

        int buttonIndex = System.Array.IndexOf(gameObjects, clickedButton.gameObject);

        if (gameObjects[2].GetComponent<TMP_InputField>().text == _number.ToString())
        {
            foreach (var loadingButtons in gameLoadingButtons)
            {
                loadingButtons.GetComponent<Image>().color = _greenColor;
            }
            _usersTurn = false;
        }
        else
        {
            foreach(var loadingButtons in gameLoadingButtons)
            {
                loadingButtons.GetComponent<Image>().color = _redColor;
            }
            _gameOn = false;
            Debug.LogWarning("Игра окончена");
        }
    }
    IEnumerator LineTimer(int seconds)
    {
        gameObjects[1].gameObject.SetActive(false);
        gameObjects[2].GetComponent<TMP_InputField>().interactable = false;
        gameObjects[0].GetComponent<TextMeshProUGUI>().text = "LEVEL " + (++_userMoveIndex).ToString();
        RandomNumber(_userMoveIndex);
        int counter = seconds;
        int index = 0;
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < gameLoadingButtons.Length; i++)
        {
            gameLoadingButtons[i].GetComponent<Image>().color = _blueColor2;
        }
        while (counter > 0)
        {
            for (int i = index; i < index+seconds*2; i++)
            {
                gameLoadingButtons[i].GetComponent<Image>().color = Color.white;
                yield return new WaitForSeconds(0.125f);
            }
            counter--;
            index += 8;
        }
        gameObjects[2].GetComponent<TMP_InputField>().text = "";
        for (int i = 0; i < gameLoadingButtons.Length; i++)
        {
            gameLoadingButtons[i].GetComponent<Image>().color = _blueColor;
        }
        gameObjects[2].GetComponent<TMP_InputField>().interactable = true;
        gameObjects[1].gameObject.SetActive(true);
    }

    void RandomNumber(int moveIndex)
    {
        _number = Random.Range((int)Mathf.Pow(10, moveIndex - 1), (int)Mathf.Pow(10, moveIndex));
        gameObjects[2].GetComponent<TMP_InputField>().text = _number.ToString();
    }

    IEnumerator GameBegins()
    {
        while (_gameOn)
        {
            if (!_usersTurn)
            {
                yield return StartCoroutine(LineTimer(NumberTimer));
                _usersTurn = true;
                yield return new WaitUntil(() => !_usersTurn);
            }
        }
    }
}
