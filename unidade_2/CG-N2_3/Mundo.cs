/**
  Autor: Dalton Solano dos Reis
**/

//#define CG_Privado // código do professor.
#define CG_Gizmo  // debugar gráfico.
#define CG_Debug // debugar texto.
#define CG_OpenGL // render OpenGL.
//#define CG_DirectX // render DirectX.

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
  class Mundo : GameWindow
  {
    private static Mundo instanciaMundo = null;

    private Mundo(int width, int height) : base(width, height) { }

    public static Mundo GetInstance(int width, int height)
    {
      if (instanciaMundo == null)
        instanciaMundo = new Mundo(width, height);
      return instanciaMundo;
    }

    private CameraOrtho camera = new CameraOrtho();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private ObjetoGeometria objetoSelecionado = null;
    private char objetoId = '@';
    private bool bBoxDesenhar = false;
    int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
    private bool mouseMoverPto = false;
    // private Retangulo obj_Retangulo;
    private Circulo obj_Circulo;
    private Circulo obj_Circulo2;
    private Circulo obj_Circulo3;
    private SegReta obj_SegReta;
    private SegReta obj_SegReta2;
    private SegReta obj_SegReta3;
#if CG_Privado
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      camera.xmin = -300; camera.xmax = 300; camera.ymin = -300; camera.ymax = 300;

      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra teclas usadas. ");

      objetoId = Utilitario.charProximo(objetoId);
      Ponto4D ptoCentral = new Ponto4D(-100, -100, 0);
      Ponto4D ptoCentral2 = new Ponto4D(100, -100, 0);
      Ponto4D ptoCentral3 = new Ponto4D(0, 100, 0);

      obj_Circulo = new Circulo(objetoId, null, ptoCentral, 100, 72);
      obj_Circulo.ObjetoCor.CorR = 0; obj_Circulo.ObjetoCor.CorG = 0; obj_Circulo.ObjetoCor.CorB = 0;
      obj_Circulo.PrimitivaTipo = PrimitiveType.Points;
      obj_Circulo.PrimitivaTamanho = 5;
      objetosLista.Add(obj_Circulo);
      objetoSelecionado = obj_Circulo;

      objetoId = Utilitario.charProximo(objetoId);
      obj_Circulo2 = new Circulo(objetoId, null, ptoCentral2, 100, 72);
      obj_Circulo2.ObjetoCor.CorR = 0; obj_Circulo2.ObjetoCor.CorG = 0; obj_Circulo2.ObjetoCor.CorB = 0;
      obj_Circulo2.PrimitivaTipo = PrimitiveType.Points;
      obj_Circulo2.PrimitivaTamanho = 5;
      objetosLista.Add(obj_Circulo2);
      objetoSelecionado = obj_Circulo2;

      objetoId = Utilitario.charProximo(objetoId);
      obj_Circulo3 = new Circulo(objetoId, null, ptoCentral3, 100, 72);
      obj_Circulo3.ObjetoCor.CorR = 0; obj_Circulo3.ObjetoCor.CorG = 0; obj_Circulo3.ObjetoCor.CorB = 0;
      obj_Circulo3.PrimitivaTipo = PrimitiveType.Points;
      obj_Circulo3.PrimitivaTamanho = 5;
      objetosLista.Add(obj_Circulo3);
      objetoSelecionado = obj_Circulo3;

      objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta = new SegReta(objetoId, null, ptoCentral, ptoCentral2);
      obj_SegReta.ObjetoCor.CorR = 0; obj_SegReta.ObjetoCor.CorG = 255; obj_SegReta.ObjetoCor.CorB = 255;
      obj_SegReta.PrimitivaTipo = PrimitiveType.Lines;
      obj_SegReta.PrimitivaTamanho = 5;
      objetosLista.Add(obj_SegReta);
      objetoSelecionado = obj_SegReta;

      objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta2 = new SegReta(objetoId, null, ptoCentral2, ptoCentral3);
      obj_SegReta2.ObjetoCor.CorR = 0; obj_SegReta2.ObjetoCor.CorG = 255; obj_SegReta2.ObjetoCor.CorB = 255;
      obj_SegReta2.PrimitivaTipo = PrimitiveType.Lines;
      obj_SegReta2.PrimitivaTamanho = 5;
      objetosLista.Add(obj_SegReta2);
      objetoSelecionado = obj_SegReta2;

      objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta3 = new SegReta(objetoId, null, ptoCentral3, ptoCentral);
      obj_SegReta3.ObjetoCor.CorR = 0; obj_SegReta3.ObjetoCor.CorG = 255; obj_SegReta3.ObjetoCor.CorB = 255;
      obj_SegReta3.PrimitivaTipo = PrimitiveType.Lines;
      obj_SegReta3.PrimitivaTamanho = 5;
      objetosLista.Add(obj_SegReta3);
      objetoSelecionado = obj_SegReta3;

#if CG_Privado
      objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta = new Privado_SegReta(objetoId, null, new Ponto4D(50, 150), new Ponto4D(150, 250));
      obj_SegReta.ObjetoCor.CorR = 255; obj_SegReta.ObjetoCor.CorG = 255; obj_SegReta.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_SegReta);
      objetoSelecionado = obj_SegReta;

      objetoId = Utilitario.charProximo(objetoId);
      obj_Circulo = new Privado_Circulo(objetoId, null, new Ponto4D(100, 300), 50);
      obj_Circulo.ObjetoCor.CorR = 255; obj_Circulo.ObjetoCor.CorG = 255; obj_Circulo.ObjetoCor.CorB = 255;
      obj_Circulo.PrimitivaTipo = PrimitiveType.Points;
      obj_Circulo.PrimitivaTamanho = 5;
      objetosLista.Add(obj_Circulo);
      objetoSelecionado = obj_Circulo;
