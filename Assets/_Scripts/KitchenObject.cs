using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return _kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {   // clear obj from old counter
        if (this._kitchenObjectParent != null)
            this._kitchenObjectParent.ClearKitchenObject();

        // this obj receives the new counter reference
        this._kitchenObjectParent = kitchenObjectParent;

        // check if counter has obj placed already
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has an object");
        }

        // the new counter receives this obj
        kitchenObjectParent.SetKitchenObject(this);

        // update the visual
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return _kitchenObjectParent;
    }
}
