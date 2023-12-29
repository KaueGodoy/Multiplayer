using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject GameObject;
    }

    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSO_GameObjectsList;

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += _plateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in _kitchenObjectSO_GameObjectsList)
        {
            kitchenObjectSO_GameObject.GameObject.SetActive(false);
        }
    }

    private void _plateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in _kitchenObjectSO_GameObjectsList)
        {
            if (kitchenObjectSO_GameObject.KitchenObjectSO == e.KitchenObjectSO)
            {
                kitchenObjectSO_GameObject.GameObject.SetActive(true);
            }
        }
    }
}
