using TMPro;
using DG.Tweening;
using UnityEngine;

public class IntroTrigger : MonoBehaviour
{
    [SerializeField]
    LoreSO[] introInfo;
    int index = 0;
    [SerializeField] GameObject obj;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] float time = 5f;
    float counter = 0;
    bool reached = false; //Use to check if player reached position
    private void Start() {
        FadeOut(0);
    }

    void FixedUpdate()
    {
        RunIntro();
    }

    void RunIntro()
    {
        if (reached == false) return;
        if(Time.time > counter + time - 2f){
            FadeOut(1f);
        }
        if (Time.time > counter + time && index == introInfo.Length)
        {
            obj.SetActive(false);
            gameObject.SetActive(false);
        }
        if (index == 0){
            obj.SetActive(true);
            SetTextInfo();
        }
        if (Time.time > counter + time && index < introInfo.Length)
        {
            SetTextInfo();
        }
    }

    void SetTextInfo()
    {
        counter = Time.time;
        LoreSO info = introInfo[index];
        title.text = info.LoreName;
        text.text = info.Detail;
        index++;
        FadeIn(1.5f);
    }

    void FadeIn(float time){
        title.DOFade(1, time).SetEase(Ease.InBack);
        text.DOFade(1, time).SetEase(Ease.InBack);
    }

    void FadeOut(float time){
        title.DOFade(0, time);
        text.DOFade(0, time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            reached = true;
        }
    }
}
