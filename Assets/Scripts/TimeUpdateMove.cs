using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move este GameObject cara adiante no seu eixo local Z.
// O movemento realízase en Update, que se chama cada frame.
// Emprégase Update para movementos non dependentes directamente da física.
public class TimeUpdateMove : MonoBehaviour
{
    // Velocidade de movemento en unidades por segundo. Pódese axustar desde o Inspector.
    public float speed = 0.5f;

    // Chamado unha vez por frame.
    // Multiplicamos pola duración do frame (Time.deltaTime) para movemento consistente entre FPS.
    void Update()
    {
        // Move en Z local (adiante) segundo a rotación do obxecto.
        this.transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
