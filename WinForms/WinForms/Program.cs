using System;
<<<<<<< HEAD
using System.Windows.Forms;
using WinForms;

namespace WinForms
=======
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms;

namespace Example2
>>>>>>> a5c073f9fef23fd2f9fddcc5f3b52027be21a0a1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
<<<<<<< HEAD
}
=======
}

//using System;
//using WinForms;

//namespace Example3
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            DisplayMessage d = new DisplayMessage(SetText);
//            Brain brain = new Brain(d);
//            while (true)
//            {
//                string line = Console.ReadLine();
//                brain.ProcessSignal(line);

//                //Console.WriteLine(brain.stack_of_numbers.Peek());
//                Console.Write(" " + brain.stack_of_numbers.Count + "size ");
//                if (brain.stack_of_operations.Count != 0)
//                {
//                    Console.Write(brain.stack_of_operations.Peek() + " ");
//                    Console.WriteLine(brain.stack_of_operations.Count);
//                }
//                else
//                {
//                    //Console.WriteLine("empty");
//                }
//            }
//        }

//        static void SetText(string msg)
//        {
//            Console.Write(msg);
//        }
//    }
//}
>>>>>>> a5c073f9fef23fd2f9fddcc5f3b52027be21a0a1
