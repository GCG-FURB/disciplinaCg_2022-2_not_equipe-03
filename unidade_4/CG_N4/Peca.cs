using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;
using System.Collections.Generic;

namespace gcgcg
{
    internal class Peca : ObjetoGeometria
    {

        public int bolinhas_esquerda = 0;
        public int bolinhas_direita = 0;

        public float transalacao_inicial = 0;
        public bool is_conectado_esquerda = false;
        public bool is_conectado_direita = false;
        public bool is_carroca;

        private Retangulo divisoria;
        private Transformacao4D matriz;
        public Peca(char rotulo, Objeto paiRef, int bolinhas_esquerda, int bolinhas_direita) : base(rotulo, paiRef)
        {
            this.CriarPontos();
            this.bolinhas_esquerda = bolinhas_esquerda;
            this.bolinhas_direita = bolinhas_direita;

            if (bolinhas_direita == bolinhas_esquerda)
            {
                this.is_carroca = true;

            }
            else
            {
                this.is_carroca = false;
            }

            this.divisoria = new Retangulo(new Ponto4D(0, 2, 0.901), new Ponto4D(2.5, 2.5, 0.901));
            FilhoAdicionar(this.divisoria);

            switch (this.bolinhas_esquerda)
            {
                case 0:
                    break;
                case 1:
                    FilhoAdicionar(new Circulo(new Ponto4D(1.25, 3.5, 0.901), 0.2, 72, true)); // Ponto do meio
                    break;
                case 2:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 4, 0.901), 0.2, 72, true)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 3, 0.901), 0.2, 72, true)); // Ponto de baixo direita
                    break;
                case 3:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 4, 0.901), 0.2, 72, true)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(1.25, 3.5, 0.901), 0.2, 72, true)); // Ponto do meio
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 3, 0.901), 0.2, 72, true)); // Ponto de baixo direita
                    break;
                case 4:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 4, 0.901), 0.2, 72, true)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 4, 0.901), 0.2, 72, true)); // Ponto de cima direita
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 3, 0.901), 0.2, 72, true)); // Ponto de baixo esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 3, 0.901), 0.2, 72, true)); // Ponto de baixo direita
                    break;
                case 5:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 4, 0.901), 0.2, 72, true)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 4, 0.901), 0.2, 72, true)); // Ponto de cima direita
                    FilhoAdicionar(new Circulo(new Ponto4D(1.25, 3.5, 0.901), 0.2, 72, true)); // Ponto do meio
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 3, 0.901), 0.2, 72, true)); // Ponto de baixo esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 3, 0.901), 0.2, 72, true)); // Ponto de baixo direita
                    break;
                case 6:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 4, 0.901), 0.2, 72, true)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 4, 0.901), 0.2, 72, true)); // Ponto de cima direita
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 3.5, 0.901), 0.2, 72, true)); // Ponto de baixo mei
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 3.5, 0.901), 0.2, 72, true)); // Ponto de cima meio
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 3, 0.901), 0.2, 72, true)); // Ponto de baixo esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 3, 0.901), 0.2, 72, true)); // Ponto de baixo direita
                    break;

            }

            switch (this.bolinhas_direita)
            {
                case 0:
                    break;
                case 1:
                    FilhoAdicionar(new Circulo(new Ponto4D(1.25, 1, 0.901), 0.2, 72)); // Ponto do meio
                    break;
                case 2:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 1.5, 0.901), 0.2, 72)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 0.5, 0.901), 0.2, 72)); // Ponto de baixo direita
                    break;
                case 3:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 1.5, 0.901), 0.2, 72)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(1.25, 1, 0.901), 0.2, 72)); // Ponto do meio
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 0.5, 0.901), 0.2, 72)); // Ponto de baixo direita
                    break;
                case 4:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 1.5, 0.901), 0.2, 72)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 1.5, 0.901), 0.2, 72)); // Ponto de cima direita
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 0.5, 0.901), 0.2, 72)); // Ponto de baixo esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 0.5, 0.901), 0.2, 72)); // Ponto de baixo direita
                    break;
                case 5:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 1.5, 0.901), 0.2, 72)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 1.5, 0.901), 0.2, 72)); // Ponto de cima direita
                    FilhoAdicionar(new Circulo(new Ponto4D(1.25, 1, 0.901), 0.2, 48)); // Ponto do meio
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 0.5, 0.901), 0.2, 72)); // Ponto de baixo esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 0.5, 0.901), 0.2, 72)); // Ponto de baixo direita
                    break;
                case 6:
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 1.5, 0.901), 0.2, 72)); // Ponto de cima esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 1.5, 0.901), 0.2, 72)); // Ponto de cima direita
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 1, 0.901), 0.2, 72)); // Ponto de baixo mei
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 1, 0.901), 0.2, 72)); // Ponto de cima meio
                    FilhoAdicionar(new Circulo(new Ponto4D(0.5, 0.5, 0.901), 0.2, 72)); // Ponto de baixo esquerda
                    FilhoAdicionar(new Circulo(new Ponto4D(2, 0.5, 0.901), 0.2, 72)); // Ponto de baixo direita
                    break;

            }
        }

        public void AtualizarPrimitivaDivisoria()
        {
            this.divisoria.ProximaPrimitiva();
        }
        public void SalvarPosicao()
        {
            this.matriz = base.matriz;
        }
        public void ResetarPeca()
        {
            this.Translacao(this.transalacao_inicial, 'x');
            Console.WriteLine("peca - " + this.transalacao_inicial);
        }
        public void CriarPontos()
        {
            base.PontosAdicionar(new Ponto4D(0, 0, 0)); // PtoA listaPto[0]
            base.PontosAdicionar(new Ponto4D(0, 0, 0.9)); // PtoB listaPto[1]
            base.PontosAdicionar(new Ponto4D(0, 4.5, 0.9)); // PtoD listaPto[3]
            base.PontosAdicionar(new Ponto4D(0, 4.5, 0)); // PtoC listaPto[2]
            base.PontosAdicionar(new Ponto4D(2.5, 0, 0)); // PtoA listaPto[0]
            base.PontosAdicionar(new Ponto4D(2.5, 0, 0.9)); // PtoB listaPto[1]
            base.PontosAdicionar(new Ponto4D(2.5, 4.5, 0.9)); // PtoD listaPto[3]
            base.PontosAdicionar(new Ponto4D(2.5, 4.5, 0)); // PtoC listaPto[2]
            // Peça (2.5 X 4.5 X 0.9)
        }

        public void GirarPecaLogica()
        {
            int bolinha = this.bolinhas_direita;
            this.bolinhas_direita = this.bolinhas_esquerda;
            this.bolinhas_esquerda = bolinha;
        }

        protected override void DesenharObjeto()
        {       // Sentido anti-horário
            GL.Begin(PrimitiveType.Quads);
            // Face da frente (vermelho)
            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Normal3(0, 0, 1);
            GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
            GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
            GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
            GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
                                                                                                // Face do fundo (verde)
            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Normal3(0, 0, -1);
            GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
            GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
                                                                                                // Face de cima (azul)
            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Normal3(0, 1, 0);
            GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
            GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
            GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
                                                                                                // Face de baixo (amarelo)
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.Normal3(0, -1, 0);
            GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
            GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
            GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
                                                                                                // Face da direita (ciano)
            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Normal3(1, 0, 0);
            GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
            GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
            GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
                                                                                                // Face da esquerda (magenta)
            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Normal3(-1, 0, 0);
            GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
            GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
            GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
            GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.End();

        }

        //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Peça: " + base.rotulo + "\n";
            retorno += "Bolinhas esquerda: " + this.bolinhas_esquerda + "\n";
            retorno += "Bolinhas direita: " + this.bolinhas_direita + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }
    }
}