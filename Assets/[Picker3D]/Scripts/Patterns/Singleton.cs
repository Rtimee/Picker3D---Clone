using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
	#region Variables

	static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null)
				instance = FindObjectOfType<T>();

			return instance;
		}
	}

	#endregion

	#region MonoBehaviour Callbacks

	public virtual void Awake ()
	{
		instance = this as T;
	}

	#endregion	
}