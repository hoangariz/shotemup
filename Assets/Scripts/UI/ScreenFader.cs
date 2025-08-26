using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class ScreenFader : MonoBehaviour {
    public float duration = 0.35f;
    Image img;
    void Awake(){ img = GetComponent<Image>(); img.raycastTarget = true; img.color = new Color(0,0,0,1); }
    IEnumerator Start(){
        float t=0; while(t<duration){ t+=Time.deltaTime;
            float a = 1f - Mathf.Clamp01(t/duration);
            img.color = new Color(0,0,0,a); yield return null;
        }
        img.raycastTarget = false;
    }
}
