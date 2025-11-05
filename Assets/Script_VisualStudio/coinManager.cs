using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class coinManager : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    public Player_Movement playerMovement;

    public void UpdateCoinUI(int coin)
    {
        CoinText.text = coin.ToString();
        if (coin > 0)
        {
            playerMovement.ActivateDoubleJump();
        }

       
    }

    
    
}
