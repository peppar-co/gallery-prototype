// StencilViewer
// Simple script that renders the four different stencils at the end of each frame, over-drawing anything on screen.
// Providing visualisation of the actual stencil portals.

using UnityEngine;

namespace peppar
{
    [ExecuteInEditMode]
    public class StencilViewer : MonoBehaviour
    {
        public Shader[] _stencilViewers;
        public Color[] _colours = new Color[] { new Color(1, 0, 1) };

        private void Start()
        {

        }

        private void OnPostRender()
        {
            if (_stencilViewers == null || _stencilViewers.Length == 0)
                return;

            Material tmpMat = new Material(_stencilViewers[0]);
            tmpMat.SetColor("_Color", _colours[0]);

            GL.PushMatrix();
            GL.LoadOrtho();

            for (int i = 0; i < _stencilViewers.Length; i++)
            {
                if (_stencilViewers[i] == null)
                    continue;

                tmpMat.shader = _stencilViewers[i];
                tmpMat.SetColor("_Color", _colours[i] != null ? _colours[i] : new Color(1, 0, 1));
                tmpMat.SetPass(0);

                GL.Begin(GL.QUADS);
                GL.TexCoord2(0, 0);
                GL.Vertex3(0.0F, 0.0F, 0);
                GL.TexCoord2(0, 1);
                GL.Vertex3(0.0F, 1.0F, 0);
                GL.TexCoord2(1, 1);
                GL.Vertex3(1.0F, 1.0F, 0);
                GL.TexCoord2(1, 0);
                GL.Vertex3(1.0F, 0.0F, 0);
                GL.End();
            }
            GL.PopMatrix();
        }
    }
}