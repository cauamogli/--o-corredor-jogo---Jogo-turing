using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrollingCamera : MonoBehaviour
{
    public Transform trackedObject; // Objeto a ser seguido
    public float height = 6.5f; // Altura normal da c�mera
    public float undergroundHeight = -9.5f; // Altura quando est� no subsolo
    public float undergroundThreshold = 0f; // Ponto que define se est� no subsolo
    public float smoothSpeed = 0.125f; // Suavidade do movimento da c�mera

    private float targetHeight; // Altura alvo da c�mera

    private void Start()
    {
        if (trackedObject == null)
        {
            Debug.LogError("O Tracked Object n�o foi atribu�do. Por favor, configure no Inspector.");
            enabled = false; // Desativa o script para evitar mais erros
            return;
        }

        targetHeight = height; // Define a altura inicial como a altura normal
    }

    private void LateUpdate()
    {
        if (trackedObject == null) return;

        // Define a posi��o alvo no eixo X (c�mera segue suavemente)
        Vector3 desiredPosition = transform.position;
        desiredPosition.x = Mathf.Max(transform.position.x, trackedObject.position.x); // Segue apenas em frente
        desiredPosition.y = Mathf.Lerp(transform.position.y, targetHeight, smoothSpeed); // Suavemente ajusta a altura

        // Atualiza a posi��o da c�mera
        transform.position = desiredPosition;
    }

    public void SetUnderground(bool underground)
    {
        // Define a altura alvo com base na l�gica underground
        targetHeight = underground ? undergroundHeight : height;
    }
}
