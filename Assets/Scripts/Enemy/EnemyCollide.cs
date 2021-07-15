using UnityEngine;

public class EnemyCollide : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Explosion explosion))
		{
			Destroy(gameObject);
		}
	}
}
