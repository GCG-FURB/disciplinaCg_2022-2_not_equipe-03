
#define CG_Debug
#define CG_OpenGL
// #define CG_DirectX

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class SrPalito : SegReta

    {
        private double angulo = 0;
        private double raio = 0;
        // private SegReta obj_segReta;
        public SrPalito(
            char rotulo, 
            Objeto paiRef, 
            double raio,
            double angulo
             ) : base(rotulo, paiRef)
        {
            this.angulo = angulo;
            this.raio = raio;
            Ponto4D pontoInicial = new Ponto4D(0,0,0);
            Ponto4D pontoFinal = Matematica.GerarPtosCirculo(angulo, raio);
            SegReta segReta = this;
            segReta.PontosAdicionar(pontoInicial);
            segReta.PontosAdicionar(pontoFinal);
        }

        private Ponto4D DeslocarPonto(Ponto4D ponto, Ponto4D ptoAntigo){
            ponto.X += ptoAntigo.X;
            ponto.Y += ptoAntigo.Y;
            return ponto;
        }

        public void MoverEsquerda(){
            base.pontosLista[0].X += 2;
            base.pontosLista[1].X += 2;
        }

        public void MoverDireita(){
            base.pontosLista[0].X -= 2;
            base.pontosLista[1].X -= 2;
        }

        public void AumentarRaio(){
            this.raio += 2;
            Ponto4D pontoAntigo = base.pontosLista[1];
            Ponto4D pontoFinal = Matematica.GerarPtosCirculo(this.angulo, this.raio);
            base.PontosAlterar(pontoFinal, 1);
        }

        public void DiminuirRaio(){
            if (this.raio - 2 > 0) {
                this.raio -= 2;
                Ponto4D pontoFinal = Matematica.GerarPtosCirculo(this.angulo, this.raio);
                base.PontosAlterar(pontoFinal, 1);
            }
        }

        public void AumentarAngulo(){
            this.angulo += 2;
            Ponto4D pontoFinal = Matematica.GerarPtosCirculo(this.angulo, this.raio);
            base.PontosAlterar(pontoFinal, 1);
        }

        public void DiminuirAngulo(){
            this.angulo -= 2;
            Ponto4D pontoFinal = Matematica.GerarPtosCirculo(this.angulo, this.raio);
            base.PontosAlterar(pontoFinal, 1);
        }
        protected override void DesenharObjeto()
        {
#if CG_OpenGL && !CG_DirectX
            GL.Begin(base.PrimitivaTipo);
            foreach (Ponto4D pto in pontosLista)
            {
                GL.Vertex2(pto.X, pto.Y);
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
            retorno = "__ Objeto SrPalito: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }
#endif

    }
}