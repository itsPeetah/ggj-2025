using UnityEngine;

[CreateAssetMenu(fileName = "SoundtrackSong", menuName = "Scriptable Objects/SoundtrackSong")]
public class SoundtrackSong : ScriptableObject
{
    public AudioClip fullPiece;
    // public AudioClip loopSegment;
    public float loopTime;
}
