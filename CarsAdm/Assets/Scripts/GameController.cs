using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {

    public GameObject[] maps;
    public bool isMainScene;
    public GameObject[] cars;
    public GameObject canvasLosePanel;
    public float timeToSpawnFrom = 2f, timeToSpawnTo = 4.5f;
    private int countCars;
    private Coroutine bottomCars, leftCars, rightCars, upCars;
    private bool isLoseOnce;
    public Text nowScore, topScore, coinsCount;
    public GameObject horn;
    public AudioSource turnSignal;
    [NonSerialized] public static int countLoses;
    private static bool isAdd;
    public GameObject adsManager;

    private void Start() {
        if (!isAdd) {
            Instantiate(adsManager, Vector3.zero, Quaternion.identity);
            isAdd = true;
        }

        if(PlayerPrefs.GetInt("NowMap") == 2){
            Destroy(maps[0]);
            maps[1].SetActive(true);
            Destroy(maps[1]);
        }
        else if(PlayerPrefs.GetInt("NowMap") == 3){
            Destroy(maps[0]);
            Destroy(maps[1]);
            maps[2].SetActive(true);
        }
        else{
            maps[0].SetActive(true);
            Destroy(maps[1]);
            Destroy(maps[2]);
        }
        CarController.isLose = false;
        CarController.countCars = 0;
        
        if (isMainScene) {
            timeToSpawnFrom = 4f;
            timeToSpawnTo = 6f;
        }

        bottomCars = StartCoroutine(BottomCars());
        leftCars = StartCoroutine(LeftCars());
        rightCars = StartCoroutine(RightCars());
        upCars = StartCoroutine(UpCars());

        StartCoroutine(CreateHorn());
    }

    private void Update() {
        if (CarController.isLose && !isLoseOnce) {
            countLoses++;
            StopCoroutine(bottomCars);
            StopCoroutine(leftCars);
            StopCoroutine(rightCars);
            StopCoroutine(upCars);
            nowScore.text = "<color=#D687DB>Score:</color> " + CarController.countCars.ToString();
            if (PlayerPrefs.GetInt("Score") < CarController.countCars)
                PlayerPrefs.SetInt("Score", CarController.countCars);

            topScore.text = "<color=#D687DB>Top:</color> " + PlayerPrefs.GetInt("Score").ToString();
            
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.countCars);
            coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();
            
            canvasLosePanel.SetActive(true);
            isLoseOnce = true;
        }
    }

    IEnumerator BottomCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-7.8f, 0.2f, 62.4f), 180f, true);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator LeftCars()
    {
        while (true)
        {
            SpawnCar3(new Vector3(-61.7f, 0.2f, 3f), 270f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator RightCars()
    {
        while (true)
        {
            SpawnCar4(new Vector3(25.7f, 0.2f, 10.1f), 90f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator UpCars()
    {
        while (true)
        {
            SpawnCar2(new Vector3(-0.4f, 0.2f, -23.04f), 0f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    void SpawnCar(Vector3 pos, float rotationY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, 180, 0)) as GameObject;
            newObj.name = "Car - " + ++countCars;
            
            int random = isMainScene ? 1 : Random.Range(1, 6);
            if(isMainScene)
            newObj.GetComponent<CarController>().speed = 10f;
            switch (random){
                case 1:
                case 2:
                newObj.GetComponent<CarController>().rightTurn = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying){
                turnSignal.Play();
                Invoke("StopSound", 4f);
                }
                break;

                case 3:
                case 4:
                newObj.GetComponent<CarController>().leftTurn = true;
                if(isMoveFromUp)
                newObj.GetComponent<CarController>().moveFromUp = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying){
                turnSignal.Play();
                Invoke("StopSound", 4f);
                }
                break;

                case 5:
                break;
            }
    }


    void SpawnCar2(Vector3 pos, float rotationY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, 0, 0)) as GameObject;
        newObj.name = "Car - " + ++countCars;

            int random = isMainScene ? 1 : Random.Range(1, 6);
            if(isMainScene)
            newObj.GetComponent<CarController>().speed = 10f;
            switch (random){
                case 1:
                case 2:
                newObj.GetComponent<CarController>().rightTurn = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying){
                turnSignal.Play();
                Invoke("StopSound", 4f);
                }
                break;

                case 3:
                case 4:
                newObj.GetComponent<CarController>().leftTurn = true;
                if(isMoveFromUp)
                newObj.GetComponent<CarController>().moveFromUp = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying){
                turnSignal.Play();
                Invoke("StopSound", 4f);
                }
                break;

                case 5:
                break;
            }
    }

    void SpawnCar3(Vector3 pos, float rotationY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, 90, 0)) as GameObject;
        newObj.name = "Car - " + ++countCars;

            int random = isMainScene ? 1 : Random.Range(1, 6);
            if(isMainScene)
            newObj.GetComponent<CarController>().speed = 10f;
            switch (random){
                case 1:
                case 2:
                newObj.GetComponent<CarController>().rightTurn = true;
                if (PlayerPrefs.GetString("music") != "No")
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying){
                turnSignal.Play();
                Invoke("StopSound", 4f);
                }
                break;

                case 3:
                case 4:
                newObj.GetComponent<CarController>().leftTurn = true;
                if(isMoveFromUp)
                newObj.GetComponent<CarController>().moveFromUp = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying){
                turnSignal.Play();
                Invoke("StopSound", 4f);
                }
                break;

                case 5:
                break;
            }
    }

    void SpawnCar4(Vector3 pos, float rotationY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, 270, 0)) as GameObject;
        newObj.name = "Car - " + ++countCars;

            int random = isMainScene ? 1 : Random.Range(1, 6);
            if(isMainScene)
            newObj.GetComponent<CarController>().speed = 10f;
            switch (random){
                case 1:
                case 2:
                newObj.GetComponent<CarController>().rightTurn = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying){
                turnSignal.Play();
                Invoke("StopSound", 4f);
                }
                break;

                case 3:
                case 4:
                newObj.GetComponent<CarController>().leftTurn = true;
                if(isMoveFromUp)
                newObj.GetComponent<CarController>().moveFromUp = true;
                if (PlayerPrefs.GetString("music") != "No" && !turnSignal.isPlaying){
                turnSignal.Play();
                Invoke("StopSound", 4f);
                }
                break;

                case 5:
                break;
            }
    }

    void StopSound(){
        turnSignal.Stop();
    }

    IEnumerator CreateHorn(){
        while(true){
            yield return new WaitForSeconds(Random.Range(5, 9));
            if (PlayerPrefs.GetString("music") != "No")
            Instantiate(horn, Vector3.zero, Quaternion.identity);
        }
    }
}
