using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class pantallaFinal : MonoBehaviour
{
    public Text rankingText; 

    void Start()
    {
        int numJugadores = PlayerPrefs.GetInt("numJugadores", 2);
        List<KeyValuePair<string, int>> listaPuntuaciones = new List<KeyValuePair<string, int>>();

        for (int i = 0; i < numJugadores; i++)
        {
            int puntuacion = PlayerPrefs.GetInt("PuntuacionJugador" + i, 0);
            string nombre = PlayerPrefs.GetString("NombreEquipo_" + i, "Jugador " + (i + 1));
            listaPuntuaciones.Add(new KeyValuePair<string, int>(nombre, puntuacion));
        }

        listaPuntuaciones.Sort((a, b) => b.Value.CompareTo(a.Value));

        rankingText.text = "Ranking Final:\n";
        foreach (var par in listaPuntuaciones)
        {
            rankingText.text += par.Key + ": " + par.Value + " puntos\n";
        }
    }

    public void volverInicio() => SceneManager.LoadScene("Main");
}