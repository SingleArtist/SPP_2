using System;
using System.Threading;

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
    class Program
    {
        delegate void Operation(string sourceFieName, string destFileName);
        static void Main(string[] args)
        {
            FileCopier Сopier = new FileCopier();
            Сopier.Start();
        }
    }
}