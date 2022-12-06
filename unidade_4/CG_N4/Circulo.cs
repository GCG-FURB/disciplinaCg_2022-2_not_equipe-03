
#define CG_Debug
#define CG_OpenGL
// #define CG_DirectX

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Circulo : ObjetoGeometria
    {

        Ponto4D ptoCentral;
        int qtdPontos;
        double raio;
        bool corVermelho;
        public Circulo(
            Ponto4D ptoCentral,
            double raio,
            int qtdPontos,
            bool corVermelho = false
             ) : base('c', null)
        {
            this.ptoCentral = ptoCentral;
            this.qtdPontos = qtdPontos;
            this.raio = raio;
            base.PrimitivaTamanho = 5;
            this.corVermelho = corVermelho;
            // Distancia entre os pontos
            this.AtualizarPontos();
        }

        private void AtualizarPontos()
        {
            double distanciaPontos = 360 / this.qtdPontos;
            double anguloAnterior = 0;
            // calcular pontos
            for (int i = 0; i < this.qtdPontos; i++)
            {
                Ponto4D ponto = Matematica.GerarPtosCirculo(anguloAnterior, this.raio);
                base.PontosAdicionar(this.DeslocarPonto(ponto, this.ptoCentral));
                anguloAnterior += distanciaPontos;
            };
        }
        public void AtualizarPtoCentral(Ponto4D pto)
        {
            this.ptoCentral = pto;
            this.PontosRemoverTodos();
            this.AtualizarPontos();
        }

        private Ponto4D DeslocarPonto(Ponto4D ponto, Ponto4D ptoCentral)
        {
            ponto.X += ptoCentral.X;
            ponto.Y += ptoCentral.Y;
            ponto.Z += ptoCentral.Z;
            return ponto;
        }

        protected override void DesenharObjeto()
        {
#if CG_OpenGL && !CG_DirectX
            GL.Begin(base.PrimitivaTipo);
            foreach (Ponto4D pto in pontosLista)
            {
                if (this.corVermelho)
                {
                    GL.Color3(1.0f, 0.0f, 0.0f);

                }
                else
                {
                    GL.Color3(0.0f, 0.0f, 0.0f);
                }
                GL.Vertex3(pto.X, pto.Y, pto.Z);
            }
            GL.End();
#elif CG_DirectX && !CG_OpenGL
    Console.WriteLine(" .. Coloque aqui o seu código em DirectX");
#elif (CG_DirectX && CG_OpenGL) || (!CG_DirectX && !CG_OpenGL)
    Console.WriteLine(" .. ERRO de Render - escolha OpenGL ou DirectX !!");
#endif
        }

        //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
#if CG_Debug
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Circulo: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }
#endif

    }
}