using UnityEngine;

public static class Vector3Extension
{
	public static Vector2Int Vector3toVector2Int(this Vector3 vector3)
	{
		return new Vector2Int((int)Mathf.Round(vector3.x), (int)Mathf.Round(vector3.y));
	}

	public static Vector2 Vector3toVector2(this Vector3 vector3)
	{
		return new Vector2(vector3.x, vector3.y);
	}
}
