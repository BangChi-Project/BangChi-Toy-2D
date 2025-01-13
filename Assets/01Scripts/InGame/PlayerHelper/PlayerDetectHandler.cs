using UnityEngine;
using InGame.Layers;

public class PlayerDetectHandler : MonoBehaviour
{
    [SerializeField] private Player player;
    public void Detect()
    {
        Debug.Log("Detect ");
        float detectRadius = 1.5f;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectRadius, Layers.Enemy);
        while (enemies.Length != 0)
        {
            if (detectRadius > 15f)
                break;
            detectRadius += 1.5f;
            enemies = Physics2D.OverlapCircleAll(transform.position, detectRadius);
        }

        if (enemies.Length != 0) // set dir
        {
            Debug.Log($"Find Enemy! {enemies.Length}");
            foreach (var enemy in enemies)
            {
                
            }
        }

        // player.State = Player.StateEnum.Moving;
    }
}
