using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Animator controlador;
    private bool isGrounded;

    public float forcaHorizontal = 5f;
    public float forcaVertical = 10f; // Nova variável para controlar a força do movimento vertical
    public float impulso = 12f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        controlador = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimento horizontal
        float eixoX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(eixoX * forcaHorizontal, rb.velocity.y);

        // Movimento vertical
        float eixoY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, eixoY * forcaVertical);

        // Verifica se o jogador pode pular
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, impulso);
        }

        // Configurações do Animator
        controlador.SetBool("moverDireita", eixoX > 0);
        controlador.SetBool("moverEsquerda", eixoX < 0);
        controlador.SetBool("abaixar", Input.GetKey(KeyCode.DownArrow));

        // Ajuste do collider quando abaixado
        if (Input.GetKey(KeyCode.DownArrow))
        {
            col.size = new Vector2(0.18f, 0.16f); // Reduz a altura do collider
            col.direction = CapsuleDirection2D.Vertical;
        }
        else
        {
            col.size = new Vector2(0.18f, 0.3f); // Restaura o tamanho original do collider
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o jogador está no chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Verifica se o jogador saiu do chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
