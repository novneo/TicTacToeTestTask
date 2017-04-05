using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour {
    // to be assigned in editor
    public GameObject difficultyButton;

    // Use this for initialization
    void Start () {
        UpdateDifficultyButtonText();
    }

    public void ChangeDifficultyLevel()
    {
        GameControl.aiLevel += 1;
        if (GameControl.aiLevel > 2) GameControl.aiLevel = 0;
        UpdateDifficultyButtonText();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("game");
    }

    private void UpdateDifficultyButtonText()
    {
        difficultyButton.transform.Find("Text").GetComponent<Text>().text = "Уровень ИИ: " + GameControl.GetAiLevelName();
    }
}