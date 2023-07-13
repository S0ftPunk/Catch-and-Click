using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    private int scores = 0;//
    private int bestScores;

    private bool inZone = false;
    private bool canClick = true;
    private bool sessionStarted = false;

    private bool dalayedIsForPromo = true;

    [SerializeField] private ColorZoneManager colorZone;

    private float delayFoReboot = 0.1f;
    private float ballSpeed = 0.9f; //

    [SerializeField] private Animator animator;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text complimentText;
    [SerializeField] private Text bestScoresText;

    [SerializeField] private List<string> compliments;
    [SerializeField] private UserAgent user;

    private Tweener textTweenPos, textTweenFade;

    [SerializeField] private Transform startPosText;
    [SerializeField]  private Transform leftPos, rightPos, posForPanel;
    [SerializeField] private GameObject bestPanel;

    [SerializeField] private Color textColor;

    [SerializeField] private AudioClip clickClip, loseClick;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private RewardManager rewardManager;
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        if (PlayerPrefs.HasKey("best"))
        {
            bestScores = PlayerPrefs.GetInt("best");
        }
        else
        {
            bestScores = 0;
            PlayerPrefs.SetInt("best",bestScores);
        }
        bestScoresText.text = bestScores.ToString();
        animator.speed = ballSpeed;
    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if (sessionStarted)
                {
                    if (canClick)
                        Click();
                }
                else
                {
                    StartSession();
                }
            }
        }
        else if (Input.anyKeyDown)
        {
            if (sessionStarted)
            {
                if (canClick)
                    Click();
            }
            else
            {
                StartSession();
            }
        }
    }
    private void StartSession()
    {
        StartCoroutine(StartAnimation());
    }
    private void Click()
    {
        if (inZone)
        {
            scores++;
            audioSource.PlayOneShot(clickClip,1);
            Progress(scores);
            StartCoroutine(DelayForRestart(true));
            ComplimentsShowing();
            PrefRegister(scores);
            //dalayedIsForPromo = false;
        }
        else
        {
            if (scores == bestScores && bestScores != 0)
            {
                rewardManager.ScoresSet(bestScores);
            }
            scores = 0;
            audioSource.PlayOneShot(loseClick, 0.5f);
            StartCoroutine(DelayForRestart(false));
            ballSpeed = 1;
            animator.speed = ballSpeed;
            rewardManager.AddShow();
        }
        scoreText.text = scores.ToString();
        colorZone.KillTweens();
    }
    private void PrefRegister(int scores)
    {
        if(bestScores < scores)
        {
            bestScores = scores;
            PlayerPrefs.SetInt("best", bestScores);
            bestScoresText.text = bestScores.ToString();
        }
    }
    private void Progress(int scores)
    {
        if(scores > 30)
        {
            SpeedUp();
        }
        
    }
    private void SpeedUp()
    {
        if(scores % 10 == 0)
        {
            ballSpeed += 0.05f;
            animator.speed = ballSpeed;
        }
    }
    private void ComplimentsShowing()
    {
        if(compliments != null)
        {
            if (compliments.Count != 0)
            {
                int index = Random.Range(0, compliments.Count);
                complimentText.text = compliments[index];
                compliments.RemoveAt(index);
                TextAnimate();
            }
            else
            {
                ListFill();
                ComplimentsShowing();
            }
        }
        else
        {
            ListFill();
        }
    }
    private void TextAnimate()
    {
        float xPos = Random.Range(leftPos.position.x, rightPos.position.x);
        complimentText.transform.position = new Vector3(xPos, startPosText.position.y, 0);
        complimentText.color = textColor;
        if (textTweenFade != null)
        {
            textTweenFade.Kill();
            textTweenPos.Kill();
        }

        float duration = 2.75f;
        textTweenPos = complimentText.transform.DOMoveY(startPosText.position.y + 2, duration);
        textTweenFade = complimentText.DOFade(0, duration);
    }
    private void ListFill()
    {
        foreach(string compliment in user.internationalCompliments)
        {
            compliments.Add(compliment);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<ColorZoneManager>())
        {
            inZone = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<ColorZoneManager>())
        {
            inZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<ColorZoneManager>())
        {
            inZone = false;
        }
    }
    IEnumerator DelayForRestart(bool isSuccess)
    {
        canClick = false;
        if (isSuccess)
        {
            Camera.main.DOColor(new Color32(77, 255, 129, 255), delayFoReboot);
            colorZone.RandomizeValues(scores);
        }
        else
        {
            Camera.main.DOColor(new Color32(255, 0, 105, 255), delayFoReboot);
            colorZone.RandomizeValues(scores, true);
        }
        yield return new WaitForSeconds(0.1f);
        Camera.main.backgroundColor = Color.white;
        canClick = true;
        //
        //yield return new WaitForSeconds(0.2f);
        //dalayedIsForPromo = true;
    }
    IEnumerator StartAnimation()
    {
        Tweener tween = Camera.main.DOOrthoSize(5, 0.2f);
        animator.enabled = true;
        colorZone.RandomizeValues(0);
        scoreText.text = scores.ToString();
        yield return tween.WaitForCompletion();
        bestPanel.SetActive(true);
        sessionStarted = true;
        colorZone.gameObject.SetActive(true);
        tween = bestPanel.transform.DOMoveY(posForPanel.position.y + 0.1f, 0.3f);
        yield return tween.WaitForCompletion();
        tween = bestPanel.transform.DOMoveY(posForPanel.position.y, 0.02f);
        yield return tween.WaitForCompletion();
    }
    //
    //IEnumerator PromoVideo()
    //{
    //    yield return new WaitForSeconds(0.05f);
    //    if(dalayedIsForPromo)
    //        Click();
    //}
}
