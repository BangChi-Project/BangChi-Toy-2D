using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : Weapon
{
    private Vector3 targetDir;
    private Collider2D target; // Guided
    private float t;
    [SerializeField] private float deleteTime;

    [SerializeField] float bulletSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = 0;
        StartCoroutine(nameof(CoDeleteBullet), deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case(InGameManager.StateEnum.Start):
                break;
            case(InGameManager.StateEnum.Running):
                t += Time.deltaTime;
                transform.position += targetDir * bulletSpeed * Time.deltaTime;
                break;
            case(InGameManager.StateEnum.Pause):
                break;
            case(InGameManager.StateEnum.End):
                break;
        }
        
        // // Chase enemy like guided missile
        // Vector3 enemyPos = target.transform.position;
        // Vector3 dir = (enemyPos - transform.position).normalized;
        // transform.position += dir * bulletSpeed * Time.deltaTime;
    }
    
    protected override void OnStateChange(InGameManager.StateEnum state)
    {
        State = state;
        switch (state)
        {
            case(InGameManager.StateEnum.Running):
                StartCoroutine(nameof(CoDeleteBullet), deleteTime-t);
                break;
            case(InGameManager.StateEnum.Pause):
                StopCoroutine(nameof(CoDeleteBullet));
                break;
            case(InGameManager.StateEnum.End):
                StopCoroutine(nameof(CoDeleteBullet));
                break;
        }
    }

    public void SetTarget(Collider2D other)
    {
        targetDir = (other.gameObject.transform.position - transform.position).normalized;
        
        target = other; // test;
    }

    IEnumerator CoDeleteBullet(float lifeTime)
    {
        if (lifeTime <= 0)
            lifeTime = 0;
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Delete or any effect
        if (other.CompareTag(InGameManager.Instance.enemyTag))
        {
            other.GetComponent<Enemy>().TakeDamage(Atk);
            // Destroy(other.gameObject);
        }
    }

    public override void Initialize(Collider2D other, float atk)
    {
        SetTarget(other);
        Atk = atk;
    }
}
