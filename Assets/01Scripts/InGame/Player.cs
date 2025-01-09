using System;
using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;
using InGame.Layers;

public class Player : MonoBehaviour
{
    public enum StateEnum // 상태 패턴 도입해야함
    {
        Idle,
        Detect,
        Moving,
        Attack,
        Hitted,
        Death,
    }
    [SerializeField] private float detectSpeed = 0.5f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private UIHandler uiHandler;
    public StateEnum State { get; private set; } = StateEnum.Idle;
    public float Health { get; private set; } = 100f;
    public float MaxHealth { get; private set; } = 100f;
    public float Atk { get; private set; } = 100f;
    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }
        set
        {
            attackSpeed = value;
        }
    }
    [SerializeField] private Bullet bullet;
    [SerializeField] private PlayerDetectHandler detectHandler;

    private Vector3 moveDir;
    [SerializeField] private float moveSpeed = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case (StateEnum.Idle):
                Detect();
                break;
            case (StateEnum.Moving):
                Moving();
                break;
            case (StateEnum.Death):
                Death();
                break;
        }
    }

    public bool EnemyEnter(Collider2D other) // EnemyEnter Event! -> Attack!
    {
        switch (State)
        {
            case(StateEnum.Detect): // Stop Detect Enemy
                State = StateEnum.Attack;
                StartCoroutine(nameof(CoAttack), other);
                return true;
            case(StateEnum.Moving): // Stop Moving
                State = StateEnum.Attack;
                StartCoroutine(nameof(CoAttack), other);
                return true;
            // case(StateEnum.Attack): // Already Attack, Handler Cut
            //     return false;
            default:
                return false;
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Take Damage! hp: " + Health);
        Health -= damage;
        if (Health <= 0)
        {
            State = StateEnum.Death;
        }
        uiHandler.SetHpBar(Health / MaxHealth);
    }

    float detectRadius = 2f; //
    public void Detect() // PlayerDetectHandler
    {
        StartCoroutine(nameof(CoDetect), detectRadius);
    }

    public void Moving()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    
    private IEnumerator CoDetect()
    {
        State = StateEnum.Detect;
        
        Debug.Log("Detect ");
        detectRadius = 3f;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectRadius, Layers.Enemy);
        while (enemies.Length == 0)
        {
            if (detectRadius > 10f)
                break;
            detectRadius += 2f;
            enemies = Physics2D.OverlapCircleAll(transform.position, detectRadius, Layers.Enemy);
        }

        Debug.Log("destance: " + detectRadius + ", dir: " + moveDir);
        if (enemies.Length == 0)
        {
            Debug.Log("No enemies");
            yield return new WaitForSeconds(detectSpeed);
            State = StateEnum.Idle; // Re Detect
        }
        else // set dir
        {
            Debug.Log($"Find Enemy! {enemies.Length}");
            float minDis = 10000f;
            foreach (var enemy in enemies)
            {
                Vector3 tmp = enemy.transform.position - transform.position;
                if (minDis > tmp.sqrMagnitude)
                {
                    minDis = tmp.sqrMagnitude;
                    moveDir = tmp;
                }
            }
            moveDir.Normalize();
            State = StateEnum.Moving;
        }
    }

    private IEnumerator CoAttack(Collider2D other)
    {
        Bullet b = Instantiate<Bullet>(bullet, transform.position, Quaternion.identity);
        b.Initialize(other, Atk);
        yield return new WaitForSeconds(AttackSpeed);
        State = StateEnum.Idle;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        float angleStep = 1f;
        Vector3 prevPoint = transform.position + new Vector3(detectRadius, 0, 0);

        for (int i = 1; i <= 360; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = transform.position + new Vector3(Mathf.Cos(angle) * detectRadius, Mathf.Sin(angle) * detectRadius, 0);
            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }
}
