using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour {
    // need to be assigned in editor
    public GameObject difficultyText;
    public GameObject winsText;
    public GameObject loosesText;
    public GameObject drawsText;
    public GameObject[] fieldButtons;
    public GameObject endGamePopup;

    // private variables
    private int[] field = { 2, 2, 2, 2, 2, 2, 2, 2, 2 }; // current field data: 0 - cross, 1 - circle, 2 - empty
    private int player; // 0 - crosses, 1 - circles
    private int ai; // 0 - crosses, 1 - circles
    private bool gameActive = true;

    // public static data
    public static int[][] allWinLines = { new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8 }, new int[] { 0, 3, 6 }, new int[] { 1, 4, 7 }, new int[] { 2, 5, 8 }, new int[] { 0, 4, 8 }, new int[] { 2, 4, 6 } };

    void Start () {
        LoadGameInit();
    }

    private void LoadGameInit()
    {
        // decide who plays first
        switch (GameControl.lastGameResult)
        {
            case "draw": player = GameControl.lastGamePlayer == 0 ? 1 : 0; break;
            case "win": player = 0; break;
            case "loose": player = 1; break;
        }
        ai = player == 0 ? 1 : 0;

        // if AI first, let him do a move
        if (ai == 0)
        {
            MakeMove(ai, TicTacToeAI.AImakeMove(field, ai, GameControl.aiLevel));
        }

        // make texts show current data
        difficultyText.GetComponent<Text>().text = "Уровень ИИ: " + GameControl.GetAiLevelName();
        winsText.GetComponent<Text>().text = "Победы: " + GameControl.wins.ToString();
        loosesText.GetComponent<Text>().text = "Поражения: " + GameControl.looses.ToString();
        drawsText.GetComponent<Text>().text = "Ничьи: " + GameControl.draws.ToString();
    }

    public void FieldButtonClick(int position)
    {
        // return if position is already taken
        if (field[position] != 2) return;

        // make move
        MakeMove(player, position);

        // check end game
        CheckEndGame();

        // return if game ended
        if (!gameActive) return;

        // make AI move
        MakeMove(ai, TicTacToeAI.AImakeMove(field, ai, GameControl.aiLevel));

        // check end game
        CheckEndGame();
    }

    private void MakeMove(int player, int move)
    {
        // change field data
        field[move] = player;
        // visualize changes
        fieldButtons[move].transform.Find("Text").GetComponent<Text>().text = player == 0 ? "X" : "O";
    }

    private void CheckEndGame()
    {
        // check all win lines and possible win lines
        int winner = 2;
        int possibleWinLines = 0;
        for (int i = 0; i < allWinLines.Length; i++)
        {
            if (field[allWinLines[i][0]] != 2 && field[allWinLines[i][0]] == field[allWinLines[i][1]] && field[allWinLines[i][0]] == field[allWinLines[i][2]])
            {
                winner = field[allWinLines[i][0]];
            }
            if ((field[allWinLines[i][0]] == 2 || field[allWinLines[i][1]] == 2 || field[allWinLines[i][2]] == 2) && !(field[allWinLines[i][0]] != field[allWinLines[i][1]] && field[allWinLines[i][0]] != field[allWinLines[i][2]] && field[allWinLines[i][1]] != field[allWinLines[i][2]]))
            {
                possibleWinLines++;
            }
        }

        if (winner != 2)
        {
            GameEnded(winner);
            return;
        }

        if (possibleWinLines == 0)
        {
            GameEnded(2);
            return;
        }

        // check no more moves
        int movesLeft = 0;
        for (int i = 0; i < field.Length; i++)
        {
            if (field[i] == 2) movesLeft++;
        }
        if (movesLeft == 0)
        {
            GameEnded(2);
            return;
        }
    }

    // winner: 0 - cross, 1 - circle, 2 - draw
    private void GameEnded(int winner)
    {
        // stop the game
        gameActive = false;

        // add score
        string resultText = "";
        if (winner == player)
        {
            GameControl.wins++;
            resultText = "Победа!";
            GameControl.lastGameResult = "win";
        }
        if (winner == ai)
        {
            GameControl.looses++;
            resultText = "Поражение";
            GameControl.lastGameResult = "loose";
        }
        if (winner == 2)
        {
            GameControl.draws++;
            resultText = "Ничья";
            GameControl.lastGameResult = "draw";
        }

        // change global vars
        GameControl.lastGamePlayer = player;

        // show popup
        endGamePopup.SetActive(true);
        endGamePopup.transform.Find("ResultText").GetComponent<Text>().text = resultText;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("game");
    }
}