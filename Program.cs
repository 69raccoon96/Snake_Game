using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Snake
{
    class Program
    {
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOZORDER = 0x0004;
        const UInt32 SWP_NOREDRAW = 0x0008;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_FRAMECHANGED = 0x0020;
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        const UInt32 SWP_HIDEWINDOW = 0x0080;
        const UInt32 SWP_NOCOPYBITS = 0x0100;
        const UInt32 SWP_NOOWNERZORDER = 0x0200;
        const UInt32 SWP_NOSENDCHANGING = 0x0400;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public static char[,] pole = new char[30, 100];
        public static bool IsFood = false;
        public static Random random = new Random();
        public static int foodX, foodY;
        public static int x = 15;
        public static int y = 50;
        public static Queue<int> snakeX = new Queue<int>();
        public static Queue<int> snakeY = new Queue<int>();
        public static void Main(string[] args)
        {
            
            IntPtr ConsoleHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            const UInt32 WINDOW_FLAGS = SWP_SHOWWINDOW;
            SetWindowPos(ConsoleHandle, HWND_NOTOPMOST, 200, 200, 850, 540, WINDOW_FLAGS);
            Console.BufferHeight = 31; 
            foodX = random.Next(2, 28);
            foodY = random.Next(2, 98);
            pole[15, 49] = '█';
            pole[15, 50] = '█';
            pole[15, 48] = '█';
            snakeX.Enqueue(15);
            snakeX.Enqueue(15);
            snakeX.Enqueue(15);
            snakeY.Enqueue(48);
            snakeY.Enqueue(49);
            snakeY.Enqueue(50);
            FullingPole();
            DrawPole();      
            Thread.Sleep(500);
            SetFood();
            GoRight();
            Console.ReadKey();
        }

        public static void GoRight()
        {         
            
            while (pole[x, y + 1] != '▐')
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.W)
                    {
                        GoUp();
                        break;
                    }
                    else if (key.Key == ConsoleKey.S)
                    {
                        GoDown();
                        break;
                    }
                }
                y += 1;
                pole[x, y] = '█';
                pole[snakeX.Dequeue(), snakeY.Dequeue()] = ' ';
                snakeX.Enqueue(x);
                snakeY.Enqueue(y);
                DrawPole();
                CheckFood();
                Thread.Sleep(100);


            }
            
        }

        public static void GoLeft()
        {

            while (pole[x, y - 1] != '▐')
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.W)
                    {
                        GoUp();
                        break;
                    }
                    else if (key.Key == ConsoleKey.S)
                    {
                        GoDown();
                        break;
                    }
                }
                y -= 1;
                pole[x, y] = '█';
                pole[snakeX.Dequeue(), snakeY.Dequeue()] = ' ';
                snakeX.Enqueue(x);
                snakeY.Enqueue(y);
                DrawPole();
                CheckFood();
                Thread.Sleep(100);

            }
        }
        public static void GoDown()
        {
            while (pole[x + 1, y] != '▀')
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.D)
                    {
                        GoRight();
                        break;
                    }
                    else if (key.Key == ConsoleKey.A)
                    {
                        GoLeft();
                        break;
                    }
                }
                x += 1;
                pole[x, y] = '█';
                pole[snakeX.Dequeue(), snakeY.Dequeue()] = ' ';
                snakeX.Enqueue(x);
                snakeY.Enqueue(y);
                DrawPole();
                CheckFood();
                Thread.Sleep(100);
            }
        }
        public static void GoUp()
        {
            while (pole[x - 1, y] != '▀')
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.D)
                    {
                        GoRight();
                        break;
                    }
                    else if (key.Key == ConsoleKey.A)
                    {
                        GoLeft();
                        break;
                    }
                }
                x -= 1;
                pole[x, y] = '█';
                pole[snakeX.Dequeue(), snakeY.Dequeue()] = ' ';
                snakeX.Enqueue(x);
                snakeY.Enqueue(y);
                DrawPole();
                CheckFood();
                Thread.Sleep(100);
            }

        }
        public static void CheckFood()
        {
            if (x==foodX&& y==foodY)
                {
                int[] SX = snakeX.ToArray();
                int[] SY = snakeY.ToArray();
                if(SX[snakeX.Count-2] < SX[snakeX.Count-1] )
                {
                    x += 1;
                    pole[x, y] = '█';
                    snakeX.Enqueue(x);
                    snakeY.Enqueue(y);
                }
                else if (SX[snakeY.Count - 2] > SX[snakeY.Count - 1])
                {
                    x -= 1;
                    pole[x, y] = '█';
                    snakeX.Enqueue(x);
                    snakeY.Enqueue(y);
                }
                else if (SY[snakeY.Count - 2] < SY[snakeY.Count - 1])
                {
                    y += 1;
                    pole[x, y] = '█';
                    snakeX.Enqueue(x);
                    snakeY.Enqueue(y);
                }
                else if (SY[snakeY.Count - 2] > SY[snakeY.Count - 1] )
                {
                    y -= 1;
                    pole[x, y] = '█';
                    snakeX.Enqueue(x);
                    snakeY.Enqueue(y);
                }
                else
                {
                    Console.WriteLine("oops");
                    Thread.Sleep(5000);
                }
                IsFood = false;
                SetFood();
            }
            
        }
        public static void SetFood()
        {
            if (IsFood == false)
            {
                foodX = random.Next(2, 28);
                foodY = random.Next(2, 98);
                if (pole[foodX, foodY] != '▀' && pole[foodX, foodY] != '▐')
                    pole[foodX, foodY] = '*';
                else
                    SetFood();
                IsFood = true;
            }
        }
        public static void DrawPole()
        {
            Console.Clear();
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Console.Write(pole[i, j]);
                }
                Console.WriteLine();
            }
        }
        public static void FullingPole()
        {
            for (int i = 1; i < 100; i++)
                pole[0, i] = '▀';

            for (int i = 1; i < 100; i++)
                pole[29, i] = '▀';

            for (int i = 0; i < 30; i++)
                pole[i, 1] = '▐';

            for (int i = 0; i < 30; i++)
                pole[i, 99] = '▐';
            
        }
        
    }
}
