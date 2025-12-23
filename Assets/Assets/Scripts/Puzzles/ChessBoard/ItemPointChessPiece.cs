
public class ItemPointChessPiece : ItemPoint
{
    public override void UseItemOnPoint(Item item)
    {
        manager.GetComponent<ChessPuzzle>().UseItem(item, this);
    }
}
