using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static float xAxis = 13f;
    public static float zAxis = 21f;

    
    public GameObject bullet;
    public GameObject player;
    public GameObject gameCanvas;
    
    public int numberOfBulletsPerSpawn;
    

    public float gameStartWait;
    public float bulletSpawnFrequency;

    private GameObject startPanel;
    private GameObject gameUIPanel;
    private GameObject touchPanel;

    private float highestTime;
    private bool isGameOver;
    private float startTime;
    private float playTime;

    private void Awake()
    {
        
        DontDestroyOnLoad(transform.gameObject);

    }
    // Use this for initialization
    void Start () {

        startPanel = gameCanvas.transform.Find("StartPanel").gameObject;

        gameUIPanel = gameCanvas.transform.Find("GameUIPanel").gameObject;
        touchPanel = gameCanvas.transform.Find("TouchArea").gameObject;



        LoadHighestScoreForStartMenu();
        gameUIPanel.SetActive(false);
        touchPanel.SetActive(false);
        player.SetActive(false);

        //StartCoroutine(SpawnBullets(numberOfBulletsPerSpawn));
    }
	
	// Update is called once per frame
	void Update () {
        if (!isGameOver && gameUIPanel.activeSelf)
            UpdateTimer();
	}

    public void StartGame()
    {
        isGameOver = false;
        startPanel.SetActive(false);
        player.SetActive(true);
        player.transform.position = new Vector3(0f, 0f, 0f);
        startTime = Time.time;
        DestoryAllBullets();
        GetHighestScore();
        SetPBOnGameUI();

        
        touchPanel.SetActive(true);
        gameUIPanel.SetActive(true);
        StartCoroutine(SpawnBullets(numberOfBulletsPerSpawn));
    }

    private IEnumerator SpawnBullets(int numberOfBulletsPerSpawn)
    {
        while (!isGameOver)
        {
            float randomFloat = Random.Range(1f, 5f);


            if (randomFloat <= 2f)
            {
                
                InstantiateBullets("top", GameController.xAxis,GameController.zAxis, numberOfBulletsPerSpawn);
            }
            else if (randomFloat <= 3f && randomFloat > 2f)
            {
                InstantiateBullets("bot", GameController.xAxis, GameController.zAxis, numberOfBulletsPerSpawn);
            }
            else if (randomFloat <= 4f && randomFloat > 3f)
            {
                InstantiateBullets("left", GameController.xAxis, GameController.zAxis, numberOfBulletsPerSpawn);
            }
            else if (randomFloat <= 5f && randomFloat > 4f)
            {
                InstantiateBullets("right", GameController.xAxis, GameController.zAxis, numberOfBulletsPerSpawn);
            }
            else
            {
                Debug.Log("Bullet spawn failed");
            }

            yield return new WaitForSeconds(bulletSpawnFrequency);
        }
        
    }

    private void InstantiateBullets(string boundarySide, float xAxisMax, float zAxisMax, int noOfBullets)
    {
        Quaternion qtn = Quaternion.Euler(new Vector3(90f, 0, 0));
        xAxisMax = xAxisMax + 2f; 
        zAxisMax = zAxisMax + 2f;

        for (int i = 0; i < noOfBullets; i++)
        {
            Vector3 position;
            switch (boundarySide)
            {
                case "top":
                    position = new Vector3(Random.Range(-xAxisMax, xAxisMax), 0f, zAxisMax);
                    break;

                case "bot":
                    position = new Vector3(Random.Range(-xAxisMax, xAxisMax), 0f, -zAxisMax);
                    break;

                case "left":
                    position = new Vector3(-xAxisMax, 0f, Random.Range(-zAxisMax, zAxisMax));
                    break;

                case "right":
                    position = new Vector3(xAxisMax, 0f, Random.Range(-zAxisMax, zAxisMax));
                    break;

                default:
                    position = new Vector3(-xAxisMax, 0f, Random.Range(-zAxisMax, zAxisMax));
                    break;

            }


            Instantiate(bullet, position, qtn);
        }
    }
    
    private void GetHighestScore()
    {
        highestTime = (PlayerPrefs.HasKey("bestScore")) ? PlayerPrefs.GetFloat("bestScore") : 0f;
    }

    private void LoadHighestScoreForStartMenu()
    {

        GetHighestScore();
        Transform scoreText = startPanel.transform.Find("HighestScore");
        if (scoreText != null)
        {
            scoreText.gameObject.GetComponent<Text>().text = highestTime.ToString("F2") + " s";
        }
            
    }

    public void GameOver()
    {
        isGameOver = true;
        highestTime = (PlayerPrefs.HasKey("bestScore")) ? PlayerPrefs.GetFloat("bestScore") : 0f;
        Transform pbText = gameUIPanel.transform.Find("PBText");
        if (playTime > highestTime)
        {
            PlayerPrefs.SetFloat("bestScore", playTime);

            if (pbText != null)
                pbText.gameObject.GetComponent<Text>().text = "Congrats! Highest Score!";
        }
        else
        {
            pbText.gameObject.GetComponent<Text>().text = "";
        }

        gameUIPanel.transform.Find("Button").gameObject.SetActive(true);
        touchPanel.SetActive(false);
        
    }

    private void UpdateTimer()
    {
        Transform timerText = gameUIPanel.transform.Find("Timer");
        gameUIPanel.transform.Find("PBText").gameObject.GetComponent<Text>().text="";
        gameUIPanel.transform.Find("Button").gameObject.SetActive(false);
        playTime = Time.time - startTime;
        if (timerText != null)
        {
            timerText.gameObject.GetComponent<Text>().text = playTime.ToString("F2") + " s";
        }
            
    }

    private void SetPBOnGameUI()
    {
        GetHighestScore();
        gameUIPanel.transform.Find("CurPBText").gameObject.GetComponent<Text>().text = "Best: " + highestTime.ToString("F2") + " s";
    }

    private void DestoryAllBullets()
    {
        GameObject[] bulletObjects = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject obj in bulletObjects)
        {
            Destroy(obj);
        }
    }

    
}
