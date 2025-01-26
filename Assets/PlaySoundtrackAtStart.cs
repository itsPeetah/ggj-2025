using SPNK.Game.Events;
using UnityEngine;

public class PlaySoundtrackAtStart : MonoBehaviour
{
    public SoundtrackSongEventChannelSO channel;
    public SoundtrackSong song;

    private void Start()
    {
        channel.RaiseEvent(song);
    }
}
