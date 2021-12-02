using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        //Função que coloca comida de maneira aleatória dentro do jogo
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        //Limite de área aonde a comida aparece
        Bounds bounds = this.gridArea.bounds;

        //Localização onde a comida aparecerá
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Arredonda os números escolhidos para um numero inteiro
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Quando a cobra colidir com a comida ela reaparece em outro lugar(comida)
        RandomizePosition();
    }

}
