using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUICanvas : MonoBehaviour
{
    enum YokaiName{};
    string[] yokaiNames = { "�ЂƂߏ��m", "������", "�ʂ��Ђ��", "���~��炵", "�񓔂�����", "�낭���", "�A�}�r�G", "�V��" };

    [SerializeField] Sprite[] yokaiSprites;

    [SerializeField] Image resultYokaiImage;
    [SerializeField] Text resultScoreText;
    [SerializeField] Text resultYokaiNameText;

    [SerializeField] Text flavourText;

    string[] flavourtexts =
    {
        "����ꂳ�܁I�I",
        "�悭�撣�����ˁI",
        "������傢�I",
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�e�X�g�p
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int score = Random.Range(0, 1000)*10;
            int yokaiIndex = Random.Range(0, 8);
            SetActiveResultUICanvas(!gameObject.GetComponent<Canvas>().enabled, score, yokaiIndex);
        }
    }

    //�Q�[���N���A�\��������Ƃ��ɂ��̃��\�b�h���Ă�ł�������!
    //����1 value�FUI(Canvas)��Canvas�R���|�[�l���g��SetActive(value)
    //����2 score�F���̂܂܃X�R�A�ɂȂ�܂�
    //����3 yokaiIndex�F0�`7�Ŏw�肵�Ă�������. �d���̉摜�A�d�������w�肷��̂ɔz���index�Ƃ��Ďg���܂�
    ////0����ڏ��m�ŁA7���V��ł�
    public void SetActiveResultUICanvas(bool value,int score,int yokaiIndex)
    {
        gameObject.GetComponent<Canvas>().enabled = value;

        resultYokaiImage.sprite = yokaiSprites[yokaiIndex];
        resultScoreText.text = score.ToString();

        resultYokaiNameText.text = yokaiNames[yokaiIndex];

        flavourText.text = flavourtexts[Random.Range(0, flavourtexts.Length)];
    }
}
