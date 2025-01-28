using System;

namespace Games
{
    public class SecondGame
    {
        public void StartGame()
        {
            string[] options = { "piedra", "papel", "tijera" };
            Random random = new Random();

            Console.WriteLine("\n   --------------------------------------   ");    
            Console.WriteLine("            Piedra, Papel o Tijera   ");

            Console.Write("\n     Escribe: piedra, papel o tijera: ");
            string playerChoice = Console.ReadLine().ToLower();

            if (Array.IndexOf(options, playerChoice) == -1)
            {
                Console.WriteLine("\n   🚨 Opción no válida. Intenta de nuevo");
                return;  
            }

            string computerChoice = options[random.Next(options.Length)];
            Console.WriteLine($"\n     La computadora elige: {computerChoice}");

            if (playerChoice == computerChoice)
            {
                Console.WriteLine("               🤝 ¡Es un empate!");

            }
            else if ((playerChoice == "piedra" && computerChoice == "tijera") ||
                     (playerChoice == "papel" && computerChoice == "piedra") ||
                     (playerChoice == "tijera" && computerChoice == "papel"))
            {
                Console.WriteLine("               🎉 ¡Ganaste!");
            }
            else
            {
                Console.WriteLine("            😞 La computadora ganó ");
            }

            Console.WriteLine("\n----------------------------------------------");
        }
    }
}
