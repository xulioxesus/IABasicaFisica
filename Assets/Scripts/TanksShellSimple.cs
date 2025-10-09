using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script sinxelo para mover un proxectil (shell) adiante no seu eixo local Z.
// A velocidade determína pola variable pública `speed` (unidades por segundo).
public class TanksShellSimple : MonoBehaviour {

    // Velocidade de movemento (unidades por segundo).
    public float speed = 1.0f;

    void Update() {

        // Move cara adiante segundo o eixo local Z multiplicando por Time.deltaTime
        // para manter a velocidade consistente entre distintos FPS.
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }
}
