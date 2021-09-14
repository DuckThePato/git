using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    private GameObject button;
    public GameObject game;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        CreateButton(game);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateButton(GameObject gameObject)
    {
        button = new GameObject("Botao");
        button.transform.parent = gameObject.transform;
        button.AddComponent<Image>();
        button.GetComponent<Image>().sprite = sprite;
        button.AddComponent<Button>();
        button.transform.position = new Vector3(100, 100, 0);
        button.transform.localScale = new Vector3(1, 1, 0);
        button.GetComponent<Button>().onClick.AddListener(onClick);
    }

    public void onClick()
    {
        SceneManager.LoadScene("batalha");
    }
}
