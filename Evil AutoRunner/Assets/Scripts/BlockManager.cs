using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [Header("Level Time")]
    [SerializeField] private float levelStartTime = 30f;
    [SerializeField] private float levelDeltaTime = 5f;
    //Time it takes for player to complete a level
    private float levelTime = 30f;
    //Current Level Player is in
    private int level = 1;
    public int Level { get => level; }
    //For deciding what factor of level gets harder 
    private int tempLevel = 1;
    private float runTime = 0;

    [Header("Customizables")]
    [SerializeField] private float blockSpeed = 10;
    [SerializeField] private float waveDelay = 3;
    [SerializeField] private int numberBlocks = 1;
    public float BlockSpeed { get => blockSpeed; }

    private BlockSpawner[,] spawners = new BlockSpawner[3, 3];
    private bool[,] spawned = new bool[3,3];

    private int option = 1;
    private int maxOptions = 1;
    private void Awake()
    {
        BlockSpawner[] tempSpawnerList = GetComponentsInChildren<BlockSpawner>();

        spawned = new bool[,] { { false, false, false }, { false, false, false }, { false, false, false } };

        levelTime = levelStartTime;

        foreach (BlockSpawner spawner in tempSpawnerList)
        {
            spawners[spawner.x, spawner.y] = spawner;
        }
        StartCoroutine(SpawnBlocks());
    }

    private void Update()
    {
        runTime += Time.deltaTime;

        if(runTime >= levelTime)
        {
            runTime -= levelTime;
            NextLevel();
        }
    }

    private void NextLevel()
    {
        levelTime += levelDeltaTime;
        if(tempLevel == 1)
        {
            blockSpeed++;
            tempLevel++;
        }
        else if(tempLevel == 2)
        {
            if(waveDelay > 0.75f)
            {
                waveDelay -= 0.75f;
                tempLevel++;
            }
            else
            {
                tempLevel++;
            }
            
        }
        else
        {
            numberBlocks += (numberBlocks < 5) ? 1 : 0;
            tempLevel = 1;
            level++;

            if (numberBlocks >= 2)
                maxOptions = 2;
            if (numberBlocks == 5)
                maxOptions = 3;
            else
            {
                blockSpeed++;
                tempLevel++;
            }
        }
    }

    private void SpawnRandomBlock()
    {
        for (int i = 0; i < numberBlocks; i++)
        {
            int[] index = { Random.Range(0, 3), Random.Range(0, 3) };
            for(int j = 0; j < 100; j++)
            {
                if (spawned[index[0], index[1]])
                    index = new int[] { Random.Range(0, 3), Random.Range(0, 3) };
                else
                    break;
            }
            if(!spawned[index[0], index[1]])
            {
                spawners[index[0], index[1]].SpawnBlock();
                spawned[index[0], index[1]] = true;
            }
            else
                Debug.Log("We lost");
        }
    }
    
    private void SpawnBlockLine()
    {
        int index = Random.Range(0, 3);
        int widthOrHeight = Random.Range(0, 2);

        foreach (BlockSpawner spawner in spawners)
        {
            if(numberBlocks < 4) 
            {
                if ((widthOrHeight == 0) ? (spawner.x == index) : (spawner.y == index))
                {
                    spawner.SpawnBlock();
                }
            }
            else
            {
                if ((widthOrHeight == 0) ? (spawner.x != index) : (spawner.y != index))
                {
                    spawner.SpawnBlock();
                }
            }
        }
    }
    private void SpawnHole()
    {
        int[] index = { Random.Range(0, 3), Random.Range(0, 3) };

        foreach (BlockSpawner spawner in spawners)
        {
            if (spawner.x != index[0] && spawner.y != index[1])
                spawner.SpawnBlock();
        }
    }

    private IEnumerator SpawnBlocks()
    {
        spawned = new bool[,] { { false, false, false }, { false, false, false }, { false, false, false } };
        option = Random.Range(1, maxOptions + 1);
        yield return new WaitForSeconds(waveDelay);

        Debug.Log("Option: " + option);

        if (option == 1)
            SpawnRandomBlock();
        else if (option == 2)
            SpawnBlockLine();
        else
            SpawnHole();

        StartCoroutine(SpawnBlocks());
    }
}
