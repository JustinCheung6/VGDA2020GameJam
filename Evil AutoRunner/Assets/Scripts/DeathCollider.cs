using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCollider : MonoBehaviour
{
    [SerializeField] private float killTime = 1f;
    
    private bool killZone = false;

    private MovingBlock parent = null;

    private void Awake()
    {
        parent = GetComponentInParent<MovingBlock>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            killZone = true;
            parent.Moving = false;
            StartCoroutine(InKillZone(0f));
        }
    }
    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            killZone = false;
            parent.Moving = true;
        }
    }

    private IEnumerator InKillZone(float countDown)
    {
        yield return new WaitForSeconds(0.01f);
        if (!killZone)
            yield break;

        if (countDown >= killTime)
            FindObjectOfType<ResetGame>().GameOver();

        StartCoroutine(InKillZone(countDown + 0.01f));
    }
}
