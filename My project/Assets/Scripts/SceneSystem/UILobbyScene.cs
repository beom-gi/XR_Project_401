using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       //UI 를 사용하기 위해 선언

public class UIEndScene : MonoBehaviour
{
    protected SceneChanger SceneChanger => SceneChanger.Instance;           //싱글톤 불러오기
    public Button gameButton;                           //선언한 버튼 설정

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