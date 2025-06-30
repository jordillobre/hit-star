using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class pantallaFinal : MonoBehaviour
{
    public Text rankingText; // Arrastrar el Text en el Inspector

    void Start()
    {
        int numJugadores = PlayerPrefs.GetInt("numJugadores", 2);
        List<KeyValuePair<int, int>> listaPuntuaciones = new List<KeyValuePair<int, int>>();

        // Recuperar puntuaciones
        for (int i = 0; i < numJugadores; i++)
        {
            int puntuacion = PlayerPrefs.GetInt("PuntuacionJugador" + i, 0);
            listaPuntuaciones.Add(new KeyValuePair<int, int>(i + 1, puntuacion)); // i+1 para Jugador 1,2,3...
        }

        // Ordenar de mayor a menor puntuación
        listaPuntuaciones.Sort((a, b) => b.Value.CompareTo(a.Value));

        // Mostrar en el Text
        rankingText.text = "Ranking Final:\n";
        foreach (var par in listaPuntuaciones)
        {
            rankingText.text += "Jugador " + par.Key + ": " + par.Value + " puntos\n";
        }
    }
    public void volverInicio()
    {
        SceneManager.LoadScene("Main");
    }
}
