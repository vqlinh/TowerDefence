using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip Bg;
    public AudioClip standard;
    public AudioClip missile;
    public AudioClip enemyRun;
    public AudioClip enemyDead;


    public static bool GameIsOver ;
    public  GameObject gameOverUi;
    public  GameObject completeLevelUi;

    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance!=null && instance!=this) Destroy(this);
        else instance = this;

    }
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        GameIsOver = false;
        AudioBg();
    }


    private void Update()
    {
        if (GameIsOver) return; // làm cho hàm endgame chỉ chạy 1 lần
        if (PlayerStats.lives <= 0) EndGame();
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUi.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUi.SetActive(true);
    }

    public void AudioBg()
    {
        if (audioSource != null) audioSource.PlayOneShot(Bg);
        Debug.Log("AudioShoot");
    } 

    public void AudioStandard()
    {
        if (audioSource != null) audioSource.PlayOneShot(standard);
        Debug.Log("AudioStandard");
    }  

    public void AudioMissile()
    {
        if (audioSource != null) audioSource.PlayOneShot(missile);
        Debug.Log("AudioMissile");
    } 

    public void AudioEnemyRun()
    {
        if (audioSource != null) audioSource.PlayOneShot(enemyRun);
        Debug.Log("AudioEnemyRun");
    }  

    public void AudioEnemyDie()
    {
        if (audioSource != null) audioSource.PlayOneShot(enemyDead);
        Debug.Log("AudioEnemyDie");
    }

}
