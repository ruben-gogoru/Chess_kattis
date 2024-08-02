using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Linq;
using System.Data.Common;
using System.IO;

namespace ConsoleApp1
{
    
    public class Solution
    {
        private static Dictionary<int, char> _dictionaryColumn = new Dictionary<int, char>() { { 1, 'A' }, { 2, 'B' }, { 3, 'C' }, { 4, 'D' }, { 5, 'E' }, { 6, 'F' } , { 7, 'G' } , { 8, 'H' } };
        static void Main(string[] args)
        {
            string readLine = Console.ReadLine();
            var iterations = int.Parse(readLine);

            for (int i = 0; i < iterations; i++)
            {
                string line = Console.ReadLine();
                var result = GetPath(line);
                Console.WriteLine(result);
            }

            //var result = GetPath("B 3 E 6");
            //Console.WriteLine(result);

        }


        public static string GetPath(string line)
        {

            var result = "Impossible";
            line = line.ToUpper().Replace(" ", "");

            
            var intColumnStart = _dictionaryColumn.First(x => x.Value == line[0]).Key;
            var intRowStart = (int)Char.GetNumericValue(line[1]);
            var intColumnEnd = _dictionaryColumn.First(x => x.Value == line[2]).Key;
            var intRowEnd = (int)Char.GetNumericValue(line[3]);

            var canFindPath = CanFindPath(intColumnStart,
                                    intRowStart,
                                    intColumnEnd,
                                    intRowEnd);

            if(canFindPath)
            {
                var path = FindPath(intColumnStart,
                                    intRowStart,
                                    intColumnEnd,
                                    intRowEnd);

                return path;
                
            }

            return result ;

        }

        

        public static string FindPath(int columnStart, int rowStart, int columnEnd, int rowEnd)
        {
            
            if(columnStart == columnEnd && rowStart == rowEnd)
                return "0 " + _dictionaryColumn[columnStart] + " " + rowStart ;

            //See if start and end are in the same diagonal
            if(Math.Abs(rowStart - rowEnd) == Math.Abs(columnStart - columnEnd) )
            {
                return "1 " +
                    _dictionaryColumn[columnStart] + " " +
                    rowStart.ToString() + " " +
                    _dictionaryColumn[columnEnd] + " " +
                    rowEnd.ToString();
            }
                

            var path = "";
            for(int i = 1; i<=8; i++)
            {
                for(int j = 1; j<=8; j++)
                {
                    // in this line we check if the point (i,j) are in the diagonal 
                    if(Math.Abs(rowStart - i) == Math.Abs(columnStart - j) &&
                            Math.Abs(rowEnd - i) == Math.Abs(columnEnd - j))
                    {
                        var columnIntersection = _dictionaryColumn[ j];
                        var rowIntersection = i;

                        path =  columnIntersection.ToString() + " " + rowIntersection.ToString();
                    }
                }
            }


            return "2 " +
                    _dictionaryColumn[columnStart] + " " +
                    rowStart.ToString() + " " +
                    path + " " +
                    _dictionaryColumn[columnEnd] + " " +
                    rowEnd.ToString();
        }

        public static bool CanFindPath(int columnStart, int rowStart, int columnEnd, int rowEnd)
        {
            return (rowStart + columnStart) % 2 == (rowEnd + columnEnd) % 2;
        }


    }
}


