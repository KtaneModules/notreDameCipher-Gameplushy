using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    static class Ciphers
    {
        public static char[,] RosaceCipher(char[,] matrix, int rowchange, int colchange, bool clock)
        {
            if (clock)
            {
                //Clocwise
                char reserve = matrix[2 + rowchange, 2 + colchange]; //Stpck 1
                matrix[2 + rowchange, 2 + colchange] = matrix[2 - colchange, 2 + rowchange]; //4 va dans 1
                matrix[2 - colchange, 2 + rowchange] = matrix[2 - rowchange, 2 - colchange]; //3 va dans 4
                matrix[2 - rowchange, 2 - colchange] = matrix[2 + colchange, 2 - rowchange]; //2 va dans 3
                matrix[2 + colchange, 2 - rowchange] = reserve; //Reserve dans 2
            }
            else
            {
                //Counter
                char reserve = matrix[2 + rowchange, 2 + colchange];
                matrix[2 + rowchange, 2 + colchange] = matrix[2 + colchange, 2 - rowchange];
                matrix[2 + colchange, 2 - rowchange] = matrix[2 - rowchange, 2 - colchange];
                matrix[2 - rowchange, 2 - colchange] = matrix[2 - colchange, 2 + rowchange];
                matrix[2 - colchange, 2 + rowchange] = reserve;
            }

            return matrix;
        }

        public static char[,] VitrailCipher(char[,] matrix, int colstart, int colend, int overflow)
        {
            if (colstart == colend) throw new ArgumentException();
            Queue<char> queuein = new Queue<char>();
            Queue<char> queueout = new Queue<char>();
            for (int i = 0; i < overflow; i++)
            {
                queuein.Enqueue(matrix[i, colstart]);
                queueout.Enqueue(matrix[4 - i, colend]);
            }
            for (int i = 4; i >= 0; i--)
            {
                matrix[i, colend] = queuein.Count == i + 1 ? queuein.Dequeue() : matrix[i - queuein.Count, colend];
                matrix[4 - i, colstart] = queueout.Count == i + 1 ? queueout.Dequeue() : matrix[4 - i + queueout.Count, colstart];
            }
            return matrix;
        }

        public static char[,] CrossCipher(char[,] matrix, int colused, int rowused)
        {
            Queue<char> rowData = new Queue<char>();
            Queue<char> colData = new Queue<char>();
            for (int i = 0; i < 5; i++)
            {
                if (!(i == colused)) rowData.Enqueue(matrix[rowused, i]);
                if (!(i == rowused)) colData.Enqueue(matrix[i, colused]);
            }
            for (int i = 0; i < 5; i++)
            {
                if (!(i == colused)) matrix[rowused, i] = colData.Dequeue();
                if (!(i == rowused)) matrix[i, colused] = rowData.Dequeue();
            }
            return matrix;
        }
    }

