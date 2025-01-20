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
    [SerializeField] private float atk = 100f;
    [SerializeField] private StateUIHandler _stateUIHandler;
    [SerializeField] private PlayerDetectHandler detectHandler;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float moveSpeed = 1f;
    private Vector3 moveDir;
    private float runnedTime;

    public StateEnum State { get; private set; } = StateEnum.Idle;
    public InGameManager.StateEnum GameState { get; private set; } = InGameManager.StateEnum.Running;
    public float Health { get; private set; } = 100f;
    public float MaxHealth { get; private set; } = 100f;

    [SerializeField]
    public float Atk
    {
        get
        {
            return atk;
        }
        private set
        {
            atk = value;
        }
    }

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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InGameManager.Instance.OnStateChange += OnStateChange;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState == InGameManager.StateEnum.Running)
        {
            runnedTime += Time.deltaTime;
            switch (State)
            {
                case (StateEnum.Idle):
                    Detect();
                    break;
                case (StateEnum.Moving):
                    Moving();
                    break;
                case (StateEnum.Death):
                    InGameManager.Instance.SetGameState(InGameManager.StateEnum.End);
                    Death();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(InGameManager.Instance.itemTag))
        {
            var item = other.GetComponent<Item>();
            InGameManager.Instance.AddItem(item);
            Destroy(other.gameObject);
        }
    }

    // public void OnEnable()
    // {
    //     InGameManager.Instance.OnStateChange += OnStateChange;
    // }
    public void OnDisable()
    {
        InGameManager.Instance.OnStateChange -= OnStateChange;
    }

    private void OnStateChange(InGameManager.StateEnum state)
    {
        Debug.Log($"OnStateChange::playerState:{State}");
        GameState = state;
        switch (state)
        {
            case(InGameManager.StateEnum.Running):
                if (State == StateEnum.Attack)
                    StartCoroutine(nameof(CoAttack), attackSpeed-runnedTime);
                if (State == StateEnum.Detect) // when State is Attack, cant Detect
                    StartCoroutine(nameof(CoDetect), detectSpeed-runnedTime);
                break;
            case(InGameManager.StateEnum.Pause):
                StopCoroutine(nameof(CoAttack));
                StopCoroutine(nameof(CoDetect));
                break;
        }
    }

    public bool EnemyEnter(Collider2D other) // EnemyEnter Event! -> Attack!
    {
        switch (State)
        {
            case(StateEnum.Detect): // Stop Detect Enemy
                Attack(other);
                return true;
            case(StateEnum.Moving): // Stop Moving
                runnedTime = 0;
                Attack(other);
                return true;
            // case(StateEnum.Attack): // Already Attack, Handler Cut
            //     return false;
            default:
                return false;
        }
    }

    public void TakeDamage(float damage)
    {
        _stateUIHandler.PresentDamageText(damage);
        Health -= damage;
        float calculHp = Health + InGameManager.Instance.playerUpgrader.Hp;
        float calculMaxHp = MaxHealth + InGameManager.Instance.playerUpgrader.Hp;
        if (calculHp <= 0)
        {
            State = StateEnum.Death;
        }

        UpdateHpBar(calculHp / calculMaxHp);
    }

    public float GetCalCulatedAtk()
    {
        float res = Atk + InGameManager.Instance.playerUpgrader.Atk;

        return res;
    }

    float detectRadius = 2f; //

    public void Moving()
    {
        playerObject.transform.position += moveDir * moveSpeed * Time.deltaTime;
        // transform.parent.position += moveDir * moveSpeed * Time.deltaTime;
    }
    
    public void Detect() // PlayerDetectHandler
    {
        State = StateEnum.Detect;
        
        // Debug.Log("Detect!");
        detectRadius = 3f;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectRadius, Layers.Enemy);
        while (enemies.Length == 0)
        {
            if (detectRadius > 10f)
                break;
            detectRadius += 2f;
            enemies = Physics2D.OverlapCircleAll(transform.position, detectRadius, Layers.Enemy);
        }

        // Debug.Log("destance: " + detectRadius + ", dir: " + moveDir);
        if (enemies.Length == 0)
        {
            // Debug.Log("No enemies");
            StartCoroutine(nameof(CoDetect), detectSpeed);
            // yield return new WaitForSeconds(detectSpeed);
            // State = StateEnum.Idle; // Re Detect
        }
        else // set dir
        {
            // Debug.Log($"Find Enemy! {enemies.Length}");
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
    private IEnumerator CoDetect(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        State = StateEnum.Idle; // Re Detect
    }

    private void Attack(Collider2D other)
    {
        State = StateEnum.Attack;
        Bullet b = Instantiate<Bullet>(bullet, transform.position, Quaternion.identity);
        b.Initialize(other, GetCalCulatedAtk());

        StartCoroutine(nameof(CoAttack), AttackSpeed);
    }

    private IEnumerator CoAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        State = StateEnum.Idle;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }

    public void UpdateHpBar(float value)
    {
        _stateUIHandler.SetHpBar(value);
    }

    public void UpdateHpBar()
    {
        float calculHp = Health + InGameManager.Instance.playerUpgrader.Hp;
        float calculMaxHp = MaxHealth + InGameManager.Instance.playerUpgrader.Hp;
        _stateUIHandler.SetHpBar(calculHp / calculMaxHp);
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
