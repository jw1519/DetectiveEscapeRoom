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
    public GameObject Reward;

    public List<Sprite> sprites; //tile sprites
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
        CreateBoard();
    }
    public void CreateBoard()
    {
        tileParent.localPosition = new Vector3(-spacing, -spacing, 0);
        int tileIndex = sprites.Count - 1;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 pos = new Vector3(x, y, 0) * spacing;

                GameObject prefabInstance = Instantiate(TilePrefab, tileParent);
                prefabInstance.transform.localPosition = pos;

                prefabInstance.GetComponentInChildren<SpriteRenderer>().sprite = sprites[tileIndex]; // Assign the corresponding sprite to the tile
                prefabInstance.name = tileIndex.ToString(); // Set the name to the tile index for correct position reference
                prefabInstance.GetComponent<PuzzleTile>().SetValues();
                tiles[tileIndex] = prefabInstance;
                tileIndex--;
            }
        }
        int index = tiles.Count - 1;
        tiles[index].GetComponent<PuzzleTile>().SetEmpty();
        ShuffleTiles();
    }
    public void ShuffleTiles()
    {
        int count = 0;
        while (count < (gridSize * gridSize) * gridSize)
        {
            int random = Random.Range(0, gridSize * gridSize - 1);
            PuzzleTile tile = tiles[random].GetComponent<PuzzleTile>();
            if (IsAdjacentToEmpty(tile))
            {
                HandleTileMoveAttempt(tile);
                count++;
            }
        }
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<PuzzleTile>().SetValues();
        }
    }
    public void CheckSolved()
    {
        if (IsSolved())
        {
            Debug.Log("Puzzle Solved!");
            Reward.SetActive(true);
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

        GameObject tileGO = tile.gameObject;

        if ((tileIndex + 1) <= tiles.Count - 1 && (tileIndex + 1) % gridSize != 0) //check right, check if tile index + 1 is divisable by gridsize
        {
            if (tileIndex + 1 <= tiles.Count && tiles[tileIndex + 1].GetComponent<PuzzleTile>().isEmptyTile) 
                return true;
        }
        if ((tileIndex - 1) >= 0 && tileIndex % gridSize != 0) //left
        {
            if (tiles[tileIndex - 1] != null && tiles[tileIndex - 1].GetComponent<PuzzleTile>().isEmptyTile) 
                return true;
        }
        //down
        if (tileIndex + gridSize <= tiles.Count - 1)
        {
            if (tiles[tileIndex + gridSize] != null && tiles[tileIndex + gridSize].GetComponent<PuzzleTile>().isEmptyTile)
                return true;
        }
        if (tileIndex - gridSize >= 0)
        {
            if (tiles[tileIndex - gridSize] != null && tiles[tileIndex - gridSize].GetComponent<PuzzleTile>().isEmptyTile) //check up
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
