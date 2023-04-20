using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip deathSound;
    private AudioSource audioSource;
    private static BackgroundMusic instance = null;
    public static BackgroundMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
    public void PlayMusic()
    {
        audioSource.Play();
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }
    public void PlayLoseSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(loseSound);
    }
}















