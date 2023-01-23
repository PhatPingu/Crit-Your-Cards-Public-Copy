using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer iceIceLogo;
    [SerializeField] TextMeshProUGUI disclaimer01;
    [SerializeField] TextMeshProUGUI disclaimer02;

    void Update()
    {
        FadeIn();
        Invoke("FadeOut", 7f);
        Invoke("ChangeScene", 10f);
    }

    float count = 0;
    void FadeIn()
    {
        if(count < 1)
        {
            iceIceLogo.color = new Color(iceIceLogo.color.r, iceIceLogo.color.g, iceIceLogo.color.b, iceIceLogo.color.a + 0.005f);
            count += 0.005f;
        }
    }

    void FadeOut()
    {
        iceIceLogo.color = new Color(iceIceLogo.color.r, iceIceLogo.color.g, iceIceLogo.color.b, iceIceLogo.color.a - 0.005f);
        disclaimer01.color = new Color(disclaimer01.color.r, disclaimer01.color.g, disclaimer01.color.b, disclaimer01.color.a - 0.005f);
        disclaimer02.color = new Color(disclaimer02.color.r, disclaimer02.color.g, disclaimer02.color.b, disclaimer02.color.a - 0.005f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

}
