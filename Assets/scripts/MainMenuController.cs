using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;

public class MainMenuController : MonoBehaviour
{
    [Header("Apartados canvas")]
    public GameObject canvaInicio;
    public GameObject canvaJugadores;
    public GameObject canvaNombres;
    public GameObject canvaCategorias;
    public GameObject canvaOpciones;

    [Header("Creación numero de jugadores")]
    public TMP_InputField numJugadores;
    private int totalJugadores;
    private int equipoActual = 0;

    [Header("Asignación nombre a los jugadores")]
    public TMP_InputField inputNombreEquipo;
    public TextMeshProUGUI textoIndicadorEquipo;

// apartado por hacer

    private int jugadores;
    void Start()
    {
        if(canvaInicio != null) canvaInicio.SetActive(true);
        if(canvaJugadores != null) canvaJugadores.SetActive(false);
        if(canvaNombres != null) canvaNombres.SetActive(false);
        if(canvaCategorias != null) canvaCategorias.SetActive(false);
        if(canvaOpciones != null) canvaOpciones.SetActive(false);
    }

    public void Empezar(){
        CambiarCanva(canvaInicio, canvaJugadores);
        if (string.IsNullOrEmpty(numJugadores.text))
        {
            Debug.LogWarning("Introduce un número de jugadores.");
            return;
        }

        int jugadores;
        if (int.TryParse(numJugadores.text, out jugadores))
        {
            PlayerPrefs.SetInt("numJugadores", totalJugadores);
            equipoActual = 0;
            AsignarNombres();
        }
        else
        {
            Debug.LogWarning("El valor introducido no es un número válido.");
        }
    }

    public void AsignarNombres(){
        Debug.LogWarning("Vamos a poner nombre a estos equipos");
        if (equipoActual < totalJugadores)
        {
            CambiarCanva(canvaJugadores, canvaNombres);
            inputNombreEquipo.text = "";
            textoIndicadorEquipo.text = "Introduce el nombre para el Equipo " + (equipoActual + 1);
        }
        else
        {
            PlayerPrefs.Save();
            CambiarCanva(canvaNombres, canvaCategorias);
        }
    }
    public void GuardarNombreYContinuar()
    {
        string nombre = string.IsNullOrEmpty(inputNombreEquipo.text) ? 
                        "Equipo " + (equipoActual + 1) : inputNombreEquipo.text;
        
        PlayerPrefs.SetString("NombreEquipo_" + equipoActual, nombre);
        equipoActual++;
        AsignarNombres();
    }

    public void CambiarCanva(GameObject canvaOcultar, GameObject canvaMostrar){
        if(canvaOcultar != null) canvaOcultar.SetActive(false);
        if(canvaMostrar != null) canvaMostrar.SetActive(true);
    }

    public void SelJugadores(){
        CambiarCanva(canvaInicio, canvaJugadores);
    }

    public void VerOpciones(){
        CambiarCanva(canvaInicio, canvaOpciones);
    }

    public void CerrarOpciones(){
        CambiarCanva(canvaOpciones, canvaInicio);
    }

    public void CerrarJuego(){
        Application.Quit();
    }


}
