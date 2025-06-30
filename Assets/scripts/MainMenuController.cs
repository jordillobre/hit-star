using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainMenuController : MonoBehaviour
{

    public Text numJugadores;
    private int jugadores;
    private string njugadores;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
//
    public void empezar()
    {
        if (string.IsNullOrEmpty(numJugadores.text))
        {
            Debug.LogWarning("Introduce un número de jugadores.");
            return;
        }

        int jugadores;
        if (int.TryParse(numJugadores.text, out jugadores))
        {
            PlayerPrefs.SetInt("numJugadores", jugadores);
            PlayerPrefs.Save(); // Opcional, pero recomendable
            SceneManager.LoadScene("Musica");
        }
        else
        {
            Debug.LogWarning("El valor introducido no es un número válido.");
        }
    }
}
