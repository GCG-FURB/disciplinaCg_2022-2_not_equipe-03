#define CG_Gizmo
// #define CG_Privado

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

        private CameraPerspective camera = new CameraPerspective();
        protected List<Objeto> objetosLista = new List<Objeto>();
        private ObjetoGeometria objetoSelecionado = null;
        private char objetoId = '@';
        private String menuSelecao = "";
        private char menuEixoSelecao = 'z';
        private float deslocamento = 1;
        private bool bBoxDesenhar = false;
        Domino domino = new Domino();

        private void ResetarCamera()
        {
            Vector3 vector = Vector3.Zero;
            vector.Y = 3;
            vector.Z = 17;

            camera.Eye = vector;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.ResetarCamera();

            Console.WriteLine(" --- Ajuda / Teclas: ");
            Console.WriteLine(" [  H     ] mostra teclas usadas. ");

            this.domino.CriarPecas(objetosLista);

            objetoSelecionado = this.domino.PegarPecaAtual();

            Ponto4D pontoFundo = new Ponto4D(Utilitario.RetornaMetro(20), 0);
            objetosLista.Add(new Chao(pontoFundo, Utilitario.RetornaMetro(60)));

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(camera.Fovy, Width / (float)Height, camera.Near, camera.Far);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 modelview = Matrix4.LookAt(camera.Eye, camera.At, camera.Up);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
#if CG_Gizmo
            Sru3D();
#endif
            if (domino.Pecas.Count == 0)
            {
                Console.WriteLine("FIM DE JOGO!");
                Exit();
            }
            for (var i = 0; i < objetosLista.Count; i++)
                objetosLista[i].Desenhar();
            if (bBoxDesenhar && (objetoSelecionado != null))
                objetoSelecionado.BBox.Desenhar();
            this.SwapBuffers();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            // Console.Clear(); //TODO: não funciona.
            if (e.Key == Key.H) Utilitario.AjudaTeclado();
            else if (e.Key == Key.Escape) Exit();
            //--------------------------------------------------------------
            else if (e.Key == Key.Number9)
                objetoSelecionado = null;                     // desmacar objeto selecionado
            else if (e.Key == Key.B)
                bBoxDesenhar = !bBoxDesenhar;     //FIXME: bBox não está sendo atualizada.
            else if (e.Key == Key.E)
            {
                Console.WriteLine("--- Objetos / Pontos: ");
                for (var i = 0; i < objetosLista.Count; i++)
                {
                    Console.WriteLine(objetosLista[i]);
                }
            }
            //--------------------------------------------------------------
            else if (e.Key == Key.X) menuEixoSelecao = 'x';
            else if (e.Key == Key.Y) menuEixoSelecao = 'y';
            else if (e.Key == Key.Z) menuEixoSelecao = 'z';
            else if (e.Key == Key.Minus) deslocamento--;
            else if (e.Key == Key.Plus) deslocamento++;
            else if (e.Key == Key.C) menuSelecao = "[menu] C: Câmera";
            else if (e.Key == Key.O) menuSelecao = "[menu] O: Objeto";
            else if (menuSelecao == "[menu] O: Objeto")
            {
                if (e.Key == Key.Right)
                {
                    this.domino.TrocarPecaAtual("direita");
                    objetoSelecionado = this.domino.PegarPecaAtual();
                }
                else if (e.Key == Key.Left)
                {
                    this.domino.TrocarPecaAtual("esquerda");
                    objetoSelecionado = this.domino.PegarPecaAtual();
                }
                else if (e.Key == Key.A)
                {
                    objetoSelecionado.Rotacao(5, 'x');
                }
                else if (e.Key == Key.S)
                {
                    objetoSelecionado.Rotacao(5, 'y');
                }
                else if (e.Key == Key.I) // inicio da fila
                {
                    bool jogada = this.domino.JogarPeca(true);
                    objetoSelecionado = this.domino.PegarPecaAtual();
                }
                else if (e.Key == Key.F) // fim da fila
                {
                    this.domino.JogarPeca(false);
                    objetoSelecionado = this.domino.PegarPecaAtual();
                }
                else if (e.Key == Key.Q) // troca primitiva
                {
                    this.domino.AtualizarPrimitivaDivisoria();
                }
            }

            // Menu: seleção
            else if (menuSelecao.Equals("[menu] C: Câmera"))
            {
                if (e.Key == Key.R)
                {
                    this.ResetarCamera();
                }
                else
                {
                    camera.MenuTecla(e.Key, menuEixoSelecao, deslocamento);
                }
            }
            else if (menuSelecao.Equals("[menu] O: Objeto"))
            {
                if (objetoSelecionado != null) objetoSelecionado.MenuTecla(e.Key, menuEixoSelecao, deslocamento, bBoxDesenhar);
                else Console.WriteLine(" ... Objeto NÃO selecionado.");
            }

            else
                Console.WriteLine(" __ Tecla não implementada.");

            if (!(e.Key == Key.LShift)) //FIXME: não funciona.
                Console.WriteLine("__ " + menuSelecao + "[" + deslocamento + "]");
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
        }

#if CG_Gizmo
        private void Sru3D()
        {
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
        }
#endif
    }
    class Program
    {
        static void Main(string[] args)
        {
            ToolkitOptions.Default.EnableHighResolution = false;
            Mundo window = Mundo.GetInstance(600, 600);
            window.Title = "CG_N4";
            window.Run(1.0 / 60.0);
        }
    }
}
