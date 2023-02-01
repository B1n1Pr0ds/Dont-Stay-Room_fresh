using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Spawner Functions")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> spawnObjects;
    private List<int> itemIndex = new List<int>();
    private List<int> spawnerIndex = new List<int>();
    [SerializeField] private List<int> alreadyUsed = new List<int>();


    [Header("Util")]
    private int randomNumber;


    [Header("Game management")]

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject levelWon;
    [SerializeField] private GameObject objectiveScreen;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private FPMovement fpmovement;
    [SerializeField] private TPMovement tpmovement;
    [SerializeField] private float yourTime;
    [SerializeField] private float bestTime;
    [SerializeField] private TextMeshProUGUI yourTimeTxt;
    [SerializeField] private TextMeshProUGUI bestTimeTxt;




    [Header("Timer")]
    private bool startTimer = false;
    [SerializeField] private float timeValueDecrease = 0;
    public float initialTimeValue = 300;
    [SerializeField] TextMeshProUGUI timeTxt;
    

    

    private bool shouldSpawn = true;
    public int itemCount = 0;


    private void Start()
    {
        for(int i=0; i < spawnObjects.Count; i++)
        {
            itemIndex.Add(i);
        }
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnerIndex.Add(i);
        }
        SpawnItem();
        mouseLook.enabled = false;
        fpmovement.enabled = false;
        tpmovement.enabled = false;
        objectiveScreen.SetActive(true);
        gameOver.SetActive(false);
        levelWon.SetActive(false);
     
        bestTimeTxt.text = PlayerPrefs.GetFloat("BestTime", initialTimeValue).ToString();
    }


    private void Update()
    {
        if (timeValueDecrease > 0 && startTimer)
        {
            timeValueDecrease -= Time.deltaTime;
        }
        else
        {
            timeValueDecrease = 0;
        }
        TimeDisplay(timeValueDecrease);

        if (startTimer)
        {
            yourTime += Time.deltaTime;
            if(yourTime > initialTimeValue)
            {
                GameOver();
            }
        }
    }
    int GenerateRandomObject()
    {
        int _index = Random.Range(0, itemIndex.Count);
        randomNumber = itemIndex[_index];
        alreadyUsed.Add(randomNumber);
        itemIndex.Remove(itemIndex[_index]);
        return randomNumber;
        
    }
    int GenerateRandomSpawn()
    {
        int _index = Random.Range(0, spawnerIndex.Count);
        randomNumber = spawnerIndex[_index];
        alreadyUsed.Add(randomNumber);
        spawnerIndex.Remove(spawnerIndex[_index]);
        return randomNumber;

    }
    private void SpawnItem()
    {
        if (itemCount >= 4)
        {
            shouldSpawn = false;
            LevelWon();
        }
        if (shouldSpawn)
        {
            Instantiate(spawnObjects[GenerateRandomObject()], spawnPoints[GenerateRandomSpawn()]);
            shouldSpawn = false;
        }
        else
            return;
    }

    public void ItemPickedUp()
        {
              
            shouldSpawn = true;
            itemCount++;
            SpawnItem();
        
        }

    private void LevelWon()
    {
        startTimer = false;
        mouseLook.enabled = false;
        fpmovement.enabled = false;
        tpmovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        ShowYourTime(yourTime);
        if( yourTime < PlayerPrefs.GetFloat("BestTime", initialTimeValue))
        {
            bestTime = yourTime;
            PlayerPrefs.SetFloat("BestTime", yourTime);
        }
        ShowBestTime(PlayerPrefs.GetFloat("BestTime"));
        levelWon.SetActive(true);
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartLevel()
    {
        objectiveScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        timeValueDecrease = initialTimeValue;
        startTimer = true;
        mouseLook.enabled = true;
        fpmovement.enabled = true;
        
    }


    void TimeDisplay(float _timeToDisplay)
    {
        if(_timeToDisplay < 0)
        {
            _timeToDisplay = 0;
        }

        float _minutes = Mathf.FloorToInt(_timeToDisplay / 60);
        float _seconds = Mathf.FloorToInt(_timeToDisplay % 60);

        timeTxt.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }

    private void ShowYourTime(float _yourTime)
    {
        float _minutes = Mathf.FloorToInt(_yourTime/60);
        float _seconds = Mathf.FloorToInt(_yourTime % 60);
        yourTimeTxt.text = string.Format("{0:00}:{1:00}", _minutes, _seconds );
    }
    private void ShowBestTime(float _bestTime)
    {
        float _minutes = Mathf.FloorToInt(_bestTime/60);
        float _seconds = Mathf.FloorToInt(_bestTime % 60);
        bestTimeTxt.text = string.Format("{0:00}:{1:00}",_minutes, _seconds);
    }

    public void GameOver()
    {
        startTimer = false;
        mouseLook.enabled = false;
        fpmovement.enabled = false;
        tpmovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        gameOver.SetActive(true);
    }

    public void ResetBestTime()
    {
        PlayerPrefs.DeleteAll();
        bestTime = initialTimeValue;
        ShowBestTime(bestTime);
    }


}
