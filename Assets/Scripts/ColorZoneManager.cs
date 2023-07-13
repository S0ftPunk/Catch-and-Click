using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorZoneManager : MonoBehaviour
{
    [SerializeField] private float durationScale;

    [SerializeField] Transform rectSprite;

    private int angle;
    private int moveAngle;

    private float scaleMinimizeDuration = 2f;
    private float rotationDuration = 1.5f;

    private Tweener scaleTween;
    private Tweener rotationTween;
    private Tweener startRotationTweener;
    private void Start()
    {
       // RandomizeValues(0);
    }
    public void RandomizeValues(int scores, bool isLooser = false)
    {
        
        int newAngle = RandomizeAngle();      
        float duration = Difference(newAngle) * durationScale / 360f;           
        startRotationTweener = transform.DORotate(Vector3.forward * angle, duration);
        if(isLooser)
            rectSprite.DOScaleX(8, duration);
        if (scores > 30)
        {
            float scale = ScaleRandomize();
            if(scale > 10)
            {
                rectSprite.DOScaleX(scale, duration).OnComplete(ScaleSmaller);
            }
            else
            {
                rectSprite.DOScaleX(scale, duration);
            }
            if(scores > 60)
            {
                int chance = Random.Range(0, 10);
                if (chance < 2)
                    StartCoroutine(RotateArea(angle));
            }
        }
    }
    private float ScaleRandomize()
    {    
        float chance = Random.Range(0, 21);
        float newScale;
        if(chance <= 5)
        {
            newScale = Random.Range(8f, 15.1f);
            if (chance == 0)
                newScale = 15;
        }
        else
        {
            newScale = Random.Range(4f, 8f);
        }
        return newScale;
    }
    private float Difference(int newAngle)
    {
        float difference = Mathf.Abs(newAngle - angle);
        float angleDifference = 180;
        if (difference > 180)
        {
            if (newAngle >= angle)
            {
                angleDifference = 360 - newAngle + angle;
            }
            else
            {
                angleDifference = 360 - angle + newAngle;
            }
        }
        else
            angleDifference = difference;
        if(angleDifference < 35)
        {
            angleDifference += 35;
            angle = newAngle + 35;
        }
        else
        {
            angle = newAngle;
        }
        return angleDifference;
    }
    private int RandomizeAngle()
    {
        int newAngle = Random.Range(0, 361);
        return newAngle;
    }
    private void ScaleSmaller()
    {
        scaleTween = rectSprite.DOScaleX(3, scaleMinimizeDuration);
    }
    private void StartAnimation()
    {
        rotationTween = transform.DORotate(Vector3.forward  * (angle + moveAngle), rotationDuration/2f).SetEase(Ease.Linear).OnComplete(ReverseAnimation);
    }

    private void ReverseAnimation()
    {
        rotationTween = transform.DORotate(Vector3.forward * (angle + (-moveAngle)), rotationDuration).SetEase(Ease.Linear).OnComplete(ReverseAnimation);
        moveAngle *= -1;
    }
    public void KillTweens()
    {
        if(scaleTween != null)
            scaleTween.Kill();
        if(rotationTween != null)
            rotationTween.Kill();
        scaleTween = null;
        rotationTween = null;
    }
    private IEnumerator RotateArea(int angle)
    {
        int chance = Random.Range(0, 15);
        int newAngle = 30;
        if(chance < 4)
        {
            if (angle < 300)
            {
                newAngle = 60;
            }
        }
        else if(chance < 10)
        {
            if (angle < 315)
            {
                newAngle = 45;
            }
        }
        else
        {
            if (angle < 280)
            {
                newAngle = 80;
            }
        }
        int direction = Random.Range(0, 1);
        if (direction == 0)
            newAngle *= -1;
        moveAngle = newAngle;
        yield return startRotationTweener.WaitForCompletion();
        StartAnimation();
    }
}