#endif
#if CG_OpenGL
      GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
#endif
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
#if CG_OpenGL
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);
#endif
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);
#if CG_OpenGL
      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();
#endif
#if CG_Gizmo      
      Sru3D();
#endif
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();
#if CG_Gizmo
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
#endif
      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.H)
        Utilitario.AjudaTeclado();
      else if (e.Key == Key.Escape)
        Exit();
      else if (e.Key == Key.V)
        mouseMoverPto = !mouseMoverPto;   //TODO: falta atualizar a BBox do objeto
      
      else if (e.Key == Key.E && camera.xmax <= 600)
        camera.PanEsquerda();

      else if (e.Key == Key.D && camera.xmin >= -600)
        camera.PanDireita();

      else if (e.Key == Key.C && camera.ymin >= -600)
        camera.PanCima();

      else if (e.Key == Key.B && camera.ymax <= 600)
        camera.PanBaixo();

      else if (e.Key == Key.I && camera.xmin < -100 && camera.ymin < -100)
        camera.ZoomIn();

      else if (e.Key == Key.O && camera.xmin > -400 && camera.ymin > -400)
        camera.ZoomOut();
      else
        Console.WriteLine(" __ Tecla não implementada.");
    }

    //TODO: não está considerando o NDC
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = 600 - e.Position.Y; // Inverti eixo Y
      if (mouseMoverPto && (objetoSelecionado != null))
      {
        objetoSelecionado.PontosUltimo().X = mouseX;
        objetoSelecionado.PontosUltimo().Y = mouseY;
      }
    }

#if CG_Gizmo
    private void Sru3D()
    {
#if CG_OpenGL
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      // GL.Color3(1.0f,0.0f,0.0f);
      GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      // GL.Color3(0.0f,1.0f,0.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(255), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      // GL.Color3(0.0f,0.0f,1.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
#endif
    }
#endif    
  }
  class Program
  {
    static void Main(string[] args)
    {
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N2";
      window.Run(1.0 / 60.0);
    }
  }
}
