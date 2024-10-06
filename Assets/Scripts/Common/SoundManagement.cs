using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public static SoundManagement Instance;

    public AudioSource thisSource;
    public AudioSource audioSource;

    public AudioClip CoinCollected;     // 코인 획득 시
    public AudioClip Death;             // 죽을 경우
    public AudioClip GameTheme;         // 게임 테마곡 (BGM)
    public AudioClip NextLevel;         // 다음 스테이지 진입 시
    public AudioClip SFX;               // 각종 특수상황 효과음

    private readonly string path_coin = "01. Coin Collected";
    private readonly string path_death = "02. Death";
    private readonly string path_gameTheme = "03. Game Theme";
    private readonly string path_nextLevel = "04. Next Level";
    private readonly string path_sfx = "05. SFX";

    void Awake()
    {
        Instance = this;

        TryGetComponent(out thisSource);
        thisSource.playOnAwake = false;

        GameObject child = Instantiate(new GameObject("sound"), transform);
        child.AddComponent<AudioSource>();
        child.TryGetComponent(out audioSource);
        audioSource.playOnAwake = false;

        CoinCollected = Resources.Load<AudioClip>(path_coin);
        Death = Resources.Load<AudioClip>(path_death);
        GameTheme = Resources.Load<AudioClip>(path_gameTheme);
        NextLevel = Resources.Load<AudioClip>(path_nextLevel);
        SFX = Resources.Load<AudioClip>(path_sfx);
    }

    void Start()
    {
        PlayMusic(GameTheme);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (thisSource.isPlaying)
            thisSource.Stop();
        thisSource.clip = clip;
        thisSource.loop = true;
        thisSource.Play();
    }

    private void PlaySound(AudioClip clip, float startOffsetTime)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = clip;
        audioSource.time = startOffsetTime;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void StartMusic() => PlayMusic(GameTheme);

    public void PlayCoinCollected() => PlaySound(CoinCollected, 0.2f);
    public void PlayDeath() => PlaySound(Death, 0.15f);
    public void PlayNextLevel() => PlaySound(NextLevel, 0.1f);
    public void PlaySFX() => PlaySound(SFX, 0.1f);
}
