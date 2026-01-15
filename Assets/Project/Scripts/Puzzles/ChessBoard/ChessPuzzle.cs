using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChessPuzzle : MonoBehaviour
{
    public List<Item> chessPieces;
    public List<ItemPoint> itemPoints;

    // Check if all chess pieces are in their correct positions
    public bool IsPuzzleSolved()
    {
        foreach (var piece in chessPieces)
        {
            if (!piece.isInCorrectPosition)
            {
                return false;
            }
        }
        return true;
    }
    public void UseItem(Item item, ItemPoint itemPoint)
    {
        foreach (var piece in chessPieces)
        {
            //check if item is part of this puzzle
            if (piece.itemID == item.itemID)
            {
                //check if item is in correct spot
                if (piece.itemID == itemPoint.itemNeeded.itemID)
                {
                    item.isInCorrectPosition = true;
                }
                else
                {
                    item.isInCorrectPosition = false;
                }
                //place item
                item.PlaceItem(itemPoint.transform);
                Debug.Log("Placed");
            }
        }
        if (IsPuzzleSolved())
        {
            Debug.Log("chess puzzle done");
        }
    }
}
