using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance; // start���� �ص� �Ǳ��ϴµ�,
    // lifecycle �󿡼� awake�� ���� ������ ������ awake���� �ν��Ͻ� ����

    // Obj
    [Header("- Objs")]
    public GameObject foodPrefab; // ���� �޾ƿ���
    public GameObject dovePrefab;
    public GameObject hawkPrefab;
    public GameObject controlPannel;
    // �ӵ� ���� �����̵�
    public Slider timeMultSlider;
    private RectTransform doveBarSize;

    // values
    [Header("- Values")]
    [SerializeField]
    public float foodTimer; // Update�� �� ���� �������� �غ���.
    [HideInInspector]
    public float mapSize = 23f;
    private const float DoveBarMaxHeight = 443.3f;

    [Header("- InitialAmount")]
    public int initialFoodAmount;
    public int initialDoveAmount;
    public int initialHawkAmount;

    [HideInInspector] public int DoveCount;
    [HideInInspector] public int HawkCount;

    [Header("- Text")]
    public TextMeshProUGUI timeScaleText;
    public TextMeshProUGUI FoodAmountText;
    public TextMeshProUGUI HawkAmountText;
    public TextMeshProUGUI DoveAmountText;




    // �̸� �Ҵ��� ��ü
    private WaitForSeconds foodCD;
    
    

    private void Awake()
    {
        Instance = this; // instance�� ������ ä����
        foodCD = new WaitForSeconds(foodTimer);

        // StartSimulation(); - ��ư�� �����

        
    }

    private void Update()
    {
        Time.timeScale = timeMultSlider.value; // 0.5 ~ 5, �⺻ 1

        var tempSize = doveBarSize.sizeDelta;
        tempSize.y = DoveBarMaxHeight * DoveCount / (DoveCount + HawkCount + float.Epsilon);
        doveBarSize.sizeDelta = tempSize;
    }

    public void StartSimulation()
    {
        // Generate Initial Foods
        for(int i = 0; i < initialFoodAmount; i++)
        {
            SpawnPrefabRandomPos(foodPrefab);
        }

        // Generate Initial Doves
        for (int i = 0; i < initialDoveAmount; i++)
        {
            SpawnPrefabRandomPos(dovePrefab);
            DoveCount++;
        }

        // Generate Initial Hawk
        for (int i = 0; i < initialHawkAmount; i++)
        {
            SpawnPrefabRandomPos(hawkPrefab);
            HawkCount++;
        }

        StartCoroutine(SpawningFood());

        SetActive();
    }

    private void SpawnFood()
    {
        // ����
        // ���� ��ǥ
        var posX = Random.Range(-mapSize, mapSize);
        var posY = Random.Range(-mapSize, mapSize);
        var foodPos = new Vector3(posX, 1f, posY);
        // ��ȯ
        Instantiate(foodPrefab, foodPos, Quaternion.identity);
    }

    private IEnumerator SpawningFood()
    {
        while (true)
        {
            // ���� �ֱ�
            yield return foodCD;

            // ����
            SpawnFood();
        }
    }

    private void SpawnPrefabRandomPos(GameObject prefab)
    {
        // ����
        // ���� ��ǥ
        var posX = Random.Range(-mapSize, mapSize);
        var posY = Random.Range(-mapSize, mapSize);
        var foodPos = new Vector3(posX, 1f, posY);
        // ��ȯ
        Instantiate(prefab, foodPos, Quaternion.identity);
    }

   public void SetTimeScaleText(float value)
    {
        timeScaleText.text = "X " + value.ToString("N2");
    }

    public void SetDoveAmount(bool inc)
    {
        initialDoveAmount += inc ? 1 : -1;

        if (initialDoveAmount < 0)
            initialDoveAmount = 0;

        DoveAmountText.text = initialDoveAmount.ToString();
    }

    public void SetHawkAmount(bool inc)
    {
        initialHawkAmount += inc ? 1 : -1;

        if (initialHawkAmount < 0)
            initialHawkAmount = 0;

        // SpawnPrefabRandomPos();

        HawkAmountText.text = initialHawkAmount.ToString();
    }

    public void SetFoodAmount(bool inc)
    {
        initialFoodAmount += inc ? 1 : -1;

        if (initialFoodAmount < 0)
            initialFoodAmount = 0;

        FoodAmountText.text = initialFoodAmount.ToString();
    }

    private void SetActive()
    {
        controlPannel.SetActive(false);
    }

    private void SetDoveBar()
    {

    }
}
