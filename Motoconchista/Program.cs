using System;
using System.Collections.Generic;
using System.Threading;

namespace ElMotoconchista
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            const int meta = 500;
            Dictionary<string, int> motoconchos = new Dictionary<string, int>()
            {
                { "Motoconcho 1", 0 },
                { "Motoconcho 2", 0 },
                { "Motoconcho 3", 0 },
                { "Motoconcho 4", 0 }
            };

            string[] acciones = { "Acelerar", "Hacer Ziczac", "Evadir Amet", "Lidiar con el Pasajero" };
            HashSet<string> cancelados = new HashSet<string>();

            Console.WriteLine(" Bienvenido a la carrera de motoconchistas del Km.9 de la Autopista Duarte ");
            Console.WriteLine("El primero que llegue a 500 metros gana.\n");

            int ciclo = 1;
            bool carreraActiva = true;

            while (carreraActiva)
            {
                Console.WriteLine($"\n=== Ciclo {ciclo} ===");

                foreach (var nombre in new List<string>(motoconchos.Keys))
                {
                    if (cancelados.Contains(nombre))
                        continue;

                    string accion = acciones[random.Next(acciones.Length)];
                    int avance = 0;


                    if (random.NextDouble() < 0.1)
                    {
                        Console.WriteLine($" {nombre} fue CANCELADO por robarse el motor o el dinero.");
                        cancelados.Add(nombre);
                        continue;
                    }

                    switch (accion)
                    {
                        case "Acelerar":
                            avance = random.Next(15, 41);
                            if (random.NextDouble() < 0.1)
                            {
                                Console.WriteLine($" {nombre} cayó en un hoyo y pierde 20 metros.");
                                avance -= 20;
                            }
                            Console.WriteLine($" {nombre} acelera y avanza {Math.Max(avance, 0)} metros.");
                            break;

                        case "Hacer Ziczac":
                            avance = random.Next(10, 31);
                            if (random.NextDouble() < 0.05)
                            {
                                Console.WriteLine($" {nombre} chocó haciendo ziczac y pierde 20 metros.");
                                avance -= 20;
                            }
                            Console.WriteLine($" {nombre} hace ziczac y avanza {Math.Max(avance, 0)} metros.");
                            break;

                        case "Evadir Amet":
                            if (random.NextDouble() < 0.5)
                            {
                                avance = random.Next(10, 26);
                                Console.WriteLine($" {nombre} logra evadir al Amet y avanza {avance} metros.");
                            }
                            else
                            {
                                Console.WriteLine($" {nombre} fue detenido por el Amet. No avanza.");
                                avance = 0;
                                Thread.Sleep(2000);
                            }
                            break;

                        case "Lidiar con el Pasajero":
                            if (random.NextDouble() < 0.3)
                            {
                                Console.WriteLine($" {nombre} perdió al pasajero y pierde 6 segundos (no avanza).");
                                avance = 0;
                                Thread.Sleep(6000);
                            }
                            else
                            {
                                avance = 20;
                                Console.WriteLine($" {nombre} mantiene al pasajero y avanza {avance} metros.");
                            }
                            break;
                    }

                    motoconchos[nombre] += avance;
                    if (motoconchos[nombre] < 0)
                        motoconchos[nombre] = 0;

                    Console.WriteLine($" {nombre} lleva {motoconchos[nombre]} metros recorridos.");

                    if (motoconchos[nombre] >= meta)
                    {
                        Console.WriteLine($"\n ¡{nombre} ha llegado primero a la meta con {motoconchos[nombre]} metros! ");
                        Console.WriteLine("=== FIN DE LA CARRERA ===");
                        carreraActiva = false;
                        break;
                    }
                }

                ciclo++;
                Thread.Sleep(1000);
            }

            Console.WriteLine("\n El trabajo ha terminado ");
        }
    }
}

