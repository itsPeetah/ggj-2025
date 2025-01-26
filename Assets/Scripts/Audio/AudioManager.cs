using SPNK.Game.Events;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Component")]
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    private int currentSfxSource;

    [Header("listent to")]
    public AudioClipEventChannelSO musicChannel;
    public AudioClipEventChannelSO sfxChannel;

    private void OnEnable()
    {
        musicChannel.OnEventRaised += HandleMusicChange;
        sfxChannel.OnEventRaised += HandleSfxPlay;
    }

    private void OnDisable()
    {
        musicChannel.OnEventRaised -= HandleMusicChange;
        sfxChannel.OnEventRaised -= HandleSfxPlay;
    }

    private void HandleMusicChange(AudioClip clip)
    {
        if (musicSource.isPlaying)
            musicSource.Stop();

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void HandleSfxPlay(AudioClip clip)
    {
        AudioSource s = sfxSources[currentSfxSource];
        s.pitch = Random.Range(0.85f, 1.15f);
        s.PlayOneShot(clip);
        currentSfxSource = (currentSfxSource + 1) % sfxSources.Length;
    }
}
