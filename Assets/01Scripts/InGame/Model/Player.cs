using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;
using InGame.Layers;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerViewModel playerViewModel;
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
    [SerializeField] private PlayerDetectHandler detectHandler;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private WeaponModelView bullet;
    [SerializeField] private float moveSpeed = 1f;
    private Vector3 moveDir;
    private float runnedTime;

    public StateEnum State { get; private set; } = StateEnum.Idle;
    public InGameManager.StateEnum GameState { get; private set; } = InGameManager.StateEnum.Running;
    public float Health { get; private set; } = 100f;
    public float MaxHealth { get; private set; } = 100f;

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
    
    private void Moving()
    {
        playerObject.transform.position += moveDir * (moveSpeed * Time.deltaTime);
        // transform.parent.position += moveDir * moveSpeed * Time.deltaTime;
    }
    
    float detectRadius = 2f; // for DetectGimo
    private void Detect() // PlayerDetectHandler
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
        var bMV = Instantiate(bullet, transform.position, Quaternion.identity);
        bMV.Initialize(other, GetCalCulatedAtk());

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

    public void TakeDamage(float damage)
    {
        playerViewModel.PresentDamageText(damage);
        Health -= damage;
        float calculHp = Health + InGameManager.Instance.PlayerUpgradeStat.Hp;
        float calculMaxHp = MaxHealth + InGameManager.Instance.PlayerUpgradeStat.Hp;
        if (calculHp <= 0)
        {
            State = StateEnum.Death;
        }

        playerViewModel.UpdateHpBar(calculHp / calculMaxHp);
    }

    public void HandleStateChange(InGameManager.StateEnum state)
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
    
    public bool HandleEnemyEnter(Collider2D other) // EnemyEnter Event! -> Attack!
    {
        StopCoroutine(nameof(CoDetect));
        switch (State)
        {
            case(StateEnum.Idle):
                Attack(other);
                return true;
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

    public float GetCalCulatedAtk()
    {
        float res = Atk + InGameManager.Instance.PlayerUpgradeStat.Atk;

        return res;
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

    public Vector3 GetPlayerPos()
    {
        return playerObject.transform.position;
    }
}
