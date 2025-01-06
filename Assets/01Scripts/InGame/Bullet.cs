using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour
{
    private Vector3 targetDir;
    private Collider2D target; // test
    [SerializeField] private float deleteTime;

    [SerializeField] float bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(nameof(CoDeleteBullet));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = InGameManager.Instance.GetPlayerPos();
        
        transform.position += targetDir * bulletSpeed * Time.deltaTime;
        
        // // Chase enemy like guided missile
        // Vector3 playerPos = target.transform.position;
        // Vector3 dir = (playerPos - transform.position).normalized;
        // transform.position += dir * bulletSpeed * Time.deltaTime;
    }

    public void SetTarget(Collider2D other)
    {
        targetDir = (other.gameObject.transform.position - transform.position).normalized;
        
        target = other; // test;
    }

    IEnumerator CoDeleteBullet()
    {
        yield return new WaitForSeconds(deleteTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Delete or any effect
        // if (other.CompareTag(InGameManager.Instance.enemyTag)) {}
    }
}
