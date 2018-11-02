using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour {

    [Header("Canvas Elements")]
    [SerializeField] private Text m_leftHelperText;
    [SerializeField] private Text m_rightHelperText;
    [SerializeField] private Text m_articleText;
	[SerializeField] private Text m_timerText;
    [SerializeField] private Image m_articleBackground;
    [SerializeField] private Image m_backgroundFade;
    [SerializeField] private Image m_blackScreen;

    [Header("Black screen variables")]
    [SerializeField] private float m_fadeTime = 1.0f;

    private void Awake()
    {
        m_leftHelperText.text = "";
        m_rightHelperText.text = "";
		m_timerText.text = "";
        m_articleBackground.gameObject.SetActive(false);
        m_backgroundFade.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(FadeBlackScreenOut());
    }

    public void SetLeftHelperText(string _text)
    {
        m_leftHelperText.text = _text;
    }

    public void SetRightHelperText(string _text)
    {
        m_rightHelperText.text = _text;
    }

    public void StartReadingPaper(Text _textElement, Sprite _background = null)
    {
        m_articleBackground.gameObject.SetActive(true);
        m_backgroundFade.gameObject.SetActive(true);

        if (_background)
            m_articleBackground.sprite = _background;

        m_articleText.text = _textElement.text;
        m_articleText.font = _textElement.font;
    }

    public void StopReadingPaper()
    {
        m_articleBackground.gameObject.SetActive(false);
        m_backgroundFade.gameObject.SetActive(false);
    }

    public void StartViewingImage(Sprite _image)
    {
        m_articleText.text = "";
        m_articleBackground.sprite = _image;
        m_articleBackground.gameObject.SetActive(true);
        m_backgroundFade.gameObject.SetActive(true);
    }

    public void OnPlayerDeath()
    {
        // Completely black
        m_blackScreen.color = new Color(m_blackScreen.color.r, m_blackScreen.color.g, m_blackScreen.color.b, 1);
        StartCoroutine(PlayerDeathAnimation());
    }

    public void InstantBlackScreen()
    {
        // Completely black
        m_blackScreen.color = new Color(m_blackScreen.color.r, m_blackScreen.color.g, m_blackScreen.color.b, 1);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeBlackScreenIn());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeBlackScreenOut());
    }

	public void SetTimerText(string _text)
	{
		m_timerText.text = _text;
	}

    IEnumerator PlayerDeathAnimation()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeBlackScreenOut());
    }

    IEnumerator FadeBlackScreenIn()
    {
        m_blackScreen.color = new Color(m_blackScreen.color.r, m_blackScreen.color.g, m_blackScreen.color.b, 0);

        while (m_blackScreen.color.a < 1)
        {
            m_blackScreen.color = new Color(m_blackScreen.color.r, m_blackScreen.color.g, m_blackScreen.color.b, 
                m_blackScreen.color.a + Time.deltaTime / m_fadeTime);
            yield return null;
        }
        m_blackScreen.color = new Color(m_blackScreen.color.r, m_blackScreen.color.g, m_blackScreen.color.b, 1);
        yield return null;
    }

    IEnumerator FadeBlackScreenOut()
    {
        m_blackScreen.color = new Color(m_blackScreen.color.r, m_blackScreen.color.g, m_blackScreen.color.b, 1);

        while (m_blackScreen.color.a > 0)
        {
            m_blackScreen.color = new Color(m_blackScreen.color.r, m_blackScreen.color.g, m_blackScreen.color.b,
                m_blackScreen.color.a - Time.deltaTime / m_fadeTime);
            yield return null;
        }
        m_blackScreen.color = new Color(m_blackScreen.color.r, m_blackScreen.color.g, m_blackScreen.color.b, 0);
        yield return null;
    }
}
