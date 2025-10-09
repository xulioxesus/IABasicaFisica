using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script simple para destruír un proxectil (shell) pasado un tempo.
// Ao iniciar, programa a destrución deste GameObject despois de 3 segundos.
// Útil para limpar proxectís que non colisionaron ou para evitar acumulación de obxectos.
// Nota: para xogo con moitas balas, considera usar un object pool en lugar de Destroy para mellorar o rendemento.
public class TanksShellDestroy : MonoBehaviour
{
    void Start()
    {
        // Destrúe este GameObject despois de 3 segundos.
        Destroy(this.gameObject, 3);
    }
}
