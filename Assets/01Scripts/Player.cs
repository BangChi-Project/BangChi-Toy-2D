using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(Collider2D other)
    {
        Instantiate(bullet, transform.position, Quaternion.identity).SetTarget(other);
    }
}
