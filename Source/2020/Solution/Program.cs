using System;
using System.Collections.Generic;

namespace Solution
{
    class Program
    {
        public struct Book
        {
            public int id;
            public int score;
            public bool scanned;
        }

        public struct Library
        {
            public int nBooks;
            public int signupTime;
            public int booksPerDay;
            public int[] bookSet;
            public int[] totalScore;
            public bool signingUp;
            public bool signedUp;

            public int nScannedBooks;
            public List<int> scannedBooks;
        }

        // public struct SignedLibrary
        // {
        //     public int[]
        // }

        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(@"..\Input\a_example.txt");
            string[] line0 = input[0].Split(' ');
            Book[] books = new Book[Int32.Parse(line0[0])];
            string[] bookScores = input[1].Split(' ');

            for (int i = 0; i < books.Length; i++)
            {
                books[i].id = i;
                books[i].score = Int32.Parse(bookScores[i]);
                books[i].scanned = false;
            }

            Library[] libraries = new Library[Int32.Parse(line0[1])];

            for (int i = 0; i < libraries.Length; i++)
            {
                string[] libraryData = input[(i + 1) * 2].Split(' ');
                libraries[i].nBooks = Int32.Parse(libraryData[0]);
                libraries[i].signupTime = Int32.Parse(libraryData[1]);
                libraries[i].booksPerDay = Int32.Parse(libraryData[2]);
                libraries[i].bookSet = Array.ConvertAll(input[((i + 1) * 2) + 1].Split(' '), int.Parse);

                libraries[i].signingUp = false;
                libraries[i].signedUp = false;
            }

            int maxDays = int.Parse(line0[2]);

            bool signingUp = false;

            int signingTimeLeft = 0;

            int signedLibraries = 0;

            for (int day = 0; day < maxDays; day++)
            {
                if (signingUp && signingTimeLeft > 0)
                {
                    signingTimeLeft--;
                }
                else if (signingUp && signingTimeLeft == 0)
                {
                    signedLibraries++;
                    
                    for (int i = 0; i < libraries.Length; i++)
                    {
                        if (libraries[i].signingUp)
                        {
                            signingUp = false;
                            libraries[i].signingUp = false;
                            libraries[i].signedUp = true;
                        }
                    }
                }

                else if (!signingUp)
                {
                    for (int i = 0; i < libraries.Length; i++)
                    {
                        if (!libraries[i].signedUp)
                        {
                            libraries[i].signingUp = true;
                            signingUp = true;

                            signingTimeLeft = libraries[i].signupTime;
                        }
                    }
                }

                else
                {
                    for (int i = 0; i < libraries.Length; i++)
                    {
                        if (libraries[i].signedUp)
                        {
                            for (int j = 0; j < libraries[i].bookSet.Length; j++)
                            {
                                for (int k = 0; k < libraries[i].nBooks; k++)
                                {
                                    if (!books[libraries[i].bookSet[j]].scanned)
                                    {
                                        books[libraries[i].bookSet[j]].scanned = true;
                                        libraries[i].nScannedBooks++;
                                        libraries[i].scannedBooks.Add(books[libraries[i].bookSet[j]].id);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int[] line1;

            string[] lines = {signedLibraries.ToString(),};

            Console.WriteLine(signedLibraries);
            Console.WriteLine()
        }
    }
}
