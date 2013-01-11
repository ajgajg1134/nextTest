﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace nextTest
{
    class Controller
    {
        static List<String> fronts = new List<String>();
        static List<String> backs = new List<String>();

        static int currentIndex = -1;
        
        public Controller()
        {

        }

        public static void add(String s1, String s2)
        {
            fronts.Add(s1);
            backs.Add(s2);
        }
        public static String getFront(int index)
        {
            return fronts.ElementAt(index);
        }
        public static String getBack(int index)
        {
            return backs.ElementAt(index);
        }
        public static String getFront()
        {
            return fronts.ElementAt(currentIndex);
        }
        public static String getBack()
        {
            return backs.ElementAt(currentIndex);
        }
        public static void removeAt(int index)
        {
            fronts.RemoveAt(index);
            backs.RemoveAt(index);
        }
        public static void removeAt()
        {
            fronts.RemoveAt(currentIndex);
            backs.RemoveAt(currentIndex);
        }
        public static int getLength()
        {
            return fronts.Count;
        }
        public static int getIndex()
        {
            return currentIndex;
        }
        public static void incIndex()
        {
            currentIndex++;
        }
        public static void decIndex()
        {
            currentIndex--;
        }
        public static void setIndex(int num)
        {
            currentIndex = num;
        }
        public static void clearAll()
        {
            fronts = new List<String>();
            backs = new List<String>();
        }
        public static void loadOld(String url)
        {
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            // Specify the file path and options.
            using (var isoFileStream =
                    new System.IO.IsolatedStorage.IsolatedStorageFileStream
                        ("DataFolder\\" + url + ".txt", System.IO.FileMode.Open, local))
            {
                // Read the data.
                using (var isoFileReader = new System.IO.StreamReader(isoFileStream))
                {
                    while (!isoFileReader.EndOfStream)
                    {
                        String input = isoFileReader.ReadLine();
                        String[] inputs = input.Split('|');
                        fronts.Add(inputs[0]);
                        backs.Add(inputs[1]);
                    }
                }
            }
        }
        /// <summary>
        /// Writes the content of the open flashcards to a file called name.txt
        /// </summary>
        /// <param name="name">The file name to use, will add a .txt to end of string</param>
        public static void saveCurrent(String name)
        {
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            if (!local.DirectoryExists("DataFolder"))
                local.CreateDirectory("DataFolder");

            using (var isoFileStream =
            new System.IO.IsolatedStorage.IsolatedStorageFileStream(
                "DataFolder\\" + name +".txt",
                System.IO.FileMode.Create,
                    local))
            {
                // Write the data from the textbox.
                using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
                {
                    for (int i = 0; i < fronts.Count; i++)
                    {
                        isoFileWriter.WriteLine(fronts.ElementAt(i) + "|" + backs.ElementAt(i));
                    }
                    updateFileList(name);
                }
            }
        }
        public static void updateFileList(String name)
        {
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            if (!local.DirectoryExists("DataFolder"))
                local.CreateDirectory("DataFolder");

            String fileContents = "";

            using (var isoFileStream =
                new System.IO.IsolatedStorage.IsolatedStorageFileStream(
                    "DataFolder\\FileList.txt",
                    System.IO.FileMode.OpenOrCreate,
                        local))
            {
                using (var isoFileReader = new System.IO.StreamReader(isoFileStream))
                {
                    fileContents = isoFileReader.ReadToEnd();
                }
            }
            using (var isoFileStream =
                new System.IO.IsolatedStorage.IsolatedStorageFileStream(
                    "DataFolder\\FileList.txt",
                    System.IO.FileMode.Create,
                        local))
            {
                // Write the data from the textbox.
                using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
                {
                    isoFileWriter.Write(fileContents + name + "\n");
                }
            }
        }
        /// <summary>
        /// Writes 3 fake test load files to file FileList.txt
        /// </summary>
        public static void updateFileList()
        {
            
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            if (!local.DirectoryExists("DataFolder"))
                local.CreateDirectory("DataFolder");

            using (var isoFileStream =
                new System.IO.IsolatedStorage.IsolatedStorageFileStream(
                    "DataFolder\\FileList.txt",
                    System.IO.FileMode.OpenOrCreate,
                        local))
            {
                using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
                {
                    isoFileWriter.WriteLine("Test1");
                    isoFileWriter.WriteLine("Test2");
                    isoFileWriter.WriteLine("Test3");
                }
            }
        }
        public static List<String> getFileList()
        {
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            List<String> files = new List<String>();

            // Specify the file path and options.
            using (var isoFileStream =
                    new System.IO.IsolatedStorage.IsolatedStorageFileStream
                        ("DataFolder\\FileList.txt", System.IO.FileMode.OpenOrCreate, local))
            {
                // Read the data.
                using (var isoFileReader = new System.IO.StreamReader(isoFileStream))
                {
                    
                    while (!isoFileReader.EndOfStream)
                    {
                        files.Add(isoFileReader.ReadLine());
                    }
                }
            }
            if (files.Count <= 0)
            {
                return new List<string>();
            }
            
            return files;
        }
        public static void removeAllFiles()
        {
            List<string> files = getFileList();
            foreach (string s in files)
            {
                removeFile(s);
            }
        }
        /// <summary>
        /// Deletes a file called url.txt. And removes it from FileList.
        /// </summary>
        /// <param name="url">Filename, automatically appends .txt</param>
        public static void removeFile(String url)
        {
            System.IO.IsolatedStorage.IsolatedStorageFile local =
                System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            local.DeleteFile("DataFolder\\" + url + ".txt");

            String fileContents = "";
            using (var isoFileStream =
                   new System.IO.IsolatedStorage.IsolatedStorageFileStream
                       ("DataFolder\\FileList.txt", System.IO.FileMode.Open, local))
            {
                // Read the data.
                using (var isoFileReader = new System.IO.StreamReader(isoFileStream))
                {

                    fileContents = isoFileReader.ReadToEnd();
                }
            }
            local.DeleteFile("DataFolder\\FileList.txt");

            List<string> fileLines = fileContents.Split('\n').ToList<string>();

            fileLines.Remove(url);

            fileContents = "";



            foreach (string s in fileLines)
            {
                if(!s.Equals(""))
                    fileContents += s + '\n';
            }
            
            using (var isoFileStream =
                   new System.IO.IsolatedStorage.IsolatedStorageFileStream
                       ("DataFolder\\FileList.txt", System.IO.FileMode.Create, local))
            {
                // Read the data.
                using (var isoFileWriter = new System.IO.StreamWriter(isoFileStream))
                {
                    isoFileWriter.Write(fileContents);
                }
            }
        }
    }
}
