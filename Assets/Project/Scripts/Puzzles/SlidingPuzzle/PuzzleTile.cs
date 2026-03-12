using System;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    public SlidingPuzzle parentPuzzle;
    public int correctPosition; //the number indicating the correct position of the tile in the grid, starting from 0
    public int currentPosition;

    public bool isEmptyTile;
    public bool isInCorrectPosition;

    public static event Action<PuzzleTile> OnAttemptTileMoved;
    public static event Action InCorrectPosition;

    private void OnMouseDown()
    {
        OnAttemptTileMoved?.Invoke(this);
        CheckIfInCorrectPosition();
    }
    public void SetValues()
    {
        currentPosition = int.Parse(name);
        correctPosition = int.Parse(GetComponentInChildren<SpriteRenderer>().sprite.name);
        CheckIfInCorrectPosition();
    }
    public void CheckIfInCorrectPosition()
    {
        if (currentPosition == correctPosition)
            isInCorrectPosition = true;
        else
            isInCorrectPosition = false;

        if (isInCorrectPosition)
        {
            InCorrectPosition?.Invoke();
        }
    }
    public void SetEmpty()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        isEmptyTile = true;
    }
}
