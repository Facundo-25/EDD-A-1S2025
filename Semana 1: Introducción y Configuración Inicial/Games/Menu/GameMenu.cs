using System;
using Games;

namespace Menu
{
    public class GameMenu
    {
        public void ShowMenu()
        {
            Console.WriteLine("============================================");
            Console.WriteLine("       ¡Bienvenido al menú de juegos!       ");
            Console.WriteLine("\n1. Adivina el número");
            Console.WriteLine("2. Pieda, Papel o Tijera");
            Console.WriteLine("============================================");
            Console.Write("\n   Elige una opción (1 o 2): ");
            
            
            var option = Console.ReadLine();
            
            if (option == "1")
            {
                var numberGuessingGame = new NumberGuessing();
                numberGuessingGame.StartGame();
            }
            else if (option == "2")
            {
                var secondGame = new SecondGame();
                secondGame.StartGame();
            }
            else
            {
                Console.WriteLine("!Opción no válida. Elige 1 o 2.!");
            }
            }
        
    }
}
