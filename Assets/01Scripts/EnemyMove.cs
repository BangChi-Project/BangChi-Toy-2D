using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("hi")]
    [Tooltip("explain")]
    private Rigidbody2D _rb;
    
    [SerializeField]
    private float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = InGameManager.Instance.GetPlayerPos();

        Vector3 dir = (playerPos - transform.position).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;
        // _rb.AddForce(dir * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
}
