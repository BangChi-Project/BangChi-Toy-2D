using UnityEngine;

public class PlayerViewModel: MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ObjectView objectView;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InGameManager.Instance.OnStateChange += OnStateChange;
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
        Debug.Log($"OnStateChange::playerState:{player.State}");
        player.HandleStateChange(state);
    }

    public void TakeDamage(float damage)
    {
        player.TakeDamage(damage);
    }

    public void UpdateHpBar(float value) // view
    {
        objectView.SetHpBar(value);
    }

    public void UpdateHpBar() // view
    {
        float calculHp = player.Health + InGameManager.Instance.playerUpgrader.Hp;
        float calculMaxHp = player.MaxHealth + InGameManager.Instance.playerUpgrader.Hp;
        objectView.SetHpBar(calculHp / calculMaxHp);
    }

    public void PresentDamageText(float damage)
    {
        objectView.PresentDamageText(damage);
    }

    public Vector3 GetPlayerPos()
    {
        return player.GetPlayerPos(); //////////////////////
    }
}