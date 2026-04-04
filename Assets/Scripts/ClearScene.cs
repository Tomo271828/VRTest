using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class ClearScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI timeTMP;
    [SerializeField] TextMeshProUGUI timeSumTMP;
    [SerializeField] TextMeshProUGUI exitTMP;
    [SerializeField] TextMeshProUGUI continueTMP;
    [SerializeField] Expand left;
    [SerializeField] Expand right;
    [SerializeField] Charge charge;
    [SerializeField] Move move;
    float time;
    float timeSum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charge.chargable = false;
        move.stop = true;
        title.text = Goal.clearedScene + " Clear!!!";
        exitTMP.maxVisibleCharacters = 0;
        continueTMP.maxVisibleCharacters = 0;
        time = Goal.clearTime;
        timeSum = Goal.clearTimeSum;
        StartCoroutine(Roulette());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    string FormatTime(float t)
    {
        int m = (int)(t / 60f);
        int s = (int)(t % 60f);
        int f = (int)((t - MathF.Floor(t)) * 100f);
        return m.ToString("00") + ":" + s.ToString("00") + ":" + f.ToString("00");
    }
    IEnumerator Roulette()
    {
        for(int i = 1;i <= 120; i++)
        {
            int m1 = UnityEngine.Random.Range(0, 61);
            int m2 = UnityEngine.Random.Range(0, 61);
            int s1 = UnityEngine.Random.Range(0, 61);
            int s2 = UnityEngine.Random.Range(0, 61);
            int f1 = UnityEngine.Random.Range(0, 100);
            int f2 = UnityEngine.Random.Range(0, 100);
            timeTMP.text = m1.ToString("00") + ":" + s1.ToString("00") + ":" + f1.ToString("00");
            timeSumTMP.text = m2.ToString("00") + ":" + s2.ToString("00") + ":" + f2.ToString("00");
            yield return new WaitForSeconds(0.025f);
        }
        timeTMP.text = FormatTime(time);
        timeSumTMP.text = FormatTime(timeSum);
        yield return new WaitForSeconds(1.0f);
        charge.chargable = true;
        left.started = true;
        right.started = true;
        for(int i = 1;i <= 4; i++)
        {
            exitTMP.maxVisibleCharacters = i;
            continueTMP.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
