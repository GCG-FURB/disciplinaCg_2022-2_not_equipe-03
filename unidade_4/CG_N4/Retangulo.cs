/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Debug
#define CG_OpenGL

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Collections.Generic;

namespace gcgcg
{
    internal class Retangulo : ObjetoGeometria
    {
        private int primitiva = 0;
        public Retangulo(Ponto4D ptoInfEsq, Ponto4D ptoSupDir) : base('r', null)
        {
            base.PontosAdicionar(ptoInfEsq);
            base.PontosAdicionar(new Ponto4D(ptoSupDir.X, ptoInfEsq.Y, ptoInfEsq.Z));
            base.PontosAdicionar(ptoSupDir);
            base.PontosAdicionar(new Ponto4D(ptoInfEsq.X, ptoSupDir.Y, ptoSupDir.Z));
            base.PrimitivaTamanho = 3;
        }

        public List<Ponto4D> getListaPontos() {
            return base.pontosLista;
        }

        public void ProximaPrimitiva()
        {
            if (this.primitiva >= 15)
            {
                this.primitiva = 0;
                base.PrimitivaTipo = 0;
            }
            else
            {
                this.primitiva += 1;
                base.PrimitivaTipo += 1;
            }
        }

        protected override void DesenharObjeto()
        {
            GL.Begin(base.PrimitivaTipo);
            foreach (Ponto4D pto in pontosLista)
            {
                GL.Color3(0.0f, 0.0f, 0.0f);
                GL.Vertex3(pto.X, pto.Y, pto.Z);
            }
            GL.End();
        }

        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Retangulo: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }
    }
}