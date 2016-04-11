using UnityEngine;
using System.Collections;
using System.Linq;

namespace peppar
{
    public class InteractionLerpMaterialColorController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private InteractionType[] _interactionOnTypes = new InteractionType[] { InteractionType.MouseClick, InteractionType.TouchClick, InteractionType.LookAt };

        [SerializeField]
        private InteractionState _interactionOnState = InteractionState.OnStart;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField]
        private Color _color = Color.red;

        [SerializeField]
        private bool _loopColorLerp = false;

        [SerializeField]
        [Range(0, 100)]
        private float _lerpSpeed = 10;

        private Color _initialColor, _wantedColor;

        private bool _doColorLerp = false;

        private float _lerpDelta = 0;


        private bool _interactionOnHover = false;

        public MeshRenderer MeshRenderer
        {
            get
            {
                return _meshRenderer;
            }

            set
            {
                _meshRenderer = value;
                _initialColor = _meshRenderer.material.color;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        public Color WantedColor
        {
            get
            {
                return _wantedColor;
            }

            set
            {
                _wantedColor = value;
            }
        }

        public void Interaction(InteractionState interactionState, InteractionType interactionType)
        {
            if (_interactionOnTypes.Contains(interactionType) == false)
            {
                return;
            }

            if (_interactionOnState == interactionState && _interactionOnHover == false)
            {
                _doColorLerp = true;

                if(interactionState == InteractionState.OnHover)
                {
                    _interactionOnHover = true;
                }
            }

            if (_interactionOnState == InteractionState.OnHover
                && interactionState == InteractionState.OnStop)
            {
                MeshRenderer.material.color = _initialColor;
                _lerpDelta = 0;
                _doColorLerp = false;

                _interactionOnHover = false;
            }
        }

        private void LerpMaterialColor()
        {
            if (_doColorLerp == false)
            {
                return;
            }

            _lerpDelta += _lerpSpeed / 100;

            MeshRenderer.material.color = Color.Lerp(MeshRenderer.material.color, WantedColor, _lerpDelta);

            if (_lerpDelta >= 1)
            {
                WantedColor = WantedColor == _initialColor ? Color : _initialColor;
                _lerpDelta = 0;

                if (_loopColorLerp == false)
                {
                    _doColorLerp = false;
                }
            }
        }

        protected override void Awake()
        {
            if (MeshRenderer == null)
            {
                MeshRenderer = GetComponent<MeshRenderer>();
            }

            if (MeshRenderer == null)
            {
                Debug.LogWarning("InteractionLerpMaterialColorController: MeshRender is null");
            }
        }

        protected override void Start()
        {
            if (MeshRenderer.material != null)
            {
                _initialColor = MeshRenderer.material.color;
            }

            WantedColor = Color;
        }

        protected override void Update()
        {

        }

        private void FixedUpdate()
        {
            LerpMaterialColor();
        }
    }
}