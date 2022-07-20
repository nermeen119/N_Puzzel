using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
namespace ConsoleApp1
{          // ret_Mat--ReadFile_GetPath---Print matrix---get_zero----Class node---Is_Same_Parent---get_goal---print path--hamming
           //manhaten ----solve or not
    internal class main
    {
        public class Node
        {
            public int Levels;
            public int Costs;
            public int[,] Matrix;
            public Node Parent_node;
            public int X_blank;
            public int Y_blank;
        }
        static int index1 = 0, index2 = 0;
        public static void Main(string[] args)
        {
            int[,] Matrix_Puzzle = Ret_Mat(ReadFile_GetPath()); 
            Stopwatch stopwatch = new Stopwatch();
            int Size_of_Matrix = Matrix_Puzzle.GetLength(0);
            int[,] Goal_OF_Matrix = Get_Matrix_Goal(Size_of_Matrix);
            bool Solvable_Or_Not = IS_Solvable_Or_Not(Matrix_Puzzle, Size_of_Matrix);
            char Choice1, Choice2;
            if (Solvable_Or_Not)
            {
                Get_Zero(Matrix_Puzzle);
                Console.WriteLine("[1]-A*");
                Console.WriteLine("[2]-BFS");
                Choice1 = Console.ReadLine()[0];
                if (Choice1 == '1')
                {
                    Console.WriteLine("[1]-Hamming");
                    Console.WriteLine("[2]-Manhatten");
                    Choice2 = Console.ReadLine()[0];
                    if (Choice2 == '1')
                    {
                        stopwatch.Start();
                        Solve_A_Star(Matrix_Puzzle, Goal_OF_Matrix, index1, index2, Choice2);
                    }
                    else
                    {
                        stopwatch.Start();
                        Solve_A_Star(Matrix_Puzzle, Goal_OF_Matrix, index1, index2, Choice2);
                    }
                }
                if (Choice1 == '2')
                {
                    stopwatch.Start();
                    Solve_BFS(Matrix_Puzzle, Goal_OF_Matrix, index1, index2);
                }
            }
            else
            {
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("Can't Solve");
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine();
            Main(args);
            Console.WriteLine();
        }
        public static bool Is_Same_Parent(Node Nodes, int i_index, int j_index)
        {
            if (Nodes.Parent_node != null)
            {
                if (i_index == Nodes.Parent_node.X_blank && j_index == Nodes.Parent_node.Y_blank)
                {
                    return true;
                }
            }
            return false;
        }
        public static void Solve_BFS(int[,] intial_Mat, int[,] Goal_Mat, int x_index, int y_index)
        {
            int Size_OF_Directions = 4;
            int[] Rows = { 1, 0, -1, 0 };
            int[] Cols = { 0, -1, 0, 1 };
            Queue<Node> queue = new Queue<Node>();
            Node First_Node = new Node();
            First_Node.Matrix = intial_Mat;
            First_Node = Create_NEW_Node(intial_Mat, x_index, y_index, x_index, y_index, 0, null);
            First_Node.Costs = Hamming_Distance(intial_Mat, Goal_Mat);
            queue.Enqueue(First_Node);
            while (queue.Count != 0)
            {
                Node node = queue.Dequeue();
                if (node.Costs == 0)
                {
                    Print_Path_node(node);
                    Console.WriteLine("# OF Movements = " + node.Levels);
                    return;
                }
                for (int i = 0; i < Size_OF_Directions; i++)
                {
                    if (Safe_MAtrix(node.X_blank + Rows[i], node.Y_blank + Cols[i], node.Matrix.GetLength(0)))
                    {
                        Node child = Create_NEW_Node(node.Matrix, node.X_blank,
                        node.Y_blank, node.X_blank + Rows[i],
                        node.Y_blank + Cols[i],
                        node.Levels + 1, node);
                        child.Costs = Hamming_Distance(child.Matrix, Goal_Mat);

                        if (!(Is_Same_Parent(node, child.X_blank, child.Y_blank)))
                        {
                            queue.Enqueue(child);
                        }
                    }
                }
            }
        }
        public static void Get_Zero(int[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        index1 = i;
                        index2 = j;
                        break;
                    }
                }
            }
        }
        public static string ReadFile_GetPath()
        {
            Console.WriteLine("------------------------ N-Puzzle --------------------\n[1] Sample test cases\n[2] Complete testing\n");
            Console.Write("\nEnter your choice [1-2]:");
            char choice = (char)Console.ReadLine()[0];
            string path = string.Empty;
            switch (choice)
            {
                case '1':
                    Console.WriteLine("[1] Solvable");
                    Console.WriteLine("[2] Un Solvable");
                    char choice1 = (char)Console.ReadLine()[0];
                    switch (choice1)
                    {
                        case '1':
                            #region SAMPLE TEST
                            Console.WriteLine("------------------ You Are In Solvable Now ------------------");
                            Console.Write("Choose From [ 1 ... 6 ] To Run Test Cases : ");
                            char choice2 = (char)Console.ReadLine()[0];
                            switch (choice2)
                            {
                                case '1': //Hamming 15 ms 2 second -->Manhatten 19 ms -->Movements 8
                                    path = "Testcases/Sample/Sample Test/Sample Test/Solvable Puzzles/8 Puzzle (1).txt";
                                    break;
                                case '2': //Hamming 70 ms 2 second -->Manhatten 65 ms -->Movements 20
                                    path = "Testcases/Sample/Sample Test/Sample Test/Solvable Puzzles/8 Puzzle (2).txt";
                                    break;
                                case '3':  //Hamming 44 ms 2 second -->Manhatten 35 ms -->Movements 14
                                    path = "Testcases/Sample/Sample Test/Sample Test/Solvable Puzzles/8 Puzzle (3).txt";
                                    break;
                                case '4': //Hamming 29 ms 2 second -->Manhatten 25 ms -->Movements 5
                                    path = "Testcases/Sample/Sample Test/Sample Test/Solvable Puzzles/15 Puzzle - 1.txt";
                                    break;
                                case '5': //Hamming 56 ms  2 second -->Manhatten 52 ms -->Movements 11
                                    path = "Testcases/Sample/Sample Test/Sample Test/Solvable Puzzles/24 Puzzle 1.txt";
                                    break;
                                case '6': //Hamming 68 ms  2 second -->Manhatten 145 ms -->Movements 24
                                    path = "Testcases/Sample/Sample Test/Sample Test/Solvable Puzzles/24 Puzzle 2.txt";
                                    break;
                                default:
                                    if (Convert.ToInt32(choice2) > 6)
                                        Console.WriteLine("Invalid Choice!");
                                    break;
                            }
                            break;
                        case '2':
                            Console.WriteLine("------------------ You Are In UNSolvable Now ------------------");
                            Console.WriteLine("Choose From [ 1 ... 5 ] To Run Test Cases");
                            char choice3 = (char)Console.ReadLine()[0];
                            switch (choice3)
                            {
                                case '1': 
                                    path = "Testcases/Sample/Sample Test/Sample Test/Unsolvable Puzzles/8 Puzzle - Case 1.txt";
                                    break;
                                case '2': 
                                    path = "Testcases/Sample/Sample Test/Sample Test/Unsolvable Puzzles/8 Puzzle(2) - Case 1.txt";
                                    break;
                                case '3':
                                    path = "Testcases/Sample/Sample Test/Sample Test/Unsolvable Puzzles/8 Puzzle(3) - Case 1.txt";
                                    break;
                                case '4': 
                                    path = "Testcases/Sample/Sample Test/Sample Test/Unsolvable Puzzles/15 Puzzle - Case 2.txt";
                                    break;
                                case '5': 
                                    path = "Testcases/Sample/Sample Test/Sample Test/Unsolvable Puzzles/15 Puzzle - Case 3.txt";
                                    break;
                                default:
                                    if (Convert.ToInt32(choice3) > 5)
                                        Console.WriteLine("wrong choice");
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid Choice!");
                            break;
                    }
                    break;
                #endregion
                case '2':
                    #region COMPLETE TEST

                    Console.WriteLine("[1] Solvable");
                    Console.WriteLine("[2] Un Solvable");
                    Console.WriteLine("[3] V.Large Test ");
                    char choice4 = (char)Console.ReadLine()[0];
                    switch (choice4)
                    {
                        case '1':
                            //Console.WriteLine("to choose manhatten & hamming select 1 to choose manhatten only select 2");
                            Console.WriteLine("------------------ You Are In Solvable Now ------------------");
                            Console.WriteLine("[1] Manhatten & Hamming ");
                            Console.WriteLine("[2] Manhatten Only");
                            char choice5 = (char)Console.ReadLine()[0];
                            switch (choice5)
                            {
                                case '1':
                                    Console.WriteLine("------------------ You Are In Manhatten & Hamming Now ------------------");
                                    Console.WriteLine("Choose From [1 ... 4] To Run Test Cases");
                                    char choice6 = (char)Console.ReadLine()[0];
                                    switch (choice6)
                                    {
                                        case '1': //Hamming 1831 ms  -->Manhatten 1562 ms -->Movements 18
                                            path = "Testcases/Complete/Complete Test/Solvable puzzles/Manhattan & Hamming/50 Puzzle.txt";
                                            break;
                                        case '2': //Hamming 104 ms   -->Manhatten 111 ms  -->Movements 18
                                            path = "Testcases/Complete/Complete Test/Solvable puzzles/Manhattan & Hamming/99 Puzzle - 1.txt";
                                            break;
                                        case '3': //Hamming 228 ms   -->Manhatten 222 ms  -->Movements 38
                                            path = "Testcases/Complete/Complete Test/Solvable puzzles/Manhattan & Hamming/99 Puzzle - 2.txt";
                                            break;
                                        case '4': //Hamming 1514 ms  -->Manhatten  1540 ms  -->Movements 4
                                            path = "Testcases/Complete/Complete Test/Solvable puzzles/Manhattan & Hamming/9999 Puzzle.txt";
                                            break;
                                        default:
                                            Console.WriteLine("Invalid Choice!");
                                            break;
                                    }
                                    break;
                                case '2':
                                    Console.WriteLine("------------------ You Are In Manhatten Only Now ------------------");
                                    Console.WriteLine("Choose From [1 ... 4] To Run Test Cases");
                                    char choice7 = (char)Console.ReadLine()[0];
                                    switch (choice7)
                                    {
                                        case '1': // 3797 ms --> 46 movements
                                            path = "Testcases/Complete/Complete Test/Solvable puzzles/Manhattan Only/15 Puzzle 1.txt";
                                            break;
                                        case '2': // 1416 ms --> 38 movements
                                            path = "Testcases/Complete/Complete Test/Solvable puzzles/Manhattan Only/15 Puzzle 3.txt";
                                            break;
                                        case '3': // 528 ms --> 44 movements
                                            path = "Testcases/Complete/Complete Test/Solvable puzzles/Manhattan Only/15 Puzzle 4.txt";
                                            break;
                                        case '4': // 24381 ms --> 45 movements
                                            path = "Testcases/Complete/Complete Test/Solvable puzzles/Manhattan Only/15 Puzzle 5.txt";
                                            break;
                                        default:
                                            Console.WriteLine("Invalid Choice!");
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case '2':
                            Console.WriteLine("------------------ You Are In UNSolvable Now ------------------");
                            Console.WriteLine("Choose From [1 ... 4] To Run Test Cases");
                            char choice8 = Console.ReadLine()[0];
                            switch (choice8)
                            {
                                case '1':
                                    path = "Testcases/Complete/Complete Test/Unsolvable puzzles/15 Puzzle 1 - Unsolvable.txt";
                                    break;
                                case '2':
                                    path = "Testcases/Complete/Complete Test/Unsolvable puzzles/99 Puzzle - Unsolvable Case 1.txt";
                                    break;
                                case '3':
                                    path = "Testcases/Complete/Complete Test/Unsolvable puzzles/99 Puzzle - Unsolvable Case 2.txt";
                                    break;
                                case '4':
                                    path = "Testcases/Complete/Complete Test/Unsolvable puzzles/9999 Puzzle.txt";
                                    break;
                                default:
                                    Console.WriteLine("Invalid Choice!");
                                    break;
                            }
                            break;
                        case '3': //17509 ms -->movements 56
                            Console.WriteLine("---------- YOU RUN V.LARGE TEST CASE NOW ----------");
                            path = "Testcases/Complete/Complete Test/V. Large test case/TEST.txt";
                            break;
                        default:
                            Console.WriteLine("Invalid Choice!");
                            break;
                    }
                    break;
                #endregion

                default:
                    Console.WriteLine("Invalid Choice!");
                    break;
            }
            return path;
        }
        public static int[,] Ret_Mat(string File_Path)
        {
            StreamReader streamReader = new StreamReader(File_Path);
            int N_size = Convert.ToInt32((streamReader.ReadLine()));
            string[] sarr;
            int[,] matrix = new int[N_size, N_size];
            string First_Line = streamReader.ReadLine();
            for (int i = 0; i < N_size; i++)  
            {
               
                if (First_Line != "")
                {
                    sarr = First_Line.Split(' ');
                           First_Line = "";
                }
                else
                {
                    sarr = streamReader.ReadLine().Split(' ');
                }
                for (int j = 0; j < N_size; j++)
                {
                    matrix[i, j] = Convert.ToInt32(sarr[j]);
                }
            }
            streamReader.Close();
            return matrix;
        }
        public static bool IS_Solvable_Or_Not(int[,] matrix, int Size_of_Matrix)
        {
            int Count_Solve = 0;
            int Index_OF_Zero = 0;
            int Counter = 0;
            int[] arr_Matrix = new int[Size_of_Matrix * Size_of_Matrix];
            for (int i = 0; i < Size_of_Matrix; i++)
            {
                for (int j = 0; j < Size_of_Matrix; j++)
                {
                    arr_Matrix[Counter++] = matrix[i, j];
                }
            }
            for (int i = 0; i < arr_Matrix.Length; i++)
            {
                if (arr_Matrix[i] == 0)
                {
                    Index_OF_Zero = i / Size_of_Matrix;
                    continue;
                }

                for (int j = i + 1; j < arr_Matrix.Length; j++) 
                {
                    if (arr_Matrix[i] > arr_Matrix[j])
                    {
                        if (arr_Matrix[j] != 0)
                        {
                            Count_Solve++;
                        }
                    }
                }
            }
            if (Size_of_Matrix % 2 != 0 && Count_Solve % 2 == 0)
            {
                return true;
            }
            else if (Size_of_Matrix % 2 == 0 && Count_Solve % 2 != 0)
            {
                if (Index_OF_Zero % 2 == 0)
                {
                    return true;
                }
                return false;
            }
            else if (Size_of_Matrix % 2 == 0 && Count_Solve % 2 == 0)
            {
                if (Index_OF_Zero % 2 != 0)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        public static int[,] Get_Matrix_Goal(int SizeofMatrix)
        {
            int[,] Goal_OF_Matrix = new int[SizeofMatrix, SizeofMatrix];

            int Counter = 1;
            int i = 0;
            while (i < SizeofMatrix)
            {
                int j = 0;
                while (j < SizeofMatrix)
                {
                    if (j == SizeofMatrix - 1 && i == SizeofMatrix - 1)
                    {
                        Goal_OF_Matrix[i, j] = 0;
                        break;
                    }
                    Goal_OF_Matrix[i, j] = Counter;
                    Counter++;
                    j++;
                }
                i++;
            }
            return Goal_OF_Matrix;
        }
        public static void Print_Matrices(int[,] MATRIX,int Mat_Size)
        {
            
            for (int i = 0;i <Mat_Size;i++)
            {
                for (int j = 0; j < Mat_Size; j++)
                {
                    Console.Write(MATRIX[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public static int Hamming_Distance(int[,] Intial_Matrix, int[,] Goal_Matrix)
        {
            int Count_Distance = 0;
           
            for (int i=0;i < Intial_Matrix.GetLength(0);i++)
            {
                for (int j = 0; j < Intial_Matrix.GetLength(1); j++)
                {
                    if (!(Goal_Matrix[i, j] == Intial_Matrix[i, j]))
                    {
                        if (Intial_Matrix[i, j] != 0)
                        {
                            Count_Distance++;
                        }
                    }
                }
            }
            return Count_Distance;
        }
        public static int Manhaten_Dist(int[,] Matrix, int size)
        {
            int manhaten_distance = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int Col, Row;
                    int mat = Matrix[i, j] - 1;
                    Row = (mat / size) - i;
                    Col = (mat % size) - j;
                    int Pos = (i * size + j) + 1;
                    if (Matrix[i, j] != 0 && (Pos != Matrix[i, j]))
                    {
                        manhaten_distance += Math.Abs(Row) + Math.Abs(Col);
                    }
                }
            }
            return manhaten_distance;
        }
        public static Node Create_NEW_Node(int[,] Matrix, int x_index, int y_index, int New_x_index, int New_y_index,
            int NumOFlevels, Node Parent_nodes)
        {
            Node node = new Node();
            if (Parent_nodes == null)
            {
                node.Matrix = Matrix;
                node.Parent_node = null;
            }
            else
            {
                node.Parent_node = Parent_nodes;
                //node.matrix = mat;
                //node.matrix = mat
                node.Matrix = new int[Parent_nodes.Matrix.GetLength(0), Parent_nodes.Matrix.GetLength(1)];
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        node.Matrix[i, j] = Matrix[i, j];
                    }
                }
            }
            //int[,] temp = new int[pa.matrix.GetLength(0),pa.matrix.GetLength(1)];
            node.Matrix = SwapOFindices(node.Matrix, x_index, y_index, New_x_index, New_y_index);
            node.Costs = int.MaxValue;
            node.Levels = NumOFlevels;
            node.X_blank = New_x_index;
            node.Y_blank = New_y_index;
            return node;
        }
        public static int[,] SwapOFindices(int[,] Matrix, int x_index, int y_index, int Newx_index, int Newy_index)
        {
            int temp_Swap;
            temp_Swap = Matrix[x_index, y_index];
            Matrix[x_index, y_index] = Matrix[Newx_index, Newy_index];
            Matrix[Newx_index, Newy_index] = temp_Swap;
            return Matrix;
        }
        public static bool Safe_MAtrix(int x_index, int y_index, int Size_OF_Matrix)
        {
            if (x_index >= 0 && x_index < Size_OF_Matrix)
            {
                if (y_index >= 0 && y_index < Size_OF_Matrix)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static void Print_Path_node(Node Elements_node)
        {
            if (Elements_node == null)
            {
                return;
            }
            Print_Path_node(Elements_node.Parent_node);
            Print_Matrices(Elements_node.Matrix,Elements_node.Matrix.GetLength(0));
            Console.WriteLine();
        }
        public static void Solve_A_Star(int[,] Intial_Matrix, int[,] Goal, int x_index, int y_index, char Method)
        {
            int Size_OF_Directions = 4;
            int[] Rows = { 1, 0, -1, 0 };
            int[] Cols = { 0, -1, 0, 1 };
            PriorityQueue<Node, int> queue = new PriorityQueue<Node, int>();
            Node First_node = new Node();
            First_node.Matrix = Intial_Matrix;
            First_node = Create_NEW_Node(Intial_Matrix, x_index, y_index, x_index, y_index, 0, null);

            if (Method == '1')
            {
                First_node.Costs = Hamming_Distance(Intial_Matrix, Goal);
            }
            else
            {
                First_node.Costs = Manhaten_Dist(Intial_Matrix, Intial_Matrix.GetLength(0));
            }
            queue.Enqueue(First_node, First_node.Costs + First_node.Levels);
            while (queue.Count != 0)
            {
                Node node = queue.Dequeue();
                if (node.Costs == 0)
                {
                    Print_Path_node(node);
                    Console.WriteLine("# of movements = " + node.Levels);
                    return;
                }
                int i = 0;
                while (i < Size_OF_Directions)
                {
                    if (Safe_MAtrix(node.X_blank + Rows[i], node.Y_blank + Cols[i], node.Matrix.GetLength(0)))
                    {
                        Node Child_node = Create_NEW_Node(node.Matrix, node.X_blank, node.Y_blank, node.X_blank + Rows[i],
                                      node.Y_blank + Cols[i], node.Levels + 1, node);
                        if (Method == '1')
                        {
                            Child_node.Costs = Hamming_Distance(Child_node.Matrix, Goal);
                        }
                        else
                        {
                            Child_node.Costs = Manhaten_Dist(Child_node.Matrix, Child_node.Matrix.GetLength(0));
                        }
                        if (!(Is_Same_Parent(node, Child_node.X_blank, Child_node.Y_blank)))
                        {
                            queue.Enqueue(Child_node, Child_node.Costs + Child_node.Levels);
                        }
                    }
                    i++;
                }
            }
        }
    }
}