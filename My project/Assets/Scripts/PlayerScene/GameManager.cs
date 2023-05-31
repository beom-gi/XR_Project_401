using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //UI �߰�
using System;           //�߰�
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static SceneChanger SceneChanger => SceneChanger.Instance;
    public enum GameState           //���� ���°� ����
    {
        Start,
        Playing,
        GameOver
    }

    public event Action<GameState> OnGameStateChanged;

    public GameState currentState = GameState.Start;

    public GameState CurrentState
    {
        get { return currentState; }
        private set
        {
            currentState = value;
            OnGameStateChanged?.Invoke(currentState);       //�̺�Ʈ�� null�� �ƴѰ�쿡�� �� �̺�Ʈ�� ȣ��
        }
    }

    public void StartGame()
    {   //���ӽ��� ������ ���⿡ �ۼ�
        CurrentState = GameState.Playing;
    }

    public void GameOver()
    {   //���ӿ��� ������ ���⿡ �ۼ�
        CurrentState = GameState.GameOver;
        SceneChanger.LoadEndScene();
    }

    public GameManager() { }
    public static GameManager Instance { get; private set; }        //�̱���ȭ

    public PlayerHp playerHp;           //�÷��̾��� HP
    public Image playerHpUIImage;       //�÷��̾��� HP UI �̹���
    public Button btnSample;

/*    private void Start()
    {
        this.btnSample.onClick.AddListener(() =>            //Listner�� ��ư ���
        {
            Debug.Log("Button Check");
        });
    }*/

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        Init();
    }

    private void OnDestroy()    //�� ������Ʈ�� �ı��� ���
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  //�̺�Ʈ�� �����Ѵ�. 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            Init();
        }
    }

    private void Init()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();             //Tag�� ������Ʈ�� ã�´�.
        playerHpUIImage = GameObject.FindGameObjectWithTag("UIHealthBar").GetComponent<Image>();    //Tag�� UI�� ã�´�.
        playerHp.Hp = 100;
        CurrentState = GameState.Start;
    }

    private void Update()
    {
        playerHpUIImage.fillAmount = (float)playerHp.Hp / 100.0f;           //ü�¿� ����ϰ� �۾�
    }
}
