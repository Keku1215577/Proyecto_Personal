using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramacionParalela
{
    public interface IOperation
    {
        public Task Iniciar();
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            IOperation operation = new JuegoPelota();
            await operation.Iniciar();
        }
    }

    public class JuegoPelota : IOperation
    {
        public async Task Iniciar()
        {
            Console.WriteLine("El Torneo de Pelota Programador!");
            Console.WriteLine("Que comience el partido!");
            Console.WriteLine("Los equipos compiten para anotar 5 goles primero");
            Console.WriteLine("Presiona 'r' para lesionar al Equipo Rojo");
            Console.WriteLine("Presiona 'a' para lesionar al Equipo Azul");
            Console.WriteLine("Presiona 'v' para lesionar al Equipo Verde");
            Console.WriteLine("Presiona 'y' para lesionar al Equipo Amarillo");
            Console.WriteLine("Presiona 'x' para terminar el torneo");
            Console.WriteLine("=====================================");

           
            var cancelationTokenRojo = new CancellationTokenSource();
            var cancelationTokenAzul = new CancellationTokenSource();
            var cancelationTokenVerde = new CancellationTokenSource();
            var cancelationTokenAmarillo = new CancellationTokenSource();

           
            var equipoRojo = Task.Factory.StartNew(() =>
            {
                JugarPartido("Equipo Rojo", cancelationTokenRojo.Token);
            });

            var equipoAzul = Task.Factory.StartNew(() =>
            {
                JugarPartido("Equipo Azul", cancelationTokenAzul.Token);
            });

            var equipoVerde = Task.Factory.StartNew(() =>
            {
                JugarPartido("Equipo Verde", cancelationTokenVerde.Token);
            });

            var equipoAmarillo = Task.Factory.StartNew(() =>
            {
                JugarPartido("Equipo Amarillo", cancelationTokenAmarillo.Token);
            });

            
            await Task.Run(() =>
            {
                while (true)
                {
                    var key = Console.ReadKey(true);

                    switch (char.ToLower(key.KeyChar))
                    {
                        case 'r':
                            Console.WriteLine("\nEl Equipo Rojo ha sido lesionado y abandona el torneo!");
                            cancelationTokenRojo.Cancel();
                            break;

                        case 'a':
                            Console.WriteLine("\nEl Equipo Azul ha sido lesionado y abandona el torneo!");
                            cancelationTokenAzul.Cancel();
                            break;

                        case 'v':
                            Console.WriteLine("\nEl Equipo Verde ha sido lesionado y abandona el torneo!");
                            cancelationTokenVerde.Cancel();
                            break;

                        case 'y':
                            Console.WriteLine("\nEl Equipo Amarillo ha sido lesionado y abandona el torneo!");
                            cancelationTokenAmarillo.Cancel();
                            break;

                        case 'x':
                            Console.WriteLine("\nTorneo terminado por el organizador!");
                            cancelationTokenRojo.Cancel();
                            cancelationTokenAzul.Cancel();
                            cancelationTokenVerde.Cancel();
                            cancelationTokenAmarillo.Cancel();
                            return;

                        default:
                            Console.WriteLine($"\nTecla '{key.KeyChar}' no reconocida. Usa: r, a, v, y, x");
                            break;
                    }
                }
            });

            Console.WriteLine("\n (Gracias Por Disfrutar de nuestro partido)");
        }

        async Task JugarPartido(string nombreEquipo, CancellationToken token)
        {
            var goles = 0;
            var random = new Random();
            var metaGoles = 5;

            try
            {
                Console.WriteLine($"{nombreEquipo} entra al campo de juego");

                while (goles < metaGoles)
                {
                    
                    token.ThrowIfCancellationRequested();

                    
                    var tiempoEntreJugadas = random.Next(1000, 4000);
                    Console.WriteLine($"{nombreEquipo} está atacando...");

                   
                    await Task.Delay(tiempoEntreJugadas, token);

                   
                    if (random.Next(1, 101) <= 70)
                    {
                        goles++;
                        Console.WriteLine($"GOL! {nombreEquipo} anota su gol #{goles}");

                       
                        await Task.Delay(500, token);
                    }
                    else
                    {
                        Console.WriteLine($"{nombreEquipo} falló el tiro...");
                    }
                }

                Console.WriteLine($"{nombreEquipo} GANA EL TORNEO con {goles} goles!");
                Console.WriteLine("Celebración del campeón!");
            }
            catch (OperationCanceledException) 
            {
                Console.WriteLine($"{nombreEquipo} ha abandonado el torneo (lesionado con {goles} goles).");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error inesperado con {nombreEquipo}: {e.Message}");
            }
        }
    }
}
