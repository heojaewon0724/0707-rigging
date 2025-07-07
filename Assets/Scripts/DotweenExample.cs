using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DotweenExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AnimateButton()
    {
       /*transform.DOMove(new Vector3(3, 5, 0), 2f);
         transform.DOScale(new Vector3(2, 2, 2), 1f);
         transform.DORotate(new Vector3(0, 0, 180), 1f, RotateMode.FastBeyond360);
         transform.DOMoveX(5, 1f).SetLoops(-1, LoopType.Yoyo);
         img.DOFade(0.2f, 2f);*/
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveZ(3f, 2f));

        seq.Append(transform.DOMoveY(2f, 1f))
            .Join(transform.DOScale(new Vector3(2,2,2), 1f))
            .Join(transform.DORotate(new Vector3(0, 180, 0), 1f));
        seq.OnComplete(() => 
        {
            Debug.Log("애니메이션 완료");
        });
    }
}
