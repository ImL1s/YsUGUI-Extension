using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundUGUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip buttonSound = null;

    void Start()
    {
        if (buttonSound == null) StartCoroutine(LoadSound());
    }

    public void SetAudioClip(AudioClip clip)
    {
        this.buttonSound = clip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSource.PlayClipAtPoint(buttonSound, Vector3.zero);
    }


    private IEnumerator LoadSound()
    {
        WWW www = new WWW("File://" + Application.streamingAssetsPath + "/Audio/buttonSound.ogg");
        yield return www;

        buttonSound = www.audioClip;

        www.Dispose();
    }
}
