
//App to search files which contains informed string at main folder and his subfolders;

namespace FileSearcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char check = 'y';
            char exit = 'y';
            int count = 0;

            while (check != 'n')
            {
                Console.Clear();
                Console.WriteLine("App to search string content in all files at folder and his subfolders");
                Console.WriteLine();
                Console.WriteLine();

                string sourceFolder;

                // get folder path and test if is null
                do
                {
                    Console.Write("Input folder path: ");
                    sourceFolder = @Console.ReadLine();

                    if (!Directory.Exists(sourceFolder))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid source Folder!");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }

                } while(!Directory.Exists(sourceFolder));
                
                while (exit != 'n')
                {
                    Console.Clear();
                    Console.WriteLine($"Searching at '{sourceFolder}'");
                    Console.WriteLine();
                    Console.Write("Input string to be find: ");
                    string searchWord = Console.ReadLine();
                    Console.WriteLine();
                    List<string> allFiles = new List<string>();
                    AddFileNamesToList(sourceFolder, allFiles);
                    foreach (string fileName in allFiles)
                    {
                        string contents = File.ReadAllText(fileName);
                        if (contents.Contains(searchWord))
                        {
                            Console.WriteLine(fileName);
                            count++;
                        }
                    }

                    if (count == 0)
                    {
                        Console.WriteLine("No files found.");
                    }

                    Console.WriteLine();

                    do
                    {
                        Console.Write("Do you want to perform a new search in the same folder (y/n)? ");
                        exit = char.Parse(Console.ReadLine());

                        if (exit != 'y' && exit != 'n')
                        {
                            Console.WriteLine("invalid option!");
                            Thread.Sleep(1000);
                            Console.Clear();
                        }

                    } while (exit != 'y' && exit != 'n');
                }

                do
                {
                    Console.Write("Do you want to change the source folder (y/n)? ");
                    check = char.Parse(Console.ReadLine());

                    if(check != 'y' && check != 'n')
                    {
                        Console.WriteLine("invalid option!");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }

                } while (check != 'y' && check != 'n');
            }

            Console.WriteLine("Leaving... Thanks for use!");
            Thread.Sleep(3000);
        }
            
        public static void AddFileNamesToList(string sourceDir, List<string> allFiles)
        {

            string[] fileEntries = Directory.GetFiles(sourceDir);
            foreach (string fileName in fileEntries)
            {
                allFiles.Add(fileName);
            }

            //Recursion
            string[] subdirectoryEntries = Directory.GetDirectories(sourceDir);
            foreach (string item in subdirectoryEntries)
            {
                // Avoid "reparse points"
                if ((File.GetAttributes(item) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                {
                    AddFileNamesToList(item, allFiles);
                }
            }
        }
    }
}