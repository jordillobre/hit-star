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
    public GameObject canvaInstrucciones;
    public GameObject canvaCanciones;

    [Header("Creación numero de jugadores")]
    public TMP_InputField numJugadores;
    private int totalJugadores;
    private int equipoActual = 0;
    public Button botonValidarNum;

    [Header("Asignación nombre a los jugadores")]
    public TMP_InputField inputNombreEquipo;
    public TextMeshProUGUI textoIndicadorEquipo;

    [Header("Mensaje de error")]
    public TextMeshProUGUI textoErrorJugadores;
    public TextMeshProUGUI textoErrorNombre;

    // private int jugadores;
    void Start(){
        if(canvaInicio != null) canvaInicio.SetActive(true);
        if(canvaJugadores != null) canvaJugadores.SetActive(false);
        if(canvaNombres != null) canvaNombres.SetActive(false);
        if(canvaCategorias != null) canvaCategorias.SetActive(false);
        if(canvaOpciones != null) canvaOpciones.SetActive(false);

        OcultarError(textoErrorJugadores);
        OcultarError(textoErrorNombre);
    }

    public void Empezar(){
        CambiarCanva(canvaInicio, canvaJugadores);
    }

    public void ValidarNumJugadores(){
        OcultarError(textoErrorJugadores);

        if (string.IsNullOrEmpty(numJugadores.text)){
            MostrarError(textoErrorJugadores, "Introduce un número de jugadores.");
            return;
        }

        int jugadoresInput;
        if (int.TryParse(numJugadores.text, out jugadoresInput)){
            if (jugadoresInput < 2 || jugadoresInput > 10)
            {
                MostrarError(textoErrorJugadores, "El número de jugadores debe ser entre 2 y 10.");
                return;
            }

            totalJugadores = jugadoresInput;
            equipoActual = 0;
            AsignarNombres();
        }
        else{
            MostrarError(textoErrorJugadores, "Introduce un número válido.");
        }
    }

    public void AsignarNombres(){
        OcultarError(textoErrorNombre);

        if (equipoActual < totalJugadores){
            CambiarCanva(canvaJugadores, canvaNombres);
            inputNombreEquipo.text = "";
            textoIndicadorEquipo.text = "Introduce el nombre para el Equipo " + (equipoActual + 1);
        }else{
            PlayerPrefs.Save();
            CambiarCanva(canvaNombres, canvaCategorias);
        }
    }  

    public void GuardarNombreYContinuar(){
        OcultarError(textoErrorNombre);

        string nombre = string.IsNullOrEmpty(inputNombreEquipo.text)
            ? "Equipo " + (equipoActual + 1)
            : inputNombreEquipo.text.Trim();
        
        if (NombreEnUso(nombre)){
            MostrarError(textoErrorNombre, "El nombre ya está en uso. Elige otro.");
            return;
        }
        
        PlayerPrefs.SetString("NombreEquipo_" + equipoActual, nombre);
        equipoActual++;
        AsignarNombres();
    }

    public bool NombreEnUso (string nombre) {
        for (int i = 0; i< equipoActual; i++){
            string nombreGuardado = PlayerPrefs.GetString("NombreEquipo_" +i, "");
            if (nombreGuardado.Equals(nombre, StringComparison.OrdinalIgnoreCase)){
                return true;
            }
        }
        return false;
    }

    public void SeleccionarCategoria(Button boton){
        TextMeshProUGUI textoBoton = boton.GetComponentInChildren<TextMeshProUGUI>();
        if (textoBoton != null){
            string categoria = textoBoton.text;
            PlayerPrefs.SetString("CategoriaSeleccionada", categoria);
            PlayerPrefs.Save();
            EmpezarJuego();
        }
    }

    public void EmpezarJuego(){
        SceneManager.LoadScene("GameScene");
    }

    public void CambiarCanva(GameObject canvaOcultar, GameObject canvaMostrar){
        if(canvaOcultar != null) canvaOcultar.SetActive(false);
        if(canvaMostrar != null) canvaMostrar.SetActive(true);
    }

    private void MostrarError(TextMeshProUGUI textoError, string mensaje){
        if (textoError == null) return;
            textoError.text = mensaje;
            textoError.gameObject.SetActive(true);
        
    }

    private void OcultarError(TextMeshProUGUI textoError){
        if (textoError == null) return;
            textoError.gameObject.SetActive(false);
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
    public void VerInstrucciones(){
        CambiarCanva(canvaInicio, canvaInstrucciones);
    }
    public void CerrarInstrucciones(){
        CambiarCanva(canvaInstrucciones, canvaInicio);
    }
    public void VerCanciones(){
        CambiarCanva(canvaInicio, canvaCanciones);
    }
    public void CerrarCanciones(){
        CambiarCanva(canvaCanciones, canvaInicio);
    }

    public void CerrarJuego(){
        Application.Quit();
    }

}
