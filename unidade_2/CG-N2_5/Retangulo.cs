/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Debug
#define CG_OpenGL
// #define CG_DirectX

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
  internal class Retangulo : ObjetoGeometria
  {
    protected byte mudaPrimitiva = 0;
    public Retangulo(char rotulo, Objeto paiRef, Ponto4D ptoInfEsq, Ponto4D ptoSupDir) : base(rotulo, paiRef)
    {
      base.PontosAdicionar(ptoInfEsq);
      base.PontosAdicionar(new Ponto4D(ptoSupDir.X, ptoInfEsq.Y));
      base.PontosAdicionar(ptoSupDir);
      base.PontosAdicionar(new Ponto4D(ptoInfEsq.X, ptoSupDir.Y));
    }
// muda pra Points, Lines, LineLoop, LineStrip, Triangles, TriangleStrip, TriangleFan, Quads, QuadStrip e Polygon.
    public PrimitiveType MudaPrimitiva()
    {
      PrimitiveType pt;

      if(this.mudaPrimitiva > 8)
      {
        mudaPrimitiva = 0;
      }

      switch (mudaPrimitiva)
      {
        case 0: 
          pt = PrimitiveType.Points;
          break;
        case 1: 
          pt = PrimitiveType.Lines;
          break;
        case 2: 
          pt = PrimitiveType.LineLoop;
          break;
        case 3: 
          pt = PrimitiveType.LineStrip;
          break;
        case 4: 
          pt = PrimitiveType.Triangles;
          break;
        case 5: 
          pt = PrimitiveType.TriangleStrip;
          break;
        case 6: 
          pt = PrimitiveType.TriangleFan;
          break;
        case 7: 
          pt = PrimitiveType.Quads;
          break;
        case 8: 
          pt = PrimitiveType.QuadStrip;
          break;
        default:
          pt = PrimitiveType.Lines;
          break;
      }
        mudaPrimitiva++;
        return pt;
    }
    protected override void DesenharObjeto()
    {
#if CG_OpenGL && !CG_DirectX
      GL.Begin(base.PrimitivaTipo);
      /*
      foreach (Ponto4D pto in pontosLista)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      */
      GL.Color3(1.0f,0.0f,1.0f);
      GL.Vertex2(pontosLista[0].X, pontosLista[0].Y);
      GL.Color3(0.0f,1.0f,1.0f);
      GL.Vertex2(pontosLista[1].X, pontosLista[1].Y);
      GL.Color3(1.0f,1.0f,0.0f);
      GL.Vertex2(pontosLista[2].X, pontosLista[2].Y);
      GL.Color3(0.0f,0.0f,0.0f);
      GL.Vertex2(pontosLista[3].X, pontosLista[3].Y);

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
      retorno = "__ Objeto Retangulo: " + base.rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }
#endif

  }
}