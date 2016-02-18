/*
 * Author:ImL1s
 *
 * Email:ImL1s@outlook.com
 *
 * Date:2016/02/19
 *
 * Description: Use for easy play sound on button. 讓你簡易又方便點擊播放音效.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundUGUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip buttonSound = null;

    private AudioListener listener;
    private AudioSource source;

    void Start()
    {
        if (buttonSound == null) StartCoroutine(LoadSound());
        listener = FindObjectOfType<AudioListener>();
    }

    /// <summary>
    /// Set button sound. 設定按鈕音效.
    /// </summary>
    /// <param name="clip"></param>
    public void SetAudioClip(AudioClip clip)
    {
        this.buttonSound = clip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound();
    }
    
    /// <summary>
    /// Play button sound. 播放按鈕音效.
    /// </summary>
    private void PlaySound()
    {
        if (listener == null || source == null)
        {
            GameObject go = FindObjectOfType<AudioListener>().gameObject;

            if (go == null)
            {
                go = GameObject.Find("GlobalAudio");
                if(go == null) listener = new GameObject("GlobalAudio").AddComponent<AudioListener>();
                source = listener.gameObject.AddComponent<AudioSource>();
            }
            else
            {
                listener = go.GetOrAddComponentEX<AudioListener>();
                source = go.GetOrAddComponentEX<AudioSource>();
            }

            source.clip = buttonSound;
            source.playOnAwake = false;
            source.loop = false;
        }

        source.Play();
    }

    private IEnumerator LoadSound()
    {
        WWW www = new WWW("File://" + Application.streamingAssetsPath + "/Audio/buttonSound.ogg");
        yield return www;

        //buttonSound = www.audioClip;
        buttonSound = www.GetAudioClip(false);

        www.Dispose();
    }
}
