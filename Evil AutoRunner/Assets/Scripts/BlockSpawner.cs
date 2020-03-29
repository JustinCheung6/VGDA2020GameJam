using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private Vector2Int gridPosition;
    public int x { get => gridPosition.x; }
    public int y { get => gridPosition.y; }

    //Object References
    [SerializeField] private ObjectPooler blockPool = null;

    private void Awake()
    {
        blockPool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooler>();
    }

    public void SpawnBlock()
    {
        GameObject block = blockPool.GetBlock();

        block.transform.position = transform.position;
        block.SetActive(true);
    }
}
