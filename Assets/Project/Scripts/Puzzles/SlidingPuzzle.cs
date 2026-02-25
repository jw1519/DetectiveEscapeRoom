using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzle : MonoBehaviour
{
    public int gridSize = 3;
    public Transform tileParent;
    public GameObject TilePrefab;
    public List<GameObject> tiles; // List of tile GameObjects, including the empty space
    public float spacing = 1.1f; // Spacing between tiles

    public Sprite[] tileSprites; // List of sprites for the tiles, should be assigned in the inspector
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
    private void Awake()
    {
        //CreateBoard();
    }
    public void CreateBoard()
    {
        tileParent.localPosition = new Vector3(-spacing, -spacing, 0);
        int tileIndex = 0;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 pos = new Vector3(x, y, 0) * spacing;

                GameObject prefabInstance = Instantiate(TilePrefab, tileParent);
                prefabInstance.transform.localPosition = pos;
                prefabInstance.GetComponentInChildren<SpriteRenderer>().sprite = tileSprites[tileIndex]; // Assign the corresponding sprite to the tile
                prefabInstance.name = tileIndex.ToString(); // Set the name to the tile index for correct position reference
                tileIndex++;
                tiles.Add(prefabInstance);
            }
        }
        int index = tiles.Count - 1;
        tiles[index].SetActive(false); // Hide the last tile to create the empty space
        tiles[index].GetComponent<PuzzleTile>().isEmptyTile = true; // Set the last tile as the empty space
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
            GameObject emptyTile = tiles.Find(t => t.GetComponent<PuzzleTile>().isEmptyTile);

            int tileIndex = tiles.IndexOf(tile.gameObject);
            int emptyTileIndex = tiles.IndexOf(emptyTile);

            Vector3 TilePosition = tile.transform.position;
            Vector3 BlankPosition = emptyTile.transform.position;

            tile.transform.position = BlankPosition;
            emptyTile.transform.position = TilePosition;

            if (tileIndex < emptyTileIndex)
            {
                tiles[tileIndex] = emptyTile;
                tiles[emptyTileIndex] = tile.gameObject;
            }
            else
            {
                tiles[emptyTileIndex] = tile.gameObject;
                tiles[tileIndex] = emptyTile;
            }
            tile.currentPosition = emptyTileIndex; // Update the current position of the tile to the index of the empty space
            emptyTile.GetComponent<PuzzleTile>().currentPosition = tileIndex; // Update the current position of the empty tile to the index of the moved tile


        }
    }
}
