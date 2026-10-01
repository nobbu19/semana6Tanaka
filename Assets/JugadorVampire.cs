using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorVampire : MonoBehaviour
{ 
    [SerializeField] private Image overlayOscuro;
    [Tooltip("Qué tan oscuro se pone cuando la vida llega a 0 (0 = nada, 1 = negro total).")]
    [SerializeField] private float alphaMaximo = 0.6f;

    [Header("Movimiento")]
    [SerializeField] private float velocidad = 5f;

    [Header("Disparo")]
    [SerializeField] private GameObject prefabProjectil;
    [SerializeField] private float cadenciaDisparo = 0.3f;
    private float proximoDisparo = 0f;

    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 10;
    private int vidaActual;

    [Header("Al morir")]
    [Tooltip("Nombre exacto de la escena a la que se va al morir (debe estar agregada en Build Settings).")]
    [SerializeField] private string escenaAlMorir;

    private Rigidbody2D rb;
    private Camera camara;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        camara = Camera.main;
        vidaActual = vidaMaxima;
        ActualizarOverlay();
    }

    private void Update()
    {
        // Disparo con clic izquierdo
        if (Mouse.current != null && Mouse.current.leftButton.isPressed && Time.time >= proximoDisparo)
        {
            Disparar();
            proximoDisparo = Time.time + cadenciaDisparo;
        }
    }

    private void FixedUpdate()
    {
        // Movimiento con WASD / flechas
        Vector2 mov = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) mov.y += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) mov.y -= 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) mov.x -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) mov.x += 1;
        }
        mov = mov.normalized;

        // MovePosition respeta las colisiones del Rigidbody2D
        rb.MovePosition(rb.position + mov * velocidad * Time.fixedDeltaTime);
    }

    private void Disparar()
    {
        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = camara.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;

        Vector2 direccion = (mouseWorld - transform.position).normalized;

        GameObject proj = Instantiate(prefabProjectil, transform.position, Quaternion.identity);

        ProjectilJugador normal = proj.GetComponent<ProjectilJugador>();
        if (normal != null)
        {
            normal.Inicializar(direccion);
            return;
        }

        ProjectilJugador deBoss = proj.GetComponent<ProjectilJugador>();
        if (deBoss != null)
        {
            deBoss.Inicializar(direccion);
        }
    }

    public void Curar(int cantidad = 1)
    {
        vidaActual = Mathf.Min(vidaActual + cantidad, vidaMaxima);
        Debug.Log("Vida del jugador: " + vidaActual);
        ActualizarOverlay();
    }

    public void RecibirDanio(int cantidad = 1)
    {
        vidaActual -= cantidad;
        Debug.Log("Vida del jugador: " + vidaActual);
        ActualizarOverlay();

        if (vidaActual <= 0)
            Morir();
    }

    private void ActualizarOverlay()
    {
        if (overlayOscuro == null) return;

        float porcentajeVidaPerdida = 1f - ((float)vidaActual / vidaMaxima);
        float nuevoAlpha = porcentajeVidaPerdida * alphaMaximo;

        Color c = overlayOscuro.color;
        c.a = Mathf.Clamp01(nuevoAlpha);
        overlayOscuro.color = c;
    }

    private void Morir()
    {
        Debug.Log("¡El jugador ha muerto!");

        if (!string.IsNullOrEmpty(escenaAlMorir))
        {
            SceneManager.LoadScene(escenaAlMorir);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}