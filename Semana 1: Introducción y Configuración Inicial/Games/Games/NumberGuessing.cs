using System;

namespace Games
{
    public class NumberGuessing
    {
        public void StartGame()
        {
            Random rand = new Random();
            int numberToGuess = rand.Next(1, 101); 
            int playerGuess = 0;

            Console.WriteLine("\n   --------------------------------------   ");    
            Console.WriteLine("      ¡Adivina el número entre 1 y 100!     ");

            while (playerGuess != numberToGuess)
            {
                Console.Write("\n      Introduce tu número: ");
                playerGuess = int.Parse(Console.ReadLine());

                if (playerGuess < numberToGuess)
                    Console.WriteLine("             🚨!El número es mayor!      ");
                else if (playerGuess > numberToGuess)
                    Console.WriteLine("             🚨!El número es menor!     ");
        
                else
                    Console.WriteLine("    🎉¡Correcto! Has adivinado el número");
                    Console.WriteLine("\n   --------------------------------------   ");    
            }
        }
    }
}
