using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform currentTarget;

    private void Start()
    {
        pointA.parent = null;
        pointB.parent = null;
        currentTarget = pointB;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == pointA)
            {
                currentTarget = pointB;
                Flip(false);
            }
            else
            {
                currentTarget = pointA;
                Flip(true);
            }
        }
    }

    void Flip(bool faceLeft)
    {
        Vector3 newScale = transform.localScale;
        if (faceLeft)
            newScale.x = -Mathf.Abs(newScale.x);
        else
            newScale.x = Mathf.Abs(newScale.x);

        transform.localScale = newScale;
    }
}