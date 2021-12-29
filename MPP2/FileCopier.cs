using System;
using System.IO;
using System.Threading;
using FirstTask;

//Задание 2
/*Реализовать консольную программу на языке C#, которая:
- принимает в параметре командной строки путь к исходному и
целевому каталогам на диске;
-выполняет параллельное копирование всех файлов из исходного
каталога в целевой каталог;
-выполняет операции копирования параллельно с помощью пула
потоков;
-дожидается окончания всех операций копирования и выводит в
консоль информацию о количестве скопированных файлов.*/

namespace SecondTask
{
    class FileCopier
    {
        private string sourcePath;
        private string targetPath;

        private int completeTaskNumber = 0;

        TaskQueue taskQueue = new TaskQueue(3);
        public FileCopier(
            string sourcePath = @"D:\UNIVERSITY\mpp2\TestCopy",
            string targetPath = @"D:\UNIVERSITY\mpp2\TestCopyDest"
)
        {
            this.sourcePath = sourcePath;
            this.targetPath = targetPath;

            SourceDirectoryTest();
            TargetDirectoryTest();
        }
        public void Start()
        {
            CopyDirectories();
            CopyFiles();

            taskQueue.Finish();
        }

        public void TargetDirectoryTest()
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
        }

        public void SourceDirectoryTest()
        {
            if (!Directory.Exists(sourcePath))//проверка, существует ли папка
            {
                Console.WriteLine("Source path don't exists");
                Environment.Exit(0);
            }

        }
       

        

        public void CopyDirectories()
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }
                catch (SystemException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }

        public void CopyFiles()
        {
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                taskQueue.EnqueueTask(
                    delegate
                    {
                        try
                        {
                            File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                        }
                        catch (SystemException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                );
                completeTaskNumber++;
            }
            Console.WriteLine(completeTaskNumber);
        }
    }
}
