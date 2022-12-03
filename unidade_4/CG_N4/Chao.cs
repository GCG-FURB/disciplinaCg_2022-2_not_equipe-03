using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
    internal class Chao : ObjetoGeometria
    {
        public readonly Ponto4D Centro;
        public readonly double Tamanho;

        public Chao(Ponto4D centro, double tamanho) : base(Utilitario.charProximo('@'), null)
        {
            PrimitivaTipo = PrimitiveType.Quads;
            Centro = centro;
            Tamanho = tamanho;
        }
        protected override void DesenharObjeto()
        {
            double x = Tamanho / 2;

            GL.Begin(PrimitivaTipo);

            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Normal3(0.0f, 1.0f, 0.0f);

            GL.Vertex3(Centro.X - x, Centro.Y, Centro.Z + x);
            GL.Vertex3(Centro.X + x, Centro.Y, Centro.Z + x);
            GL.Vertex3(Centro.X + x, Centro.Y, Centro.Z - x);
            GL.Vertex3(Centro.X - x, Centro.Y, Centro.Z - x);

            GL.End();
        }
    }
}