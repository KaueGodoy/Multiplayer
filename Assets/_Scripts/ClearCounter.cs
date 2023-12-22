using UnityEngine;

public class ClearCounter : BaseCounter
{
    //[SerializeField] private KitchenObjectSO _kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())

        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // player not carrying anything
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                // player already carrying an object
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
