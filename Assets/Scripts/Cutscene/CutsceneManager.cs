using System.Collections;
using SPNK.Game.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [System.Serializable]
    public struct CutsceneSlide
    {
        public Sprite sprite;
        public float duration;
    }

    [Header("Components")]
    public Image slideShowScreen;

    [Header("Data")]
    public float startAfter = 1f;
    public CutsceneSlide[] slides;
    public string sceneToLoad = "";

    [Header("Broadcast to")]
    public VoidEventChannelSO onStart;
    public VoidEventChannelSO onCutsceneStart;
    public VoidEventChannelSO onNextSlide;

    private void Start()
    {
        slideShowScreen.enabled = false;

        onStart.RaiseEvent();
        StartCoroutine(DoSlideshow());
        onCutsceneStart.RaiseEvent();
    }


    private IEnumerator DoSlideshow()
    {
        yield return new WaitForSeconds(startAfter);
        for (int i = 0; i < slides.Length; i++)
        {
            yield return DoSlide(slides[i]);
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator DoSlide(CutsceneSlide slide)
    {
        slideShowScreen.sprite = slide.sprite;
        slideShowScreen.enabled = true;
        onNextSlide.RaiseEvent();
        yield return new WaitForSeconds(slide.duration);
    }

}
