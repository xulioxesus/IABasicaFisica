using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move este GameObject cara adiante no seu eixo local Z.
// O movemento realízase en FixedUpdate, polo que está sincronizado co paso fixo de física.
public class TimeFixedUpdateMove : MonoBehaviour
{
    // Velocidade de movemento en unidades por segundo. Pódese axustar desde o Inspector.
    // Valores altos poden facer que o obxecto atravese colisións se non se usa física.
    public float speed = 0.5f;

    // Chamado a intervalos fixos por Unity (fixos para a física).
    // Aquí movemos o transform en Z local multiplicando a velocidade polo delta de tempo.
    // Nota: en FixedUpdate, Time.deltaTime devolve o paso fixo; tamén se pode usar Time.fixedDeltaTime
    // para deixar isto máis explícito.
    void FixedUpdate()
    {
        // Move en Z local: adiante segundo a rotación do obxecto.
        this.transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
