using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject optionPrefab;
    public Transform optionsContainer;
    public float verticalOffset;

    private float currentOffset;

    [System.Serializable]
    public struct MenuOption
    {
        public string text;
        public string sceneName;
    }

    public List<MenuOption> options = new List<MenuOption>();

    // Start is called before the first frame update
    void Start()
    {
        currentOffset = 0;
        foreach (MenuOption option in options)
        {
            // Create a new option object
            GameObject newOption = Instantiate(optionPrefab, optionsContainer);
            newOption.transform.position = new Vector3(newOption.transform.position.x, newOption.transform.position.y - currentOffset, 0);
            newOption.name = option.text;

            // Set the text of the option
            TextMeshProUGUI textMesh = newOption.GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = option.text;

            // Add a button component to the option
            UnityEngine.UI.Button button = newOption.GetComponent<UnityEngine.UI.Button>();

            // Add a click listener to the button that loads the specified scene
            button.onClick.AddListener(() => LoadScene(option.sceneName));
            currentOffset += verticalOffset;
        }
    }

    // Load the specified scene
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
