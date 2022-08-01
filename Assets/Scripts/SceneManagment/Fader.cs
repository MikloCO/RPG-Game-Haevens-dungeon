using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        GameObject FaderImage;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            FaderImage = gameObject.transform.GetChild(0).gameObject;
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null; // Run on next frame
            }
        }

        public IEnumerator FadeIn(float time)
        {
            if (canvasGroup != null)
            {
                while (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= Time.deltaTime / time;
                    yield return null; // Run on next frame
                }
            }
        }

        public void DisableFader()
        {
            if(FaderImage != null)
                FaderImage.SetActive(false);
        }
        public void EnableFader()
        {
            if (FaderImage != null)
                FaderImage.SetActive(true);
        }
    }
}
