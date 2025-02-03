using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int IdNumber { get; private set; } // DB Item CodeNumber
    public string Name { get; private set; }
    public int Amount { get; private set; }

    private float moveSpeed = 3f;
    private bool isMove = false;

    private void Start()
    {
        StartCoroutine(nameof(CoFollowPlayer));
    }

    private void Update()
    {
        if (InGameManager.Instance.GameState == InGameManager.StateEnum.Running)
        {
            if (isMove)
            {
                Moving();
            }
        }
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public void SetDisable()
    {
        StopCoroutine(nameof(CoFollowPlayer));
        this.gameObject.SetActive(false);
    }
    public void Initialize(int idNumber, string name, int amount)
    {
        this.gameObject.SetActive(true);
        
        isMove = false;
        StartCoroutine(nameof(CoFollowPlayer));
        
        IdNumber = idNumber;
        Name = name;
        Amount = amount;
    }

    IEnumerator CoFollowPlayer()
    {
        yield return new WaitForSeconds(1f);
        isMove = true;
    }

    void Moving()
    {
        Vector3 playerPos = InGameManager.Instance.PlayerPos;

        Vector3 dir = (playerPos - transform.position).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
