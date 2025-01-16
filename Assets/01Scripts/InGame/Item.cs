using UnityEngine;

public class Item : MonoBehaviour
{
    public int IdNumber { get; private set; } // DB Item CodeNumber
    public string Name { get; private set; }
    public int Amount { get; private set; }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public void Initialize(int idNumber, string name, int amount)
    {
        IdNumber = idNumber;
        Name = name;
        Amount = amount;
    }
}
