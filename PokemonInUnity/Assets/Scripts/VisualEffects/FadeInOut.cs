using Assets.Scripts.Utilities.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{

    private Image img;
    private Text text;
    private SpriteRenderer spriteRenderer;

    public DeltaTimer fadeTimer;

    public double fadeTime = 2;

    /// <summary>
    /// The fully realized state of visibility.
    /// </summary>
    public Assets.Scripts.Enums.Visibility visibility;

    public enum FadeType
    {
        Fade,
        Blink
    }

    public FadeType fadeStyle;

    // Start is called before the first frame update
    void Start()
    {
        if (visibility == Assets.Scripts.Enums.Visibility.Visible)
        {
            Debug.Log("start 1");
            this.fadeTimer = new DeltaTimer(fadeTime, Assets.Scripts.Enums.TimerType.CountDown, false, fadeOut);
            this.fadeTimer.start();
        }
        else
        {
            Debug.Log("start 2");
            this.fadeTimer = new DeltaTimer(fadeTime, Assets.Scripts.Enums.TimerType.CountUp, false, fadeIn);
            this.fadeTimer.start();
        }
        bool valid = false;
        if (this.gameObject.GetComponent<Image>() != null)
        {
            this.img = this.gameObject.GetComponent<Image>();
            valid = true;
        }
        if (this.gameObject.GetComponent<Text>() != null)
        {
            this.text = this.gameObject.GetComponent<Text>();
            valid = true;
        }
        if (this.gameObject.GetComponent<SpriteRenderer>() != null)
        {
            this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            valid = true;
        }
        if (valid == false) throw new System.Exception("No valid color changing thing found on object: " + this.gameObject.name);
    }

    void fadeIn()
    {
            Debug.Log("Hello world");
            this.fadeTimer = new DeltaTimer(fadeTime, Assets.Scripts.Enums.TimerType.CountDown, false, fadeOut);
            this.fadeTimer.start();
            visibility = Assets.Scripts.Enums.Visibility.Visible;
            return;
        /*
        else if(visibility == Assets.Scripts.Enums.Visibility.Invisible)
        {
            Debug.Log("Reset?");
            this.fadeTimer = new DeltaTimer(fadeTime, Assets.Scripts.Enums.TimerType.CountDown, false, updateEnumState);
            this.fadeTimer.start();
            visibility = Assets.Scripts.Enums.Visibility.Visible;
            return;
        }
       */
    }

    void fadeOut()
    {
            Debug.Log("Reset?");
            this.fadeTimer = new DeltaTimer(fadeTime, Assets.Scripts.Enums.TimerType.CountUp, false, fadeIn);
            this.fadeTimer.start();
            visibility = Assets.Scripts.Enums.Visibility.Invisible;
            return;
    }

    // Update is called once per frame
    void Update()
    {
        this.fadeTimer.Update();
        updateVisibility();
    }

    void updateVisibility()
    {
        float alpha = getAlphaFromFade();
            if (img != null)
            {
                Color c = this.img.color;
                this.img.color = new Color(c.r, c.g, c.b, alpha);
            }
            if (text!=null)
            {
                Color c = this.text.color;
                this.text.color = new Color(c.r, c.g, c.b, alpha);
            }
            if (this.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                Color c = this.spriteRenderer.color;
                this.spriteRenderer.color = new Color(c.r, c.g, c.b, alpha);
            }
    }

    float getAlphaFromFade()
    {
        if(this.fadeStyle== FadeType.Blink)
        {
            if (this.visibility == Assets.Scripts.Enums.Visibility.Invisible) return 0f;
            else return 1f;
        }
        if(this.fadeStyle== FadeType.Fade)
        {
            return (float)(fadeTimer.currentTime / fadeTimer.maxTime);
        }
        return (float)(fadeTimer.currentTime / fadeTimer.maxTime);
    }
}
