using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Puzle_Consola
{
    class Program
    {
        //Publicas
        #region
        const int Dimension = 4;
        static int[,] Respaldo_Matriz_Lectura = new int[Dimension, Dimension];
        static int[] Respaldo_int_lectura;
        static string N_Archivo_1= "Ranura_1.txt";
        static string N_Pos_1 = "Posicion1.txt";
        static int posf, posc, num_mov;
        static string Nickname = "";
        #endregion
        //Adicionales
        #region
        public static void Interfaz_Menu(int lc, int lf, string Titulo)
        {
            Console.Clear();
            Console.Title = Titulo;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetWindowSize(lc, lf);
            Console.SetBufferSize(lc, lf);
        }
        public static void Posicion(int lc, int lf)
        {
            Console.SetCursorPosition(lc, lf);
        }
        public static void Imprimir_Movimi()
        {
            Posicion(10, 20); Console.WriteLine("El Jugador {0} lleva {1} Movimientos", Nickname, num_mov);
        }
        public static void Menu()
        {
            Interfaz_Menu(Console.LargestWindowWidth / 3, Console.LargestWindowHeight - 20, "Menu Principal");
            int op = 0;
            Posicion(20, 4); Console.Write("1.-Nueva Partida");
            Posicion(20, 6); Console.Write("2.-Cargar Partida");
            Posicion(20, 8); Console.Write("3.-Marcadores");
            Posicion(20, 10); Console.Write("4.-Salir");
            Posicion(20, 12);Console.Write("Escoga una Opcion:");
            Posicion(40, 12);op = int.Parse(Console.ReadLine());
            int[,] Mtr = new int[Dimension, Dimension];
            switch (op)
            {
                case 1:
                    Mtr = GeneraPuzzle();
                    Jugar(Mtr, 3, 3,0 );
                    Menu();
                    break;
                case 2:
                    Sub_Menu();
                    Menu();
                    break;
                case 3:
                    Console.Clear();
                    Presentar_Marcadores();
                    Console.ReadKey();
                    break;
                case 4:
                    Environment.Exit(-1);
                    break;
            }
            Console.ReadKey();
        }
        public static void Sub_Menu()
        {
            Interfaz_Menu(Console.LargestWindowWidth / 3, Console.LargestWindowHeight - 20, "Menu Secundario-Cargar Parttida");
            int op = 0;
            Posicion(20, 4); Console.Write("1.-Ranura 1");
            Posicion(20, 6); Console.Write("2.-Ranura 2");
            Posicion(20, 8); Console.Write("3.-Ranura 3");
            Posicion(20, 10); Console.Write("4.-Volver");
            Posicion(20, 12); Console.Write("Escoga una Opcion:");
            Posicion(40, 12); op = int.Parse(Console.ReadLine());
            int[,] Mtr = new int[Dimension, Dimension];
            switch (op)
            {
                case 1:
                    N_Archivo_1 = "Ranura_1.txt";
                    N_Pos_1 = "Posicion1.txt";
                    Leer_Archivo();
                    Leer_Posiciones();
                    Mtr = Respaldo_Matriz_Lectura;
                    Jugar(Mtr, posf, posc,num_mov);
                    break;
                case 2:
                    N_Archivo_1 = "Ranura_2.txt";
                    N_Pos_1 = "Posicion2.txt";
                    Leer_Archivo();
                    Leer_Posiciones();
                    Mtr = Respaldo_Matriz_Lectura;
                    Jugar(Mtr, posf, posc,num_mov);
                    break;
                case 3:
                    N_Archivo_1 = "Ranura_3.txt";
                    N_Pos_1 = "Posicion3.txt";
                    Leer_Archivo();
                    Leer_Posiciones();
                    Mtr = Respaldo_Matriz_Lectura;
                    Jugar(Mtr, posf, posc,num_mov);
                    break;
                case 4:
                    break;
            }
            Console.ReadKey();

        }
        #endregion
        //Cuerpo Juego
        #region
        public static void Jugar(int [,] Matriz,int fila,int columna,int movimientos)
        {
            Interfaz_Menu(Console.LargestWindowWidth / 3, Console.LargestWindowHeight - 20, "Jugador ");
            int[,] Puzzle = Matriz;
            int F = fila, C = columna;
            Presentar(Puzzle);
            Imprimir_Movimi();

            while (true)
            {
                #region 
                bool Salir = false;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        //Console.WriteLine("Flecha Hacia Arriba");
                        if (F < 3)
                        {
                            Puzzle[F, C] = Puzzle[F + 1, C];
                            Puzzle[++F, C] = 0;
                            movimientos++;
                            num_mov = movimientos;
                        }

                        break;
                    case ConsoleKey.DownArrow:
                        //Console.WriteLine("Flecha Hacia Abajo");
                        if (F > 0)
                        {
                            Puzzle[F, C] = Puzzle[F - 1, C];
                            Puzzle[--F, C] = 0;
                            movimientos++;
                            num_mov = movimientos;

                        }
                        break;
                    case ConsoleKey.RightArrow:
                        //Console.WriteLine("Flecha Hacia Derecha");
                        if (C > 0)
                        {
                            Puzzle[F, C] = Puzzle[F, C - 1];
                            Puzzle[F, --C] = 0;
                            movimientos++;
                            num_mov = movimientos;

                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        //Console.WriteLine("Flecha Hacia Izquierda");
                        if (C < 3)
                        {
                            Puzzle[F, C] = Puzzle[F, C + 1];
                            Puzzle[F, ++C] = 0;
                            movimientos++;
                            num_mov = movimientos;

                        }
                        break;
                    case ConsoleKey.Escape:
                        Salir = true;
                        break;
                }
                #endregion
                if (Salir)
                {
                    Posicion(0, 10); Console.WriteLine("Desea Guardar Su Progreso...");
                    Posicion(0, 11); Console.WriteLine("Presione La Tecla S Para Guardar...");
                    Posicion(0, 12); Console.WriteLine("Presione La Tecla N Para Salir...");
                    while (true)
                    {
                        bool ban = false;
                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.S:
                                Posicion(0, 16); Console.WriteLine("Presione A Para Guardar En la Ranura A...");
                                Posicion(0, 17); Console.WriteLine("Presione B Para Guardar En la Ranura B...");
                                Posicion(0, 18); Console.WriteLine("Presione C Para Guardar En la Ranura C...");
                                ban = true;
                                #region
                                while (true)
                                {
                                    bool ban2 = false;
                                    switch (Console.ReadKey().Key)
                                    {
                                        case ConsoleKey.A:
                                            ban2 = true;
                                            N_Archivo_1 = "Ranura_1.txt";
                                            N_Pos_1 = "Posicion1.txt";
                                            Escribir_Respaldo_En_Vector(Puzzle);
                                            Guardar_Posiciones(F, C,movimientos);
                                            break;
                                        case ConsoleKey.B:
                                            ban2 = true;
                                            N_Archivo_1 = "Ranura_2.txt";
                                            N_Pos_1 = "Posicion2.txt";
                                            Escribir_Respaldo_En_Vector(Puzzle);
                                            Guardar_Posiciones(F, C,movimientos);
                                            break;
                                        case ConsoleKey.C:
                                            ban2 = true;
                                            N_Archivo_1 = "Ranura_3.txt";
                                            N_Pos_1 = "Posicion3.txt";
                                            Escribir_Respaldo_En_Vector(Puzzle);
                                            Guardar_Posiciones(F, C,movimientos);
                                            break;
                                    }
                                    if (ban2)
                                    {
                                        break;
                                    }
                                }
                                #endregion
                                break;
                            case ConsoleKey.N:
                                ban = true;
                                break;
                        }
                        if (ban)
                        {
                            break;
                        }
                    }
                    break;
                }
                Presentar(Puzzle);
                if (Ganar(Puzzle))
                {
                    Console.WriteLine("");
                    Console.WriteLine("FELICIDADES!!! Ganó el juego");
                    Console.ReadKey();
                }
            }
        }
        public static int[,] GeneraPuzzle()
        {
            int p, contador = 0;
            int[,] Matriz = new int[4, 4];
            int[] aux = new int[15];
            Random r = new Random();
            while (true)
            {
                p = r.Next(1, 16);
                if (aux[p - 1] == 0)
                {
                    Matriz[contador / 4, contador % 4] = p;
                    aux[p - 1] = 1;
                    contador++;
                }
                if (contador >= 15) break;
            }
            return Matriz;
        }
        public static void Presentar(int[,] Matriz)
        {
            Console.Clear();
            int x = 10;
            int y = 5;
            for (int i = 0; i <= Matriz.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= Matriz.GetUpperBound(1); j++)
                {
                    if (Matriz[i, j] != 0)
                    {
                        Posicion(x, y); Console.Write("{0,5}", Matriz[i, j]);
                    }
                    else
                    {
                        Posicion(x, y); Console.Write("     ");
                    }
                    x += 5;
                }
                x = 10;
                y++;
            }
            Imprimir_Movimi();

        }
        public static bool Ganar(int[,] Matriz)
        {
            for (int i = 0; i <= Matriz.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= Matriz.GetUpperBound(1); j++)
                {
                    if (i == 3 && j == 3) return true;
                    if (Matriz[i, j] != i * 4 + j + 1) return false;
                }
            }
            return false;
        }
        #endregion
        //Cuerpo Escritura Archivo
        #region
        public static void Escribir_Respaldo_En_Vector(int [,] Matriz)
        {
            int[] Respaldo_Escritura = new int[Matriz.Length];

            //Guardar Matriz en Vector
            int k = 0;
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    Respaldo_Escritura[k] = Matriz[i, j];
                    k++;
                }
            }
            //Guardar Vector en Un Txt
            StreamWriter Escribir = new StreamWriter(N_Archivo_1);
            for (int i = 0; i < Respaldo_Escritura.Length; i++)
            {
                Escribir.Write("{0}*", Respaldo_Escritura[i]);
            }
            Escribir.Close();
        }
        public static void Guardar_Posiciones(int f,int c,int pos)
        {
            StreamWriter Escribir = new StreamWriter(N_Pos_1);
            Escribir.WriteLine(f);
            Escribir.WriteLine(c);
            Escribir.WriteLine(pos);
            Escribir.Close();
        }
        #endregion
        //Cuerpo Lectura Archivo
        #region 
        public static void Leer_Archivo()
        {
            int pos = 0;
            string[] Respaldo_Lectura;

            //Leer Archivo
            StreamReader leer = new StreamReader(N_Archivo_1);
            string frase = leer.ReadLine();
            Respaldo_Lectura = frase.Split('*');
            leer.Close();

            //Convertir Vector String a Int
            Respaldo_int_lectura = new int[Respaldo_Lectura.Length - 1];
            for (pos = 0; pos < Respaldo_Lectura.Length - 1; pos++)
            {
                Respaldo_int_lectura[pos] = Convert.ToInt32(Respaldo_Lectura[pos]);
            }

            //Almacenar Datos De Vector en Matriz
            pos = 0;
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    Respaldo_Matriz_Lectura[i, j] = Respaldo_int_lectura[pos];
                    pos++;
                }
            }
        }
        public static void Leer_Posiciones()
        {
            StreamReader Leer = new StreamReader(N_Pos_1);
            posf = Convert.ToInt32(Leer.ReadLine());
            posc = Convert.ToInt32(Leer.ReadLine());
            num_mov = Convert.ToInt32(Leer.ReadLine());
            Leer.Close();
        }
        #endregion
        //Cuerpo Marcadores
        static void Presentar_Marcadores()
        {
            Interfaz_Menu(Console.LargestWindowWidth / 2, Console.LargestWindowHeight - 10, "Menu Secundario-Cargar Parttida");

            int cont = 0;
            string[] vectorpuntaje = new string[2];
            string cadena;
            StreamReader lectura;
            Posicion(30, 4);
            Console.Write("TABLA DE PUNTAJES");
            Posicion(21, 6);
            Console.Write("NICK                              Movimnientos");
                lectura = File.OpenText("Marcadores.txt");
                cadena = lectura.ReadLine();
                while (cadena != null && cont < 10)
                {
                    vectorpuntaje = cadena.Split('*');
                    Posicion(21, cont * 2 + 8);
                    Console.Write("{0}) {1}", cont + 1, vectorpuntaje[0]);
                    Posicion(61, cont * 2 + 8);
                    Console.Write(vectorpuntaje[1]);
                    cadena = lectura.ReadLine();
                    cont++;
                }
                lectura.Close();
            
        }
        //Cuerpo del Codigo
        static void Main(string[] args)
        {
            Menu();
        }
    }
}
