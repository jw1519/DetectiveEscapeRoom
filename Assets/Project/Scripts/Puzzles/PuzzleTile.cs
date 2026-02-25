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

    void Start()
    {
        isInCorrectPosition = false;
        //correctPosition = int.Parse(gameObject.name); // Assuming the tile's name is set to its correct position
        CheckIfInCorrectPosition();
    }
    private void OnMouseDown()
    {
        OnAttemptTileMoved?.Invoke(this);
        CheckIfInCorrectPosition();
    }
    public void CheckIfInCorrectPosition()
    {
        isInCorrectPosition = (currentPosition == correctPosition);
        if (isInCorrectPosition)
        {
            InCorrectPosition?.Invoke();
        }
    }
}
