using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUICanvas : MonoBehaviour
{
    enum YokaiName{};
    string[] yokaiNames = { "��ڏ��m", "������", "�ʂ��Ђ��", "���~��炵", "�񓔂�����", "�낭���", "�A�}�r�G", "�V��" };

    [SerializeField] Sprite[] yokaiSprites;

    [SerializeField] Text resultText;
    [SerializeField] Image resultYokaiImage;
    [SerializeField] Text resultScoreText;
    [SerializeField] Text resultYokaiNameText;

    [SerializeField] Text flavourText;

    string[] flavourTexts =
    {
        "����ꂳ�܁I�I",
        "�悭�撣�����ˁI",
        "���͂����Ə��ڎw�����I",
        "�܂��V��ł�",
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
            bool result;
            if (Random.Range(0f, 1f) < 0.5f)
                result = true;
            else
                result = false;
            SetActiveResultUICanvas(!gameObject.GetComponent<Canvas>().enabled, score, yokaiIndex, result);
        }
    }

    //�Q�[���N���A�\��������Ƃ��ɂ��̃��\�b�h���Ă�ł�������!
    //����1 value�FUI(Canvas)��Canvas�R���|�[�l���g��SetActive(value)
    //����2 score�F���̂܂܃X�R�A�ɂȂ�܂�
    //����3 yokaiIndex�F0�`7�Ŏw�肵�Ă�������. �d���̉摜�A�d�������w�肷��̂ɔz���index�Ƃ��Ďg���܂�
    //0����ڏ��m�ŁA7���V��ł�
    //����4 isTimeUp�F�^�C���A�b�v�ŏI���������ǂ����Afalse�Ȃ�Q�[���I�[�o�[�Ƃ��ďI��
    public void SetActiveResultUICanvas(bool value,int score,int yokaiIndex, bool isTimeUp)
    {
        gameObject.GetComponent<Canvas>().enabled = value;

        resultText.text = isTimeUp ? "�^�C���A�b�v�I" : "�Q�[���I�[�o�[�I";
        

        resultYokaiImage.sprite = yokaiSprites[yokaiIndex];
        resultScoreText.text = score.ToString();

        resultYokaiNameText.text = yokaiNames[yokaiIndex];

        flavourText.text = flavourTexts[Random.Range(0, flavourTexts.Length)];
    }
}
