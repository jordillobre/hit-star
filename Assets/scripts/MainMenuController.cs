using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Apartados canvas")]
    public GameObject canvaInicio;
    public GameObject canvaJugadores;
    public GameObject canvaNombres;
    public GameObject canvaCategorias;
    public GameObject canvaOpciones;
    public GameObject canvaAyuda;

    public Text numJugadores;


    private int jugadores;
    private string njugadores;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(canvaInicio != null) canvaInicio.SetActive(true);
        if(canvaJugadores != null) canvaJugadores.SetActive(false);
        if(canvaNombres != null) canvaNombres.SetActive(false);
        if(canvaCategorias != null) canvaCategorias.SetActive(false);
        if(canvaOpciones != null) canvaOpciones.SetActive(false);
        if(canvaAyuda != null) canvaAyuda.SetActive(false);
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

    public void selJugadores(){
        if(canvaInicio != null) canvaInicio.SetActive(false);
        if(canvaJugadores != null) canvaJugadores.SetActive(true);
    }

    public void verOpciones(){
        if(canvaInicio != null) canvaInicio.SetActive(false);
        if(canvaOpciones != null) canvaOpciones.SetActive(true);
    }

    public void cerrarOpciones(){
        if(canvaInicio != null) canvaInicio.SetActive(true);
        if(canvaOpciones != null) canvaOpciones.SetActive(false);
    }
    public void verAyuda(){
        if(canvaInicio != null) canvaInicio.SetActive(false);
        if(canvaAyuda != null) canvaAyuda.SetActive(true);
    }

    public void cerrarAyuda(){
        if(canvaInicio != null) canvaInicio.SetActive(true);
        if(canvaAyuda != null) canvaAyuda.SetActive(false);
    }
    public void cerrarJuego(){
        
    }
}
