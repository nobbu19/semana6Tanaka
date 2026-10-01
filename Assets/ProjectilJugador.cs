using UnityEngine;

public class ProjectilJugador : MonoBehaviour
{
    [SerializeField] private float velocidad = 12f;
    [SerializeField] private float tiempoVida = 3f;

    [Header("Rotación del sprite")]
    [Tooltip("Grados por segundo que gira la bandera.")]
    [SerializeField] private float velocidadRotacion = 360f;

    private Rigidbody2D rb;
    private Vector2 direccion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Inicializar(Vector2 dir)
    {
        direccion = dir.normalized;
        rb.linearVelocity = direccion * velocidad;
        Destroy(gameObject, tiempoVida);
    }

    private void Update()
    {
        // Gira constantemente sobre su eje Z
        transform.Rotate(0f, 0f, velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Enemigo"))
        {
            Destroy(otro.gameObject);
            Destroy(gameObject);
        }
    }
}