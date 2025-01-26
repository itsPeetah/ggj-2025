using System.Collections;
using SPNK.Game.Events;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Component")]
    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    private int currentSfxSource;

    [Header("listent to")]
    public SoundtrackSongEventChannelSO musicChannel;
    public AudioClipEventChannelSO sfxChannel;

    private SoundtrackSong currentSoundtrack;
    private Coroutine loopCoroutine;

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

    private void HandleMusicChange(SoundtrackSong soundtrack)
    {
        currentSoundtrack = soundtrack;
        if (!currentSoundtrack) return;
        if (loopCoroutine != null) StopCoroutine(loopCoroutine);
        musicSource.Stop();
        loopCoroutine = StartCoroutine(DoLoopSoundtrack());
    }

    private void HandleSfxPlay(AudioClip clip)
    {
        AudioSource s = sfxSources[currentSfxSource];
        s.pitch = Random.Range(0.85f, 1.15f);
        s.PlayOneShot(clip);
        currentSfxSource = (currentSfxSource + 1) % sfxSources.Length;
    }

    private IEnumerator DoLoopSoundtrack()
    {
        musicSource.clip = currentSoundtrack.fullPiece;
        musicSource.loop = true;
        musicSource.Play();
        float previousTime = 0;
        while (true)
        {
            if (musicSource.time < previousTime)
            {
                Debug.Log("Looping!!");
                musicSource.time = currentSoundtrack.loopTime;
            }

            Debug.Log($"Song at {musicSource.time / currentSoundtrack.fullPiece.length}");

            previousTime = musicSource.time;
            yield return null;
        }
    }
}
