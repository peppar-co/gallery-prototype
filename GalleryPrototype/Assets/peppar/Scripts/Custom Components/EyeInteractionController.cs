using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace peppar
{
	public class EyeInteractionController : MonoBehaviour
	{
		[SerializeField]
		private EyeGUIController _eyeGUIController;

		[SerializeField]
		private ObjectDropIn _objectDropIn;

		[SerializeField]
		private Animator _eyeAnimator;

		[SerializeField]
		private List<MeshRenderer> _stencilMeshes = new List<MeshRenderer>();

		[SerializeField]
		private List<Material> _stencilMasks = new List<Material>();

		private List<Material> _defaultStencilMasks = new List<Material>();

		private int _activeStencilIndex;

		public Animator EyeAnimator
		{
			get { return _eyeAnimator; }
			set { _eyeAnimator = value; }
		}

		[SerializeField]
		private float _maxIdleTime;

		private float _blinkTime;

		public void ChangeScene(bool next)
		{
			if (next == true)
			{
				_activeStencilIndex++;
				if (_activeStencilIndex > _stencilMasks.Count - 1)
				{
					_activeStencilIndex = _stencilMasks.Count - 1;
					return;
				}
				ChangeStencil(_activeStencilIndex);
				_objectDropIn.Shuffle = true;
			}

			//if (_activeStencilIndex > _stencilMasks.Count - 1)
			//{

			//}
			else
			{
				_activeStencilIndex--;
				if (_activeStencilIndex < 0)
				{
					_activeStencilIndex = 0;
					return;
				}
				ChangeStencil(_activeStencilIndex);
				_objectDropIn.Shuffle = true;
			}
			//else
			//{
			//	_activeStencilIndex = 0;
			//	SetDefaultStencil();
			//	_objectDropIn.Shuffle = true;
			//}

		}

		public void ResetScene()
		{
			_activeStencilIndex = 0;
			SetDefaultStencil();
			_objectDropIn.Shuffle = true;
		}

		public void ChangeStencil(int index)
		{
			foreach (var mesh in _stencilMeshes)
			{
				mesh.material = _stencilMasks[index];
			}
		}

		private void SetDefaultStencil()
		{
			for (int i = 0; i < _stencilMeshes.Count; i++)
			{
				_stencilMeshes[i].material = _defaultStencilMasks[i];
			}
		}

		private void Blink()
		{
			_eyeAnimator.SetTrigger("Blink");
		}

		private void Awake()
		{
			UnityEngine.Assertions.Assert.IsNotNull(_eyeGUIController);
			UnityEngine.Assertions.Assert.IsNotNull(_eyeAnimator);
			UnityEngine.Assertions.Assert.IsNotNull(_objectDropIn);
			UnityEngine.Assertions.Assert.IsNotNull(_stencilMeshes);
			UnityEngine.Assertions.Assert.IsNotNull(_stencilMasks);
			_maxIdleTime = 3;
			_blinkTime = _maxIdleTime;
			_activeStencilIndex = -1;

			if (_stencilMeshes != null)
			{
				foreach (var mesh in _stencilMeshes)
				{
					_defaultStencilMasks.Add(mesh.material);
				}
			}

		}

		private void Update()
		{
			//toggle gui on hit

			//get raycast from touch (android)       
			for (var i = 0; i < Input.touchCount; ++i)
			{
				if (Input.GetTouch(i).phase == TouchPhase.Began)
				{
					// Construct a ray from the current touch coordinates
					Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

					//utlDebugPrint.Inst.print(Input.GetTouch(i).position.ToString());

					if (Physics.Raycast(ray))
					{
						_eyeGUIController.OpenMenu();
					}
				}
			}

			//mouse interaction for PC debugging
			if (Input.GetMouseButtonDown(0))
			{
				Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

				//Debug.Log(Input.mousePosition);

				if (Physics.Raycast(mRay))
				{
					_eyeGUIController.OpenMenu();
				}
			}

			_maxIdleTime -= Time.deltaTime;
			if (_maxIdleTime < 0)
			{
				_maxIdleTime = Random.RandomRange(.5f, _blinkTime);
				Blink();
			}
		}
	}
}