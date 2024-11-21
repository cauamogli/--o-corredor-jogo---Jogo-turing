using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SideScrollingCamera : MonoBehaviour
{
    public Transform trackedObject; // Objeto a ser seguido
    public float height = 6.5f; // Altura normal da câmera
    public float undergroundHeight = -9.5f; // Altura quando está no subsolo
    public float undergroundThreshold = 0f; // Ponto que define se está no subsolo
    public float smoothSpeed = 0.125f; // Suavidade do movimento da câmera

    private float targetHeight; // Altura alvo da câmera

    private void Start()
    {
        if (trackedObject == null)
        {
            Debug.LogError("O Tracked Object não foi atribuído. Por favor, configure no Inspector.");
            enabled = false; // Desativa o script para evitar mais erros
            return;
        }

        targetHeight = height; // Define a altura inicial como a altura normal
    }

    private void LateUpdate()
    {
        if (trackedObject == null) return;

        // Define a posição alvo no eixo X (câmera segue suavemente)
        Vector3 desiredPosition = transform.position;
        desiredPosition.x = Mathf.Max(transform.position.x, trackedObject.position.x); // Segue apenas em frente
        desiredPosition.y = Mathf.Lerp(transform.position.y, targetHeight, smoothSpeed); // Suavemente ajusta a altura

        // Atualiza a posição da câmera
        transform.position = desiredPosition;
    }

    public void SetUnderground(bool underground)
    {
        // Define a altura alvo com base na lógica underground
        targetHeight = underground ? undergroundHeight : height;
    }
}
