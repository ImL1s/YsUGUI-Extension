/*
 * Author:ImL1s
 * Email:ImL1s@outlook.com
 * Description:
 */

using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Easy to UGUI alpha like NGUI
/// 讓UGUI的透明漸變也可以像NGUI一樣
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
public class TweenAlphaUGUI : MonoBehaviour
{
    #region inner enum

    public enum PlayMode
    {
        Once,Loop,PingPong
    }

    #endregion

    #region public field

    [SerializeField,Space(10)]
    public PlayMode playMode;

    [SerializeField, Space(15)]
    public float delayTime = 1;
    public float duration = 1;

    [SerializeField, Space(15)]
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));

    #endregion

    #region private field

    private Image image;
    private float lastTime;
    private float timer = 0;
    private float lerpTimer = 0;
    private bool toTop = true;
    private bool canLerp = true;

    #endregion

    #region public method

    /// <summary>
    /// Begin lerp
    /// </summary>
    public void Begin()
    {
        canLerp = true;
    }

    /// <summary>
    /// Stop lerp
    /// </summary>
    public void Stop()
    {
        canLerp = false;
    }

    /// <summary>
    /// End lerp immediately.
    /// </summary>
    public void EndImmediately()
    {
        canLerp = false;
        Debug.Log("EndImmediately" + this.gameObject.name + curve[curve.length - 1].value);
        image.color = new Color(1, 1, 1, curve.Evaluate(curve[curve.length - 1].value));
        print(image.color.ToString());
        this.enabled = false;

    }

    #endregion

    #region private method

    void Awake ()
    {
        if (GetComponent<Image>() != null) image = GetComponent<Image>();
        else Debug.LogError(this.gameObject.name + ": doesn't have a Image component!! Add image coponent or remove TweenAlphaUGUI!"); 
	}

    void Start()
    {
        try
        {
            if (canLerp)
            {
                float alphaColor = curve.keys[0].value;

                image.color = new Color(image.color.r, image.color.g, image.color.b, alphaColor);
                lastTime = curve.keys[curve.keys.Length - 1].time;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Gameobject name:" + this.gameObject.name + " has Error:" + e.ToString() + ",Please check curve setting.");
        }
    }
	
	
	void Update ()
    {
        if (!canLerp) return;

        if (timer >= delayTime)
        {
            Lerp();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    /// <summary>
    /// Lerp the image with playMode.
    /// 依照漸變模式漸變Image組件.
    /// </summary>
    private void Lerp()
    {
        switch (playMode)
        {
            case PlayMode.Once:

                lerpTimer += Time.deltaTime;
                image.color = new Color(1, 1, 1, curve.Evaluate(lerpTimer / duration));

                break;

            case PlayMode.Loop:

                lerpTimer += Time.deltaTime;
                image.color = new Color(1, 1, 1, curve.Evaluate(lerpTimer / duration));
                if (lerpTimer >= duration) lerpTimer = 0;

                break;

            case PlayMode.PingPong:

                if (toTop)
                {
                    lerpTimer += Time.deltaTime;
                    if(lerpTimer >= duration) toTop = false;
                }
                else
                {
                    lerpTimer -= Time.deltaTime;
                    if (lerpTimer <= float.Epsilon) toTop = true;
                }

                image.color = new Color(1, 1, 1, curve.Evaluate(lerpTimer / duration));
                

                break;
                
            default:
                Debug.LogError("Error:Fatal Exception!!A  enum in PlayMode!!;PlayMode:一個不存在的enum值");
                break;
        }
    }

    #endregion
}
