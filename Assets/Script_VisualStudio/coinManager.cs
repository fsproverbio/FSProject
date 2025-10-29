using TMPro;
using UnityEngine;

public class coinManager : MonoBehaviour
{
    public TextMeshProUGUI CoinText;

    public void UpdateCoinUI(int coin)
    {
        CoinText.text = coin.ToString();

    }

}
