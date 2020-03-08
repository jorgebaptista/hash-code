using System;
using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    class Program
    {
        public struct Book
        {
            public int score;
            public bool scanned;
        }

        public struct Library
        {
            public int totalBooks;
            public int signupTime;
            public int booksPerDay;
            public int[] bookSet;
            // public bool signingUp;
            public bool signedUp;
            public int signedUpID;
        }

        public struct SignedLibrary
        {
            public int id;
            public List<int> scannedBooks;
        }

        static void Main(string[] args)
        {
            #region Input read and variable assignment

            // Read the input file
            //d_tough_choices
            //e_so_many_books
            //f_libraries_of_the_world
            string[] input = System.IO.File.ReadAllLines(@"..\Input\c_incunabula.txt");

            // Separate first line by spaces and convert all array elements to Int for future reference
            int[] line0 = Array.ConvertAll(input[0].Split(' '), int.Parse);
            // Create B total Books based on the first number of the first line of the input file
            Book[] books = new Book[line0[0]];
            // Same for Libraries
            Library[] libraries = new Library[line0[1]];
            // And days
            int maxDays = line0[2];

            // Separate second line by spaces and convert all array elements to Int for future reference
            int[] line1 = Array.ConvertAll(input[1].Split(' '), int.Parse);
            // Iterate through all the books by Id and assign scores to each from the input line 2
            for (int i = 0; i < books.Length; i++)
            {
                books[i].score = line1[i];
                // Set scanned status to false because no books were scanned yet
                books[i].scanned = false;
            }

            // Iterate through each Library
            for (int i = 0; i < libraries.Length; i++)
            {
                // get the 3rd line in the input file and then go 2 by 2 for each library id and assign it to an array converted to Int
                int[] libraryData2 = Array.ConvertAll(input[(i + 1) * 2].Split(' '), int.Parse);

                // Assign each number of the first line: total books of this library, signup time and how many books per day it can scan
                libraries[i].totalBooks = libraryData2[0];
                libraries[i].signupTime = libraryData2[1];
                libraries[i].booksPerDay = libraryData2[2];

                // get the 4rd line in the input file and then go 2 by 2 for each library and assign it to the library bookset
                libraries[i].bookSet = Array.ConvertAll(input[((i + 1) * 2) + 1].Split(' '), int.Parse);

                // Set signingUp and signedUp as false as default value
                // libraries[i].signingUp = false;
                libraries[i].signedUp = false;
            }

            // Boolean to check if any library is currently being signed up
            bool signingUp = false;
            // Keep check of how many libraries has been signed in total            
            // int totalSignedLibraries = 0;

            int signingTimeLeft = 0;
            int signingUpLibraryID = 0;

            SignedLibrary[] signedLibraries = new SignedLibrary[0];

            #endregion


            for (int day = 0; day < maxDays; day++)
            {
                if (!signingUp)
                {
                    for (int i = 0; i < libraries.Length; i++)
                    {
                        if (!libraries[i].signedUp)
                        {
                            signingUp = true;
                            signingTimeLeft = libraries[i].signupTime;
                            signingUpLibraryID = i;
                        }
                    }
                }
                else if (signingTimeLeft > 0) signingTimeLeft--;
                else
                {
                    signingUp = false;
                    libraries[signingUpLibraryID].signedUp = true;

                    Array.Resize(ref signedLibraries, signedLibraries.Length + 1);

                    signedLibraries[signedLibraries.Length - 1].id = signingUpLibraryID;
                    libraries[signingUpLibraryID].signedUpID = signedLibraries.Length - 1;

                    signedLibraries[signedLibraries.Length - 1].scannedBooks = new List<int>();
                }

                for (int signedLibrary = 0; signedLibrary < signedLibraries.Length; signedLibrary++)
                {
                    int currentLibraryID = signedLibraries[signedLibrary].id;

                    // if (signedLibraries[signedLibrary].scannedBooks.Count == libraries[currentLibraryID].totalBooks) break;
                    // else
                    // {
                        for (int scannedBooks = 0; scannedBooks < libraries[currentLibraryID].booksPerDay; scannedBooks++)
                        {
                            for (int book = 0; book < libraries[currentLibraryID].totalBooks - 1; book++)
                            {
                                int currentBookID = libraries[currentLibraryID].bookSet[book];

                                if (!books[currentBookID].scanned)
                                {
                                    books[currentBookID].scanned = true;

                                    signedLibraries[signedLibrary].scannedBooks.Add(currentBookID);
                                }
                            }
                        }
                    // }
                }
            }


            List<SignedLibrary> signedLibrariesList = signedLibraries.OfType<SignedLibrary>().ToList();

            for (int i = 0; i < signedLibrariesList.Count - 1; i++)
            {
                if (signedLibrariesList[i].scannedBooks.Count == 0)
                {
                    signedLibrariesList.RemoveAt(i);
                }
            }

            signedLibraries = signedLibrariesList.ToArray();

            List<string> libraryData = new List<string>();

            libraryData.Add(signedLibraries.Length.ToString());

            for (int i = 0; i < signedLibraries.Length; i++)
            {
                libraryData.Add(signedLibraries[i].id.ToString() + " " + signedLibraries[i].scannedBooks.Count.ToString());
                libraryData.Add(String.Join(" ", signedLibraries[i].scannedBooks));
            }

            System.IO.File.WriteAllLines(@"C.output", libraryData);
        }
    }
}
