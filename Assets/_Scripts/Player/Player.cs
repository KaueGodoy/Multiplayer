using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour, IKitchenObjectParent
{
    public static Player LocalInstance { get; private set; }

    public static event EventHandler OnAnyPlayerSpawned;
    public static event EventHandler OnAnyPickedSomething;

    public static void ResetStaticData()
    {
        OnAnyPlayerSpawned = null;
    }

    public event EventHandler OnPickedSomething;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private LayerMask _countersLayerMask;
    [SerializeField] private Transform _kitchenObjectHoldPoint;

    private Vector3 _lastInteractionDir;

    private bool isWalking;

    private BaseCounter _selectedClearCounter;
    private KitchenObject _kitchenObject;

    private void Start()
    {
        GameInput.Instance.OnInteractAction += _gameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += _gameInput_OnInteractAlternateAction;
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
        }

        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void _gameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (_selectedClearCounter != null)
        {
            _selectedClearCounter.InteractAlternate(this);
        }
    }

    private void _gameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (_selectedClearCounter != null)
        {
            _selectedClearCounter.Interact(this);
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractionDir = moveDir;
        }

        float interactionDistance = 2f;

        if (Physics.Raycast(transform.position, _lastInteractionDir, out RaycastHit raycastHit, interactionDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedClearCounter)
                    SetSelectedCounter(baseCounter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

        //Debug.Log(_selectedClearCounter);

    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = _moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                            playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // cant move towards moveDir vector

            // attempt only X movement vector
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                            playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // cant move only on the X

                // attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                                playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    // cant move in any direction
                }

            }

        }


        if (canMove)
            transform.position += moveDir * moveDistance;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this._selectedClearCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = _selectedClearCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
            OnAnyPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }
}
