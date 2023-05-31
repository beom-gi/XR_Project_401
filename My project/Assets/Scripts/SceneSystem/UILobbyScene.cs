using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       //UI �� ����ϱ� ���� ����

public class UIEndScene : MonoBehaviour
{
    protected SceneChanger SceneChanger => SceneChanger.Instance;           //�̱��� �ҷ�����
    public Button gameButton;                           //������ ��ư ����

    void Start()
    {
        gameButton.onClick.AddListener(OnGameButtonClick);
    }

    void Update()
    {

    }

    private void OnGameButtonClick()
    {
        SceneChanger.LoadLobbyScene();
    }
}