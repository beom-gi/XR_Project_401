using System.Collections;
using System.Collections.Generic;
using System.Linq;                      //LInq ��  ���
using UnityEngine;

public enum FxType
{
    hit,
    Explosion
}

[System.Serializable]
public class FxBundle
{
    [SerializeField] private FxType fxType;         //����Ʈ Ÿ��
    [SerializeField] private GameObject fxPrefab;   //Fx ������ ����
    public FxType FxType => fxType;
    public GameObject FxPrefab => fxPrefab;

}
public class FxManager : MonoBehaviour
{
    //---------------------------------------------------
    public FxManager() { }
    public static FxManager Instance { get; private set; }
    //-------------------- �̱��� -----------------------

    [SerializeField] private List<FxBundle> fxList;

    public Dictionary<FxType, GameObject> FXDict { get; private set; } = new Dictionary<FxType, GameObject>();
    public List<FxBundle> FxList => fxList;

    private void Awake()
    {
        if (Instance)                               //�ν��Ͻ��� �����ϴ��� Ȯ��
        {
            Destroy(gameObject);                    //�ߺ��� ������Ʈ�� ������ ���ݰ� ����
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);          //�� �Լ��� Scene �� ����ǵ� ������� �ʰ� �Ѵ�.
            for (int i = 0; i < FxType.GetValues(typeof(FxType)).Length; i++)
            {
                FXDict.Add((FxType)i, FxList.FirstOrDefault(x => x.FxType == (FxType)i)?.FxPrefab);
            }
        }
    }
    public void PlayFx(Transform targetTransform, FxType targetFx, Vector3 Offset)          //�̱���� �Լ� ��𼭵� Fx�� �ҷ� �ü� �ְ� ��
    {
        Instantiate(FXDict[targetFx], targetTransform.position + Offset, new Quaternion(0.0f, 0.0f, 0.0f, 0.1f));   //�޾ƿ� �μ����� Ȱ���Ͽ� �ش� ��ġ�� Fx ����
    }
}