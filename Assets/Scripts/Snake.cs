using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    //Uma lista responsável por guardar os seguimentos da cobra
    private List<Transform> _segments = new List<Transform>();
    //Localização dos segmentos
    public Transform segmentPrefab;
    //Direção dos seguimento apontando para direita
    public Vector2 direction = Vector2.right;
    //Tamanho inicial da cobra
    public int initialSize = 4;
        
    //Função Start
    private void Start()
    {
        //Função que reinicia a cobra para o tamanho inicial
        ResetState();
    }
    
    // Atualiza a cada frame
    private void Update()
    {
        // Caso esteja andando na direção X pode se mover para cima ou para baixo
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        // Caso esteja andando na direção Y pode se mover para direita ou esquerda
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        // Atualiza a cordenada de cada segmento da cobra
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        // Arredonda a localização do segmento da cobra
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }
    
    // Função que faz a cobra ganhar mais um segmento 
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    
    // Função que reinicia o tamanho e localização da cobra
    public void ResetState()
    {
        //A cobra nasce no centro do mapa direcionada para a direita
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        // Deleta todos os segmentos da cobra e passam do tamanho inicial
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        // Limpa a lista dos segmentos da cobra e insere novos
        _segments.Clear();
        _segments.Add(this.transform);

        // Cresce a cobra para chegar ao tamanho inicial
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //Assim que colide inicia essa função
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Caso o colisor possua a  tag comida ativa a função Grow
        if (other.tag == "Food") {
            Grow();
        } 
        // Caso o colisor possua a tag obstaculo a cobra é reiniciada
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
