using UnityEngine;

public class playerInventory : MonoBehaviour
{

    public coinManager coinManager;


    int coins = 0;

    public void AddCoins(int amount)
    {
        coins += amount;
        coinManager.UpdateCoinUI(coins);
    }
}
