using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    private Text text = null;
    private BlockManager blockManager = null;

    private void Awake()
    {
        text = GetComponent<Text>();
        blockManager = GameObject.FindGameObjectWithTag("BlockManager").GetComponent<BlockManager>();
    }

    private void Update()
    {
        text.text =("Level: " + blockManager.Level);
    }
}
