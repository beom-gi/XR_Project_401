using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameManager() { }
    public static GameManager Instance { get; private set; }        //�̱���ȭ

    public PlayerHp playerHp;           //�÷��̾��� HP
    public Image playerHpUIImage;       //�÷��̾��� HP UI �̹���
    public Button btnSample;

    private void Start()
    {
        this.btnSample.onClick.AddListener(() =>            //Listner�� ��ư ���
        {
            Debug.Log("Button Check");
        });
    }

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
        Init();
    }

    private void Init()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();             //Tag�� ������Ʈ�� ã�´�.
        playerHpUIImage = GameObject.FindGameObjectWithTag("UIHealthBar").GetComponent<Image>();    //Tag�� UI�� ã�´�.
    }

    private void Update()
    {
        playerHpUIImage.fillAmount = (float)playerHp.Hp / 100.0f;           //ü�¿� ����ϰ� �۾�
    }
}