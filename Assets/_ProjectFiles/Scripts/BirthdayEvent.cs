using UnityEngine;
using UnityEngine.Video;

public class BirthdayEvent : MonoBehaviour
{
    private AudioSource source;
    public VideoPlayer Player;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlayVideo()
    {
        Player.Play();
    }
}
