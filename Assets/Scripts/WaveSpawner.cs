using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public static int enemiesALives=0;
    private int wayIndex = 0;
    private float countdown = 2f;
    public Transform spawnPoint;
    public Wave[] waves;
    public float timeBetweenWave = 5f;
    public TextMeshProUGUI waveCountdownTimer;

    private void Update()
    {
        if (enemiesALives > 0) return;
        if (wayIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown,0f,Mathf.Infinity);
        waveCountdownTimer.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        // Kiểm tra xem wayIndex có vượt quá độ dài của mảng waves không
        if (wayIndex < waves.Length)
        {
            Wave wave = waves[wayIndex];

            enemiesALives = wave.count;
            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1 / wave.rate);
            }
            wayIndex++;
        }
        else
        {
            // Nếu wayIndex vượt quá độ dài của mảng waves, bạn có thể thực hiện xử lý tương ứng.
            Debug.LogWarning("Đã hoàn thành tất cả các làn sóng.");
            enemiesALives = 0;
        }
    }


    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
