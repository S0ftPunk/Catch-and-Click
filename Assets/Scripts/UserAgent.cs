using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UserAgent : MonoBehaviour
{
    //[DllImport("__Internal")]
    //private static extern bool GetUserDevice();

    [DllImport("__Internal")]
    private static extern string GetLang();

    public string language;

    public List<string> internationalCompliments;

    [SerializeField] private List<string> ruCompliments = new List<string>()
{
    "Молодец!",
    "Отличная работа!",
    "Ты потрясающий!",
    "Фантастически!",
    "Продолжай в том же духе!",
    "Впечатляюще!",
    "Браво!",
    "Выдающаяся работа!",
    "Отлично!",
    "Замечательная работа!",
    "Ты звезда!",
    "Превосходно!",
    "Замечательно!",
    "Великолепная работа!",
    "Фантастическая работа!",
    "Ты супер!",
    "Невероятно!",
    "На высоте!",
    "Чудесно!",
    "Исключительно!",
    "Блестящая работа!",
    "Ты гений!",
    "Феноменально!",
    "Бриллиантово!",
    "Замечательная работа!",
    "Звездная работа!",
    "Ты выдающийся!",
    "Спектакулярно!",
    "Впечатляющая работа!",
    "Заслужено!",
    "Ты в огне!",
    "Отличный результат!",
    "Великолепный!",
    "Первоклассная работа!",
    "Отлично сработано!",
    "Молодец, так держать!",
    "Ты делаешь фантастически!"
};
    [SerializeField] private List<string> enCompliments = new List<string>()
{
    "Well done!",
    "Great job!",
    "You're amazing!",
    "Fantastic!",
    "Keep it up!",
    "Impressive!",
    "Bravo!",
    "Outstanding work!",
    "Excellent!",
    "Terrific job!",
    "You're a star!",
    "Superb!",
    "Wonderful!",
    "Awesome work!",
    "Fantastic job!",
    "You're a rockstar!",
    "Incredible!",
    "Top-notch!",
    "Marvelous!",
    "Exceptional!",
    "Splendid job!",
    "You're a genius!",
    "Phenomenal!",
    "Brilliant!",
    "Remarkable work!",
    "Stellar job!",
    "You're outstanding!",
    "Spectacular!",
    "Impressive work!",
    "Well deserved!",
    "You're on fire!",
    "Great performance!",
    "Magnificent!",
    "First-class work!",
    "Well executed!",
    "Kudos to you!",
    "You're doing fantastic!"
};

    [SerializeField] private string ruTaptext, enTapText;
    [SerializeField] private string ruBesttext, enBestText;

    [SerializeField] private Text tapText, bestText;
    public void Awake()
    {
#if !UNITY_EDITOR && UNITY_WEBGL   
            language = GetLang();
#endif
        LangSetUp();
        
    }
    private void LangSetUp()
    {
        if (language == "ru")
        {
            internationalCompliments = ruCompliments;
            tapText.text = ruTaptext;
            bestText.text = ruBesttext;
        }
        else
        {
            internationalCompliments = enCompliments;
            tapText.text = enTapText;
            bestText.text = enBestText;
        }
    }
}
