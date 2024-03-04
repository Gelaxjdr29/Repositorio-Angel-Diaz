using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    using System;
    using System.Threading;

    namespace ConsoleApp
    {
        class Program
        {
            // clase que representa una cuenta bancaria
            class Cuenta
            {
                // saldo actual de la cuenta
                public decimal saldo;

                // sbjeto para sincronizar el acceso a la cuenta
                private object lockObject = new object();

                // sonstructor que recibe el saldo inicial
                public Cuenta(decimal saldoInicial)
                {
                    saldo = saldoInicial;
                }

                // Método que realiza un ingreso de dinero en la cuenta
                public void Ingresar(decimal cantidad)
                {
                    // bloqueamos el acceso a la cuenta
                    lock (lockObject)
                    {
                        // actualizamos el saldo
                        saldo += cantidad;

                        // mostramos el resultado por la consola
                        Console.WriteLine("Se ha ingresado {0:C} en la cuenta. Saldo actual: {1:C}", cantidad, saldo);
                    }
                }

                // Método que realiza un retiro de dinero de la cuenta
                public void Retirar(decimal cantidad)
                {
                    // bloqueamos el acceso a la cuenta
                    lock (lockObject)
                    {
                        // comprobamos si hay saldo suficiente
                        if (saldo >= cantidad)
                        {
                            // actualizamos el saldo
                            saldo -= cantidad;

                            // mostramos el resultado por la consola
                            Console.WriteLine("Se ha retirado {0:C} de la cuenta. Saldo actual: {1:C}", cantidad, saldo);
                        }
                        else
                        {
                            // mostramos un mensaje de error por la consola
                            Console.WriteLine("No se puede retirar {0:C} de la cuenta. Saldo insuficiente: {1:C}", cantidad, saldo);
                        }
                    }
                }
            }

            // Método que ejecuta un hilo que realiza operaciones de ingreso
            public static void HiloIngreso(object cuenta)
            {
                // Convertimos el parámetro a una cuenta
                Cuenta c = (Cuenta)cuenta;

                // Creamos un objeto para generar números aleatorios
                Random r = new Random();

                // Repetimos 10 veces
                for (int i = 0; i < 10; i++)
                {
                    // Generamos una cantidad aleatoria entre 10 y 100
                    decimal cantidad = r.Next(10, 101);

                    // Llamamos al método de ingreso de la cuenta
                    c.Ingresar(cantidad);

                    // Esperamos un tiempo aleatorio entre 0 y 1 segundos
                    Thread.Sleep(r.Next(1000));
                }
            }

            // Método que ejecuta un hilo que realiza operaciones de retiro
            public static void HiloRetiro(object cuenta)
            {
                // Convertimos el parámetro a una cuenta
                Cuenta c = (Cuenta)cuenta;

                // Creamos un objeto para generar números aleatorios
                Random numeros = new Random();

                // Repetimos 10 veces
                for (int i = 0; i < 10; i++)
                {
                    // Generamos una cantidad aleatoria entre 10 y 100
                    decimal cantidad = numeros.Next(10, 101);

                    // Llamamos al método de retiro de la cuenta
                    c.Retirar(cantidad);

                    // Esperamos un tiempo aleatorio entre 0 y 1 segundos
                    Thread.Sleep(numeros.Next(1000));
                }
            }

            // Método principal del programa
            static void Main(string[] args)
            {
                // Creamos una cuenta con un saldo inicial de 500
                Cuenta cuenta = new Cuenta(500);

                // Creamos dos hilos que realizan operaciones sobre la cuenta
                Thread hilo1 = new Thread(HiloIngreso);
                Thread hilo2 = new Thread(HiloRetiro);

                // Iniciamos los hilos
                hilo1.Start(cuenta);
                hilo2.Start(cuenta);

                // Esperamos a que los hilos terminen
                hilo1.Join();
                hilo2.Join();

                // Mostramos el saldo final de la cuenta
                Console.WriteLine("Saldo final de la cuenta: {0:C}", cuenta.saldo);

                // Esperamos a que el usuario pulse una tecla
                Console.ReadKey();
            }
        }
    }

}
