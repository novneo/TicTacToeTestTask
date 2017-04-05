using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeAI : MonoBehaviour {
    public static int AImakeMove(int[] field, int player, int difficulty)
    {
        // make local copy of all win lines array
        int[][] allWinLines = MainGameController.allWinLines;

        // make win move if possible
        for (int i = 0; i < allWinLines.Length; i++)
        {
            List<int> self = new List<int>();
            List<int> enemy = new List<int>();
            List<int> empty = new List<int>();
            for (int j = 0; j < 3; j++)
            {
                if (field[allWinLines[i][j]] == 2) empty.Add(allWinLines[i][j]);
                    else if (field[allWinLines[i][j]] == player) self.Add(allWinLines[i][j]);
                    else enemy.Add(allWinLines[i][j]);
                if (self.Count == 2 && empty.Count == 1) return empty[0];
            }
        }

        // don't think more on easy difficulty
        if (difficulty == 0) return AIrandomMove(field);

        // sometimes make silly moves on meduim difficulty
        if (difficulty == 1 && UnityEngine.Random.Range(0, 100) < 30) return AIrandomMove(field);

        // block enemy possible win move
        for (int i = 0; i < allWinLines.Length; i++)
        {
            List<int> self = new List<int>();
            List<int> enemy = new List<int>();
            List<int> empty = new List<int>();
            for (int j = 0; j < 3; j++)
            {
                if (field[allWinLines[i][j]] == 2) empty.Add(allWinLines[i][j]);
                else if (field[allWinLines[i][j]] == player) self.Add(allWinLines[i][j]);
                else enemy.Add(allWinLines[i][j]);
                if (enemy.Count == 2 && empty.Count == 1) return empty[0];
            }
        }

        // don't think more on medium difficulty
        if (difficulty == 1) return AIrandomMove(field);

        // move in center if empty
        if (field[4] == 2) return 4;

        // make move in corner if any free
        List<int> emptyCorners = new List<int>();
        if (field[0] == 2) emptyCorners.Add(0);
        if (field[2] == 2) emptyCorners.Add(2);
        if (field[6] == 2) emptyCorners.Add(6);
        if (field[8] == 2) emptyCorners.Add(8);
        if (emptyCorners.Count > 0)
        {
            return emptyCorners[UnityEngine.Random.Range(0, emptyCorners.Count)];
        }

        // make move in sides
        List<int> emptySides = new List<int>();
        if (field[1] == 2) emptySides.Add(1);
        if (field[3] == 2) emptySides.Add(3);
        if (field[5] == 2) emptySides.Add(5);
        if (field[7] == 2) emptySides.Add(7);
        if (emptySides.Count > 0)
        {
            return emptySides[UnityEngine.Random.Range(0, emptySides.Count)];
        }

        // random move if no found
        return AIrandomMove(field);
    }

    // make random move from all availible
    private static int AIrandomMove(int[] field)
    {
        List<int> possibleMoves = new List<int>();

        for (int i = 0; i < field.Length; i++)
        {
            if (field[i] == 2) possibleMoves.Add(i);
        }

        return possibleMoves[UnityEngine.Random.Range(0, possibleMoves.Count)];
    }
}
