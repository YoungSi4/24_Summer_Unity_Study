using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance; // start에서 해도 되긴하는데,
    // lifecycle 상에서 awake가 가장 빠르기 때문에 awake에서 인스턴스 생성

    // Obj
    [Header("- Objs")]
    public GameObject foodPrefab; // 음식 받아오기
    public GameObject dovePrefab;
    public GameObject hawkPrefab;
    public GameObject controlPannel;
    // 속도 조절 슬라이드
    public Slider timeMultSlider;
    private RectTransform doveBarSize;

    // values
    [Header("- Values")]
    [SerializeField]
    public float foodTimer; // Update를 덜 쓰는 방향으로 해보자.
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




    // 미리 할당한 객체
    private WaitForSeconds foodCD;
    
    

    private void Awake()
    {
        Instance = this; // instance의 내용을 채워줌
        foodCD = new WaitForSeconds(foodTimer);

        // StartSimulation(); - 버튼에 연결됨

        
    }

    private void Update()
    {
        Time.timeScale = timeMultSlider.value; // 0.5 ~ 5, 기본 1

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
        // 생성
        // 생성 좌표
        var posX = Random.Range(-mapSize, mapSize);
        var posY = Random.Range(-mapSize, mapSize);
        var foodPos = new Vector3(posX, 1f, posY);
        // 소환
        Instantiate(foodPrefab, foodPos, Quaternion.identity);
    }

    private IEnumerator SpawningFood()
    {
        while (true)
        {
            // 생성 주기
            yield return foodCD;

            // 생성
            SpawnFood();
        }
    }

    private void SpawnPrefabRandomPos(GameObject prefab)
    {
        // 생성
        // 생성 좌표
        var posX = Random.Range(-mapSize, mapSize);
        var posY = Random.Range(-mapSize, mapSize);
        var foodPos = new Vector3(posX, 1f, posY);
        // 소환
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
