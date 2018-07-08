using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
	public float ScrollSpeed;
	private Transform _t;
	private Vector3 _resetPosition;

    private void Start()
	{
		_t = GetComponent<Transform>();
		_resetPosition = new Vector3(0,0,0);
	}

	private void Update()
	{
		var prevPos = _t.transform.position;

		if (prevPos.y <= -5)
		{
			_t.transform.position = _resetPosition;
		}
		else
		{
			_t.transform.position = new Vector3(
				prevPos.x,
				prevPos.y - ScrollSpeed,
				0
			);
		}
	}
}
