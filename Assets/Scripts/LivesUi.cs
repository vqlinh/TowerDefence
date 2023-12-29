using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesUi : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    private void Update()
    {
        livesText.text = PlayerStats.lives.ToString() + "LIVES";
    }
}
