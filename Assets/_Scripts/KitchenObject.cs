using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    private ClearCounter _clearCounter;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return _kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {   // clear obj from old counter
        if (this._clearCounter != null)
            this._clearCounter.ClearKitchenObject();

        // this obj receives the new counter reference
        this._clearCounter = clearCounter;

        // check if counter has obj placed already
        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has an object");
        }

        // the new counter receives this obj
        clearCounter.SetKitchenObject(this);

        // update the visual
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return _clearCounter;
    }
}
