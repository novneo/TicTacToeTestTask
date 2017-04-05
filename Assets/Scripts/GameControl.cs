using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    // score variables
    public static int wins = 0;
    public static int looses = 0;
    public static int draws = 0;

    // AI level (0 - easy; 1 - medium; 2 - hard)
    public static int aiLevel = 0;

    // last game data
    public static string lastGameResult = "draw";
    public static int lastGamePlayer = 1; // 0 - crosses, 1 - circles

	void Start () {
		
	}

    public static string GetAiLevelName()
    {
        string difficultyLevelText = "";
        switch (aiLevel)
        {
            case 0: difficultyLevelText = "легкий"; break;
            case 1: difficultyLevelText = "средний"; break;
            case 2: difficultyLevelText = "непобедимый"; break;
        }
        return difficultyLevelText;
    }
}