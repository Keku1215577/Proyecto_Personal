using System;
using System.Threading.Tasks;   

class program
{
    static async Task Main()
    {


        Console.WriteLine("Iniciando Programa..\n");

        // Tarea Padre
        Task parentTask = Task.Factory.StartNew(() =>
        {

            Console.WriteLine("[Padre] Tarea Padre Iniciada");

            // Tarea Hija 1
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine(" [Hija 1] Tarea Hija 1 Iniciada");
                Task.Delay(500).Wait();
                Console.WriteLine(" [Hija 1] Tarea Hija 1 Completada");
            }, TaskCreationOptions.AttachedToParent);


            // Tarea Hija 2
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("[Hija 2] Tarea Hija 2 Iniciada");
                Task.Delay(300).Wait();
                Console.WriteLine("[Hija 2] Tarea Hija 2 Completada");

            }, TaskCreationOptions.AttachedToParent);

            // Tarea Hija 3
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("[Hija 3] Tarea Hija 3 Iniciada");
                Task.Delay(100).Wait();
                Console.WriteLine("[Hija 3] Tarea Hija 3 Completada");

            }, TaskCreationOptions.AttachedToParent);

        });

        await parentTask;

        Console.WriteLine("\n [Tarea Padre] Todas las tareas hijas han completado.");
        Console.WriteLine("\nPrograma Finalizado.");
    }
        


}
//https://itlaedudo-my.sharepoint.com/:v:/g/personal/20231121_itla_edu_do/EcVjyAeDMvJKqJgaCOlaK1gBFCMZ9DxtEqjKnkVJzS0eqQ