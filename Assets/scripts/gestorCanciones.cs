using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;

public class gestorCanciones : MonoBehaviour
{
    public Text textInterprete;
    public Text textAño;
    public Text textPelicula;
    public Text textTitulo;
    public Text textoResultados;

    public Image imageCaratula;

    public AudioSource audioSource;
    public Slider volumeSlider;

    public listaCanciones listaCanciones;
    private int cancionActual = 0;

    public GameObject canvasPuntuacion; // Canvas para puntuaciones
    public GameObject canvasJuego; // Canvas del juego
    public GameObject canvasResultado; // Canvas del juego

    public Text textJugadorActual; // Texto que muestra el jugador actual (asignar en inspector)
    private int numJugadores;
    private int[] puntuaciones;
    private int jugadorActual = 0;
    private float volumenInicial = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string json = Resources.Load<TextAsset>("listadoCanciones").text;
        listaCanciones = JsonUtility.FromJson<listaCanciones>(json);

        volumeSlider.value = volumenInicial / 100f; // Si volumenInicial es de 0 a 100, slider debe ser 0.0 - 1.0
        audioSource.volume = volumeSlider.value;    // Asigna el volumen inicial también al AudioSource

        volumeSlider.onValueChanged.AddListener(delegate { audioSource.volume = volumeSlider.value; });

        canvasPuntuacion.SetActive(false);
        canvasJuego.SetActive(true);
        canvasResultado.SetActive(false);

        numJugadores = PlayerPrefs.GetInt("numJugadores", 2); // Por defecto 2 si no se definió antes
        puntuaciones = new int[numJugadores]; // Inicializa a 0

        cargarCancion();
    }

    void cargarCancion()
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

            audioSource.Stop(); // asegura que la nueva canción no suena aún
        }
        else
        {
            Debug.Log("No hay más canciones.");
        }
    }

    public void pauseSong()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
    }

    public void playSong()
    {
        if (audioSource.clip != null)
            audioSource.Play();
    }

    public void contesta()
    {
        audioSource.Stop();
        jugadorActual = 0; // Reset para cada ronda de puntuación
        canvasJuego.SetActive(false);
        canvasPuntuacion.SetActive(true);
        textJugadorActual.text = "Jugador " + (jugadorActual + 1);
    }
    public void añadir1Punto()
    {
        puntuaciones[jugadorActual] += 1;
        cambiarJugador();
    }
    public void añadir2Punto()
    {
        puntuaciones[jugadorActual] += 2;
        cambiarJugador();
    }
    public void añadir3Punto()
    {
        puntuaciones[jugadorActual] += 3;
        cambiarJugador();
    }
    public void añadir4Punto()
    {
        puntuaciones[jugadorActual] += 4;
        cambiarJugador();
    }
    public void añadir5Punto()
    {
        puntuaciones[jugadorActual] += 5;
        cambiarJugador();
    }
    public void sinPuntos()
    {
        cambiarJugador();
    }
    public void cambiarJugador()
    {
        jugadorActual++; // ¡falta esta línea!

        if (jugadorActual >= numJugadores)
        {
            mostrarResultados();
        }
        else
        {
            textJugadorActual.text = "Jugador " + (jugadorActual + 1);
        }
    }

    public void mostrarResultados()
    {
        canvasPuntuacion.SetActive(false);
        canvasResultado.SetActive(true);
        textoResultados.text = ""; // Limpiar antes de mostrar resultados
        for (int i = 0; i < numJugadores; i++)
        {
            textoResultados.text += "La puntuación del jugador " + (i + 1) + " es: " + puntuaciones[i] + " puntos\n";
        }
    }

    public void cerrarResultado()
    {
        canvasResultado.SetActive(false);

        cancionActual++;
        if (cancionActual < listaCanciones.Canciones.Length)
        {
            cargarCancion(); // carga pero NO reproduce
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

            // Cargar escena de ranking final
            SceneManager.LoadScene("PantallaFinal");
        }
    }
}
