using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingBlock : MonoBehaviour
{
    private BlockManager blockManager = null;
    [SerializeField] private bool moving;
    public bool Moving { get => moving; set => moving = value; }

    private void Awake()
    {
        blockManager = GameObject.FindGameObjectWithTag("BlockManager").GetComponent<BlockManager>();
    }

    private void OnEnable()
    {
        moving = true;
    }
    private void OnDisable()
    {
        moving = false;
    }
    private void Update()
    {
        if(moving)
            transform.position += new Vector3(0f, 0f, -blockManager.BlockSpeed) * Time.deltaTime;
    }

}
