using UnityEngine;

public class enemigoSeguir : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    [Header("Daño al jugador")]
    [SerializeField] private int danioAlJugador = 1;
    [SerializeField] private float tiempoEntreDanios = 1f;
    private float proximoDanio = 0f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        // Movimiento físico hacia el jugador
        Vector2 nuevaPos = Vector2.MoveTowards(
            rb.position,
            player.position,
            speed * Time.fixedDeltaTime
        );
        rb.MovePosition(nuevaPos);

        // Voltear sprite según dirección
        if (sr != null)
        {
            float dx = player.position.x - transform.position.x;
            if (Mathf.Abs(dx) > 0.01f)
                sr.flipX = dx < 0;
        }
    }

    // Contacto continuo con el jugador (ambos con Collider2D NO trigger)
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && Time.time >= proximoDanio)
        {
            JugadorVampire jugador = col.gameObject.GetComponent<JugadorVampire>();
            if (jugador != null)
            {
                jugador.RecibirDanio(danioAlJugador);
                proximoDanio = Time.time + tiempoEntreDanios;
                GameObject.Destroy(gameObject); // Destruir enemigo después de infligir daños
            }
        }
    }
}