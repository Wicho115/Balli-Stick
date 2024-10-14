using UnityEngine;

namespace _Balli_Stick.Miscellaneous
{
    public class TestMaterial : MonoBehaviour
    {
        private static readonly int _Color = Shader.PropertyToID("_Color");
    
        [SerializeField] private Color color;
        [SerializeField] private Renderer matRenderer;

        private MaterialPropertyBlock _mpb;
        public MaterialPropertyBlock Mpb
        {
            get
            {
                if(_mpb == null)
                {
                    _mpb = new();
                }

                return _mpb;
            }
        }

        private void Start()
        {
            if (!matRenderer) return;
            Mpb.SetColor(_Color, color);
            matRenderer.SetPropertyBlock(Mpb);
        }

        private void OnValidate()
        {
            if (!matRenderer) return;
            Mpb.SetColor(_Color, color);
            matRenderer.SetPropertyBlock(Mpb);
        }
    }
}
