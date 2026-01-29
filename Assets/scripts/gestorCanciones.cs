using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;

public class gestorCanciones : MonoBehaviour
{
    [Header("Información de la canción")]
    public Text textInterprete;
    public Text textAño;
    public Text textPelicula;
    public Text textTitulo;
    public Text textoResultados;

    public Image imageCaratula;

    [Header("Controlador de sonido")]
    public AudioSource audioSource;
    public Slider volumeSlider;

    [Header("Canciones")]
    public listaCanciones listaCanciones;
    private int cancionActual = 0;

    [Header("Apartados canvas")]
    public GameObject canvasPuntuacion;
    public GameObject canvasJuego;
    public GameObject canvasResultado;

    public Text textJugadorActual; 
    private int numJugadores;
    private int[] puntuaciones;
    private int jugadorActual = 0;
    private float volumenInicial = 50f;
    void Start()
    {
        string json = Resources.Load<TextAsset>("listadoCanciones").text;
        listaCanciones = JsonUtility.FromJson<listaCanciones>(json);

        volumeSlider.value = volumenInicial / 100f; 
        audioSource.volume = volumeSlider.value;

        volumeSlider.onValueChanged.AddListener(delegate { audioSource.volume = volumeSlider.value; });

        canvasPuntuacion.SetActive(false);
        canvasJuego.SetActive(true);
        canvasResultado.SetActive(false);

        numJugadores = PlayerPrefs.GetInt("numJugadores", 2);
        puntuaciones = new int[numJugadores];

        CargarCancion();
    }

    void CargarCancion()
    {
        if (cancionActual < listaCanciones.Canciones.Length)
        {
            CancionesData c = listaCanciones.Canciones[cancionActual];
            textTitulo.text = "La canción se titula: " + c.Titulo;
            textAño.text = "Salio en el año: " + c.Año.ToString();
            textInterprete.text = "Esta interprtada por: " + c.Autor;
            if (c.Pelicula != "")
            {
                textPelicula.text = "Es originaria de la pelicula: " + c.Pelicula;
            }
            else
            {
                textPelicula.text = "";
            }
            if (audioSource.clip != null)
            {
                Resources.UnloadAsset(audioSource.clip);
            }

            if (imageCaratula.sprite != null)
            {
                Resources.UnloadAsset(imageCaratula.sprite);
            }
            imageCaratula.sprite = Resources.Load<Sprite>("canciones/" + c.RutaCaratula);
            audioSource.clip = Resources.Load<AudioClip>("canciones/" + c.RutaCancion);

            audioSource.Stop();
        }
        else
        {
            Debug.Log("No hay más canciones.");
        }
    }

    public void PauseSong()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
    }

    public void PlaySong()
    {
        if (audioSource.clip != null)
            audioSource.Play();
    }

    public void Contesta()
    {
        audioSource.Stop();
        jugadorActual = 0;
        canvasJuego.SetActive(false);
        canvasPuntuacion.SetActive(true);
        textJugadorActual.text = "Jugador " + (jugadorActual + 1);
    }
    public void Añadir1Punto()
    {
        puntuaciones[jugadorActual] += 1;
        CambiarJugador();
    }
    public void Añadir2Punto()
    {
        puntuaciones[jugadorActual] += 2;
        CambiarJugador();
    }
    public void Añadir3Punto()
    {
        puntuaciones[jugadorActual] += 3;
        CambiarJugador();
    }
    public void Añadir4Punto()
    {
        puntuaciones[jugadorActual] += 4;
        CambiarJugador();
    }
    public void Añadir5Punto()
    {
        puntuaciones[jugadorActual] += 5;
        CambiarJugador();
    }
    public void sinPuntos()
    {
        CambiarJugador();
    }
    public void CambiarJugador()
    {
        jugadorActual++;

        if (jugadorActual >= numJugadores)
        {
            MostrarResultados();
        }
        else
        {
            textJugadorActual.text = "Jugador " + (jugadorActual + 1);
        }
    }

    public void MostrarResultados()
    {
        canvasPuntuacion.SetActive(false);
        canvasResultado.SetActive(true);
        textoResultados.text = "";
        for (int i = 0; i < numJugadores; i++)
        {
            textoResultados.text += "La puntuación del jugador " + (i + 1) + " es: " + puntuaciones[i] + " puntos\n";
        }
    }

    public void CerrarResultado()
    {
        canvasResultado.SetActive(false);

        cancionActual++;
        if (cancionActual < listaCanciones.Canciones.Length)
        {
            CargarCancion();
            canvasJuego.SetActive(true);
        }
        else
        {
            for (int i = 0; i < puntuaciones.Length; i++)
            {
                PlayerPrefs.SetInt("PuntuacionJugador" + i, puntuaciones[i]);
            }
            PlayerPrefs.SetInt("numJugadores", puntuaciones.Length);
            PlayerPrefs.Save();

            SceneManager.LoadScene("PantallaFinal");
        }
    }
}
