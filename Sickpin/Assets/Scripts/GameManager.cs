using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject startPoint;
    private GameObject spawnPoint; 
    private Pin currentPin;
    public GameObject pinPerfab;
    private bool isGameOver = false;
    private int a;
    public Text text;
    public GameObject message;
    private Camera mainCamera;
    public float speed =3f;
    
    void Start()
    {
        startPoint = GameObject.Find("StartPoint");
        spawnPoint = GameObject.Find("SpawnPoint");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mainCamera = Camera.main;
        message.SetActive(false);
        SwpanPin();
    }


    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            a++;
            text.text = a.ToString();
            currentPin.StartFly();
            SwpanPin();
        }
    }

    void SwpanPin()
    {
        currentPin = GameObject.Instantiate(pinPerfab, spawnPoint.transform.position, pinPerfab.transform.rotation).GetComponent<Pin>();
        
    }

    public void GameOver()
    {
        if (isGameOver) return;
        GameObject.Find("Circle").GetComponent<RotateSelf>().enabled = false;
        StartCoroutine(GameOverAnimation());
        isGameOver = true;
    }

    IEnumerator GameOverAnimation()
    {
        while (true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, speed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 5, speed * Time.deltaTime);
            message.SetActive(true);
            message.GetComponentInChildren<Text>().text = "Score:" + text.text;
            if (Mathf.Abs(mainCamera.orthographicSize - 5)  < 0.01f)
            {
                break;
            }
            yield return 0;
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
