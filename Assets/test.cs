using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class test : MonoBehaviour
{
    public GameObject[] TestButtons;
    public void ChangeObjects()
    {
        TestButtons[0].SetActive(false);
        TestButtons[1].SetActive(true);
    }
}
