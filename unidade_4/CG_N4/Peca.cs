using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Peca : ObjetoGeometria
    {

        public int bolinhas_esquerda = 0;
        public int bolinhas_direita = 0;
        public bool is_carroca;
        private Transformacao4D matriz;
        public Peca(char rotulo, Objeto paiRef, int bolinhas_esquerda, int bolinhas_direita) : base(rotulo, paiRef)
        {

            // // DESSA MANEIRA A PEÇA FICA DE LADO
            // base.PontosAdicionar(new Ponto4D(-1.5, -0.7, 0.3)); // PtoA listaPto[0] 
            // base.PontosAdicionar(new Ponto4D(1.5, -0.7, 0.3)); // PtoB listaPto[1]
            // base.PontosAdicionar(new Ponto4D(1.5, 0.7, 0.3)); // PtoC listaPto[2]
            // base.PontosAdicionar(new Ponto4D(-1.5, 0.7, 0.3)); // PtoD listaPto[3]
            // base.PontosAdicionar(new Ponto4D(-1.5, -0.7, -0.3)); // PtoE listaPto[4]
            // base.PontosAdicionar(new Ponto4D(1.5, -0.7, -0.3)); // PtoF listaPto[5]
            // base.PontosAdicionar(new Ponto4D(1.5, 0.7, -0.3)); // PtoG listaPto[6]
            // base.PontosAdicionar(new Ponto4D(-1.5, 0.7, -0.3)); // PtoH listaPto[7]

            this.CriarPontos();
            this.bolinhas_esquerda = bolinhas_esquerda;
            this.bolinhas_esquerda = bolinhas_direita;

            if (bolinhas_direita == bolinhas_esquerda)
            {
                this.is_carroca = true;

            }
            else
            {
                this.is_carroca = false;
            }

            // DESSA MANEIRA A LINHA DIVISORIA DA PRA USAR COM A PEÇA DE LADO
            //FilhoAdicionar(new DivisoriaPeca(new Ponto4D(-0.1, -0.6, 0.31), 1.2, 0.2, 0));

            // DESSA MANEIRA A LINHA DIVISORIA DA PRA USAR COM A PEÇA EM PÉ
            FilhoAdicionar(new DivisoriaPeca(new Ponto4D(-0.65, -0.1, 0.31), 0.2, 1.31, 0));

        }
        public void SalvarPosicao()
        {
            this.matriz = base.matriz;
        }
        public void ResetarPeca()
        {
            base.matriz = this.matriz;
        }
        public void CriarPontos()
        {
            base.PontosAdicionar(new Ponto4D(-0.7, -1.5, 0.3)); // PtoA listaPto[0]
            base.PontosAdicionar(new Ponto4D(0.7, -1.5, 0.3)); // PtoB listaPto[1]
            base.PontosAdicionar(new Ponto4D(0.7, 1.5, 0.3)); // PtoC listaPto[2]
            base.PontosAdicionar(new Ponto4D(-0.7, 1.5, 0.3)); // PtoD listaPto[3]
            base.PontosAdicionar(new Ponto4D(-0.7, -1.5, -0.3)); // PtoE listaPto[4]
            base.PontosAdicionar(new Ponto4D(0.7, -1.5, -0.3)); // PtoF listaPto[5]
            base.PontosAdicionar(new Ponto4D(0.7, 1.5, -0.3)); // PtoG listaPto[6]
            base.PontosAdicionar(new Ponto4D(-0.7, 1.5, -0.3)); // PtoH listaPto[7]
        }

        class DivisoriaPeca : Objeto
        {

            public readonly double Altura;

            public readonly double Comprimento;

            public readonly double Largura;

            public readonly Ponto4D PontoInicial;

            public DivisoriaPeca(Ponto4D ponto, double altura, double comprimento, double largura) : base('d', null)
            {

                PrimitivaTipo = PrimitiveType.Quads;
                ObjetoCor = new Cor(100, 100, 100);
                PrimitivaTamanho = 1;

                Altura = altura;
                Comprimento = comprimento;
                Largura = largura;
                PontoInicial = ponto;

                BBox.Atribuir(ponto);
                BBox.Atualizar(ponto + new Ponto4D(comprimento, altura, largura));
                BBox.ProcessarCentro();

            }

            protected override void DesenharGeometria()
            {
                DesenharDivisoria();
            }

            private void DesenharDivisoria()
            {
                double x = PontoInicial.X;
                double y = PontoInicial.Y;
                double z = PontoInicial.Z;

                double a = Altura;
                double c = Comprimento;
                double l = Largura;

                GL.Color3(0, 0, 0);

                GL.Begin(PrimitiveType.LineLoop);

                GL.Normal3(0, 0, 1);
                GL.Vertex3(x, y, z);
                GL.Vertex3(x, y + a, z);
                GL.Vertex3(x + c, y + a, z);
                GL.Vertex3(x + c, y, z);

                GL.End();
                GL.Begin(PrimitiveType.LineLoop);

                z -= 0;
                GL.Normal3(0, 0, 1);
                GL.Vertex3(x, y, z);
                GL.Vertex3(x, y, z + l);
                GL.Vertex3(x, y + a, z + l);
                GL.Vertex3(x, y + a, z);

                GL.End();
                GL.Begin(PrimitiveType.LineLoop);

                GL.End();
            }

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
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Normal3(0, 0, -1);
            GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
            GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
                                                                                                // Face de cima (azul)
            GL.Color3(0.0f, 0.0f, 1.0f);
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
            GL.Color3(0.0f, 1.0f, 1.0f);
            GL.Normal3(1, 0, 0);
            GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
            GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
            GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
                                                                                                // Face da esquerda (magenta)
            GL.Color3(1.0f, 0.0f, 1.0f);
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