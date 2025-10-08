using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move este GameObject ao longo do eixo Z global (polo que substitúe a posición Z) en función
// do tempo real transcorrido desde que o script comezou a executar.
// Emprégase Time.realtimeSinceStartup para medir o tempo real (non afectado por Time.timeScale).
public class TimeSecondsUpdate : MonoBehaviour
{
    // Marca de tempo inicial (en segundos) tomada con Time.realtimeSinceStartup.
    float timeStartOffset = 0;

    // Indica se xa capturamos a marca de tempo inicial.
    bool gotStartTime = false;

    // Velocidade en unidades por segundo cos que se multiplica o tempo transcorrido.
    // Pódese axustar desde o Inspector.
    public float speed = 0.5f;

    // Chamado unha vez por frame. Ao primeiro Update capturamos o tempo de inicio.
    // A cada frame actualizamos a posición do transform substituíndo a coordenada Z
    // por (tempo_real_transcorrido * speed).
    //
    // Nota: ao usar Time.realtimeSinceStartup, o movemento non se detén cando Time.timeScale = 0
    // (por exemplo, durante unha pausa). Se queres que o movemento respectase o tempo do xogo,
    // usa Time.time ou acumula Time.deltaTime.
    void Update()
    {
        // Se é o primeiro Update, gardamos o instante de inicio.
        if (!gotStartTime)
        {
            timeStartOffset = Time.realtimeSinceStartup;
            gotStartTime = true;
        }

        // Calculamos o tempo transcorrido e actualizamos só a coordenada Z da posición.
        float elapsed = Time.realtimeSinceStartup - timeStartOffset;
        this.transform.position = new Vector3(this.transform.position.x,
                                              this.transform.position.y,
                                              elapsed * speed);
    }
}
