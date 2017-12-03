using UnityEngine;

public class GameSoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip jumpLandSound;
    public AudioClip fireSound;
    public AudioClip hitSound;
    public AudioClip getGemSound;
    public AudioClip loseGemSound;
    public AudioClip respawnSound;

    private static GameSoundManager m_instance = null;
    public static GameSoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                var prefab = Resources.Load<GameObject>("GameSoundManager");
                if (prefab == null) Debug.LogError("Can't load GameSoundManager from Resources");
                var instance = Instantiate(prefab);
                if (instance == null) Debug.LogError("Instance of GameSoundManager prefab is null");
                m_instance = instance.GetComponent<GameSoundManager>();
                if (m_instance == null) Debug.LogError("No GameSoundManager found on prefab instance.");
            }
            return m_instance;
        }
    }

    public void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            DestroyObject(gameObject);
            return;
        }
        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip)
    {
        if (clip != null) audioSource.PlayOneShot(clip);
    }

    public void PlayJump()
    {
        Play(jumpSound);
    }

    public void PlayJumpLand()
    {
        Play(jumpLandSound);
    }

    public void PlayFire()
    {
        Play(fireSound);

    }

    public void PlayHit()
    {
        Play(hitSound);
    }

    public void PlayGetGem()
    {
        Play(getGemSound);
    }

    public void PlayLoseGem()
    {
        Play(loseGemSound);
    }

    public void PlayRespawn()
    {
        Play(respawnSound);
    }

}
