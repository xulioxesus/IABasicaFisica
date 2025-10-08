using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move este GameObject cara adiante no seu eixo local Z.
// O movemento realízase en LateUpdate, polo que sucede despois de Update cada frame.
// Útil para seguir/axustar a posición tras outras actualizacións que ocorran en Update.
public class TimeLateUpdateMove : MonoBehaviour
{
    // Velocidade de movemento en unidades por segundo. Pódese axustar desde o Inspector.
    public float speed = 0.5f;

    // Chamado cada frame despois de Update.
    // Movemos o transform en Z local multiplicando a velocidade polo delta de tempo do frame.
    void LateUpdate()
    {
        // Move en Z local: adiante segundo a rotación do obxecto.
        this.transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
