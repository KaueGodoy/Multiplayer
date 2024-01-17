using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Basic_PlayerNetwork : NetworkBehaviour
{

    [SerializeField] private Transform _spawnedObjectPrefab;
    private Transform _spawnedObjectTransform;

    private NetworkVariable<MyCustomData> _randomNumber = new NetworkVariable<MyCustomData>
        (new MyCustomData
        {
            Int = 56,
            Bool = true,
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    public struct MyCustomData : INetworkSerializable
    {
        public int Int;
        public bool Bool;
        public FixedString128Bytes Message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Int);
            serializer.SerializeValue(ref Bool);
            serializer.SerializeValue(ref Message);

        }
    }

    public override void OnNetworkSpawn()
    {
        _randomNumber.OnValueChanged += ((MyCustomData previousValue, MyCustomData newValue) =>
        {
            Debug.Log(OwnerClientId + " ; randomNumber: " + newValue.Int + "; " + newValue.Bool + "; " + newValue.Message);

        });
    }

    private void Update()
    {

        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {

            _spawnedObjectTransform = Instantiate(_spawnedObjectPrefab);
            _spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);

            //TestServerRpc(new ServerRpcParams());

            //TestClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } }});

            //_randomNumber.Value = new MyCustomData
            //{
            //    Int = 10,
            //    Bool = false,
            //    Message = "Testing syncing data"
            //};
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            //_spawnedObjectTransform.GetComponent<NetworkObject>().Despawn();

            Destroy(_spawnedObjectTransform.gameObject);
        }

        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    [ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams)
    {
        Debug.Log("Testing server RPC" + OwnerClientId + "; " + serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void TestClientRpc(ClientRpcParams clientRpcParams)
    {
        Debug.Log("Testing client RPC");
    }
}
