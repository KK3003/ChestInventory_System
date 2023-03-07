using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : MonoGenericSingleton<PlayerItems>
{
    [SerializeField] private int coinsInInventory;
    [SerializeField] private int gemsInInventory;
    [SerializeField] private Text coinText;
    [SerializeField] private Text gemsText;

    private void Start()
    {
        DisplayPlayerInventory();
    }

    private void DisplayPlayerInventory()
    {
        coinText.text = coinsInInventory.ToString();
        gemsText.text = gemsInInventory.ToString();
    }

    public void UpdatePlayerInventory(int coins, int gems)
    {
        this.coinsInInventory += coins;
        gemsInInventory += gems;
        DisplayPlayerInventory();
    }
        
    public bool ReduceGems(int requiredGems)
    {
        if (requiredGems <= gemsInInventory)
        {
            gemsInInventory -= requiredGems;
            DisplayPlayerInventory();
            return true;
        }
        return false;
    }
}
