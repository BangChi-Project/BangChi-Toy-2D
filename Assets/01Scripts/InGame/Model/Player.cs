using System.Collections;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
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

    public bool isIdle;
    public bool isMoving;
    public bool isLeft;
    public bool isRight;
    
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
                    if (!isIdle)
                    {
                        isIdle = true;
                        isMoving = false;
                        playerViewModel.SetAnimState("Idle");
                    }
                    break;
                
                case (StateEnum.Moving):
                    if (!isMoving)
                    {
                        isMoving = true;
                        isIdle = false;
                        playerViewModel.SetAnimState("Run");
                    }

                    Moving();
                    break;
                
                case (StateEnum.Death):
                    InGameManager.Instance.SetGameState(InGameManager.StateEnum.End);
                    Death();
                    break;
                default:
                    // playerViewModel.SetAnimState("Ready");
                    break;
            }
        }
    }
    
    private void Moving()
    {
        if (moveDir.x > 0)
        {
            if (!isRight)
            {
                isRight = true;
                isLeft = false;
                // playerViewModel.SetDir("Right");
                playerViewModel.SetAnimTurn(1f);
            }
        }
        else // dir.x < 0
        {
            if (!isLeft)
            {
                isLeft = true;
                isRight = false;
                // playerViewModel.SetDir("Left");
                playerViewModel.SetAnimTurn(-1f);
            }
        }
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

        float dir = other.transform.position.x - transform.position.x;
        playerViewModel.SetAnimTurn(dir);
        playerViewModel.SetAnimState("Fire");
        
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
        float calculHp = Health + PlayerUpgradeStat.Instance.InGameHp;
        float calculMaxHp = MaxHealth + PlayerUpgradeStat.Instance.InGameHp;
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
        float res = Atk + PlayerUpgradeStat.Instance.InGameAtk;

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
    
    // public static class AnimParam
    // {
    //     // Animator 파라미터 "Walk"를 정수 해시값으로 미리 변환
    //     public static readonly int Walk = Animator.StringToHash("Walk");
    //
    //     public static readonly int Fire = Animator.StringToHash("Fire");
    //     public static readonly int Idle = Animator.StringToHash("Idle");
    // }
}
