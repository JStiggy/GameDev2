using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class FadeSystem : MonoBehaviour
{
    public Texture2D fadeTexture;
    float fadeSpeed = 0.2f;
    int drawDepth = -1000;

    float alpha = 0;
    int fadeDir = 0;
    bool fading = false;

    public IEnumerator PerformFade(object[] values)
    {
        fading = true;
        fadeDir = (int)values[0];
        alpha = Mathf.Clamp01(1 - fadeDir);
        while (!(alpha >= 1 && fadeDir == 1) && !(alpha <= 0 && fadeDir == -1))
        {
            
            yield return null;
        }

        if (alpha >= 1 && fadeDir == 1 && values[1] != null)
        {
            SceneManager.LoadScene((string)values[1]);
            this.StopAllCoroutines();
        }
        else
        {
            fading = false;
        }
        yield return null;
    }

    public IEnumerator PerformSpriteFade(object[] vals)
    {
        SpriteRenderer sr = ((GameObject)vals[0]).GetComponent<SpriteRenderer>();
        float fade = (bool)vals[1] ? 0 : 1;
        float fSpeed = fadeSpeed;
        while (true)
        {
            if ((bool)vals[1])
            {
                //FadeIn
                fade = Mathf.SmoothDamp(fade, 1f, ref fSpeed, .2f);
                sr.color = new Color(1f, 1f, 1f, fade);
                if (fade >= .95)
                {
                    sr.color = new Color(1f, 1f, 1f, 1f);
                    break;
                }
            }
            else
            {
                fade = Mathf.SmoothDamp(fade, 0f, ref fSpeed, .2f);
                sr.color = new Color(1f, 1f, 1f, fade);
                if (fade <= 0.05)
                {
                    sr.color = new Color(1f, 1f, 1f, 0f);
                    break;
                }
                   
            }
            yield return null;
        }
        yield return null;
    }

    void OnGUI()
    { 
        if (fading == false)
            return;

        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        GUI.depth = drawDepth;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }
}