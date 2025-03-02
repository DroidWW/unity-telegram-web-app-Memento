using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CubeGameManager : MonoBehaviour
{
    public GameObject[] gameButtons;

    private List<int> _sequenceOfMoves = new List<int>(); 
    private Color _greenColor, _redColor; 
    private bool _gameOn = true, _usersTurn = false; 
    private float _coloringDuration = 1f, _delayBetweenColorings = 0.5f;
    private int _userMoveIndex = 0;


    void Start()
    {
        SetGlobalColors();
        SetOnButtonClick();
        StartCoroutine(GameBegins());

    }

    void SetGlobalColors()
    {
        ColorUtility.TryParseHtmlString("#C5DD91", out _greenColor);
        ColorUtility.TryParseHtmlString("#FF8A84", out _redColor);
    }

    void SetOnButtonClick()
    {
        foreach (var button in gameButtons)
        {
            button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick());
        }
    }

    void OnButtonClick()
    {
        Debug.Log($"clicked");

        Button clickedButton = EventSystem.current.currentSelectedGameObject?.GetComponent<Button>();
        if (clickedButton == null)
        {
            Debug.LogWarning("Нажатый объект не является кнопкой!");
            return;
        }

        int buttonIndex = System.Array.IndexOf(gameButtons, clickedButton.gameObject);

        Debug.Log($"Нажата кнопка: {buttonIndex}");
        if (buttonIndex == _sequenceOfMoves[_userMoveIndex]) { 
            gameButtons[buttonIndex].GetComponent<Image>().color = _greenColor;
            Invoke("ColoringButtonToWhite", 0.2f);
            _userMoveIndex++;
        }
        else
        {
            gameButtons[buttonIndex].GetComponent<Image>().color = _redColor;
            Debug.LogWarning("Игра окончена");
        }
        if (_userMoveIndex == _sequenceOfMoves.Count)
            _usersTurn = false;
    }

    void ColoringButtonToWhite()
    {
        Button clickedButton = EventSystem.current.currentSelectedGameObject?.GetComponent<Button>();
        int buttonIndex = System.Array.IndexOf(gameButtons, clickedButton.gameObject);
        gameButtons[buttonIndex].GetComponent<Image>().color = Color.white;
    }


    IEnumerator GameBegins()
    {
        while (_gameOn)
        {
            if (!_usersTurn)
            {
                _userMoveIndex = 0;
                yield return StartCoroutine(ColoringSequence());
                _usersTurn = true;
                yield return new WaitUntil(() => !_usersTurn);
                yield return new WaitForSeconds(_coloringDuration);
            }

        }
    }

    IEnumerator ColoringSequence()
    {
        int randomIndex = Random.Range(0, gameButtons.Length);
        _sequenceOfMoves.Add(randomIndex);
        for (int j = 0; j < _sequenceOfMoves.Count; j++)
        {
            int buttonIndex = _sequenceOfMoves[j];
            gameButtons[buttonIndex].GetComponent<Image>().color = _greenColor;
            yield return new WaitForSeconds(_coloringDuration);
            gameButtons[buttonIndex].GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(_delayBetweenColorings);
        }
    }
}