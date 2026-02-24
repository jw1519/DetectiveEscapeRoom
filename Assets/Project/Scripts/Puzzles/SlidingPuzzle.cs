using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzle : MonoBehaviour
{
    public int gridSize = 3; // Size of the grid (e.g., 3 for a 3x3 puzzle)
    public List<GameObject> tiles; // List of tile GameObjects, including the empty space

    private void OnEnable()
    {
        PuzzleTile.OnAttemptTileMoved += HandleTileMoveAttempt;
        PuzzleTile.InCorrectPosition += CheckSolved;
    }
    private void OnDisable()
    {
        PuzzleTile.OnAttemptTileMoved -= HandleTileMoveAttempt;
        PuzzleTile.InCorrectPosition -= CheckSolved;
    }

    public void CheckSolved()
    {
        if (IsSolved())
        {
            Debug.Log("Puzzle Solved!");
            // Additional logic for when the puzzle is solved (e.g., trigger an event, show a message, etc.)
        }
    }
    public bool IsSolved()
    {
        for (int i = 0; i < tiles.Count - 1; i++)
        {
            if (!tiles[i].GetComponent<PuzzleTile>().isInCorrectPosition)
                return false;
        }
        return true;
    }
    public bool IsAdjacentToEmpty(PuzzleTile tile)
    {
        int tileIndex = tiles.IndexOf(tile.gameObject);

        if (tiles[tileIndex].GetComponent<PuzzleTile>().isEmptyTile) //if tile is empty dont move
            return false;
        if ((tileIndex + 1) % gridSize != 0)
        {
            if (tileIndex + 1 <= tiles.Count && tiles[tileIndex + 1].GetComponent<PuzzleTile>().isEmptyTile) //check right
                return true;
        }
        if (tileIndex % gridSize != 0) //if tile is on the left edge, it cannot check left
        {
            if (tiles[tileIndex - 1] != null && tiles[tileIndex - 1].GetComponent<PuzzleTile>().isEmptyTile) //check left
            return true;
        }
        if (tileIndex + gridSize <= tiles.Count)
        {
            if (tiles[tileIndex + gridSize] != null && tiles[tileIndex + gridSize].GetComponent<PuzzleTile>().isEmptyTile) //check down
                return true;
        }
        else if (tileIndex + gridSize > tiles.Count) //if tile is on the bottom edge, it cannot check down
        {
            int tileToCheck = tileIndex + gridSize - tiles.Count;
            if (tiles[tileToCheck] != null && tiles[tileToCheck].GetComponent<PuzzleTile>().isEmptyTile) //check down for edge case when tile is on the bottom row
                return true;
        }
        if (tileIndex - gridSize >= 0)
        {
            if (tiles[tileIndex - gridSize] != null && tiles[tileIndex - gridSize].GetComponent<PuzzleTile>().isEmptyTile) //check up
                return true;
        }
        else if (tileIndex - gridSize < 0) //if tile is on the top edge, it cannot check up
        {
            int tileToCheck = tileIndex - gridSize + tiles.Count;
            if (tiles[tileToCheck] != null && tiles[tileToCheck].GetComponent<PuzzleTile>().isEmptyTile) //check up for edge case when tile is on the top row
                return true;
        }

        return false;
    }
    public void HandleTileMoveAttempt(PuzzleTile tile)
    {
        if (IsAdjacentToEmpty(tile))
        {
            Vector3 TilePosition = tile.transform.position;
            Vector3 BlankPosition = tiles.Find(t => t.GetComponent<PuzzleTile>().isEmptyTile).transform.position;

            tile.transform.position = BlankPosition;
            tiles.Find(t => t.GetComponent<PuzzleTile>().isEmptyTile).transform.position = TilePosition;
        }
    }
}
