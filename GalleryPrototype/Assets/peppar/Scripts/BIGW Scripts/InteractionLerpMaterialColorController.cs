using UnityEngine;
using System.Collections;
using System;

namespace peppar
{
    public class InteractionLerpMaterialColorController : BehaviourController, InteractionFunctionality
    {
        [SerializeField]
        private InteractionState _lerpColorOnState = InteractionState.Start;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField]
        private Color _color = Color.red;

        [SerializeField]
        private bool _onlyLerpColorOnInteraction;

        [SerializeField]
        private bool _loopColorLerp = false;

        [SerializeField]
        [Range(0, 100)]
        private float _lerpSpeed = 10;

        private Color _initialColor, _wantedColor;

        private bool _doColorLerp = false;

        private float _lerpDelta = 0;

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
            if (_onlyLerpColorOnInteraction == false && interactionState == _lerpColorOnState)
            {
                _doColorLerp = true;
            }
            else if (interactionState == InteractionState.Start)
            {
                _doColorLerp = true;
            }

            if (_onlyLerpColorOnInteraction && _lerpColorOnState != interactionState && interactionState == InteractionState.Stop)
            {
                MeshRenderer.material.color = _initialColor;
                _lerpDelta = 0;
                _doColorLerp = false;
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