using UnityEngine;

public class GameSoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip fireSound;
    public AudioClip hitSound;
    public AudioClip getGemSound;
    public AudioClip loseGemSound;

    private static GameSoundManager m_instance = null;
    public static GameSoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = Instantiate(Resources.Load<GameSoundManager>("GameSoundManager"));
            }
            return m_instance;
        }
    }

    public void Awake()
    {
        m_instance = this;
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayFire()
    {
        audioSource.PlayOneShot(fireSound);

    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public void PlayGetGem()
    {
        audioSource.PlayOneShot(getGemSound);
    }

    public void PlayLoseGem()
    {

    }

}
