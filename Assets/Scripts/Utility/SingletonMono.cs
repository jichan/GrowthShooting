using UnityEngine;
using System.Collections;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
	protected static T instance;
	public static T Instance {
		get {
			if (instance == null)
			{
				instance = (T)FindObjectOfType(typeof(T));

				if (instance == null)
				{
					GameObject obj = new GameObject(typeof(T).Name);
					instance = obj.AddComponent<T>();
					Debug.LogWarning(typeof(T) + "is nothing");
				}
			}

			return instance;
		}
	}

	protected virtual void Awake()
	{
		CheckInstance();
	}

	protected bool CheckInstance()
	{
		if (instance == null)
		{
			instance = (T)this;
			return true;
		} else if (Instance == this)
		{
			return true;
		}

		Destroy(this);
		return false;
	}
}