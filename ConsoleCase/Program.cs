using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Chilkat;
using ConsoleCase;
namespace ConsoleCase
{
    class Program
    {
        static void Main(string[] args){

        var set = new HashSet<string>();

            foreach (var arg in args)
            {
                set.Add(arg);
            }

            if (set.Count != args.Length)
            {
                "Duplicated names".Log();
                return;
            }
            
            if (args.Length < 3)
            {
                "Number of moves should be obb and more than 3\n Example:\n Rock Paper Scissors Lizard Spok".Log();
                return;
            }

            if (args.Length % 2 == 0)
            {
                "Please, write odd number of moves!\n Example:\n Rock Paper Scissors".Log(); 
                return;
            }

            
            var random = new Random();
            int computerChoice = random.Next(args.Length);
            var computerChoiceString = args[computerChoice];

            var key = KeyGenerator.GenerateKey();
            var hmacValue = HmacExtensions.GenerateHmac(computerChoiceString,key);
          
            $"HMAC: {hmacValue}".Log();
            "What's your choice?".Log();

            for (var index = 0; index < args.Length; index++)
            {
                var variant = args[index];
                $"{index + 1} - {variant}".Log();
            }

            "0 - exit".Log();

            var userChoice = int.Parse(Console.ReadLine()) - 1;

            if (userChoice + 1 == 0)
            {
                return;
            }

            var halfLength = (args.Length - 1) / 2;

            $"Computer choice: {computerChoiceString}".Log();

            if (computerChoice == userChoice)
            {
                "Draw!".Log();
                key.Log();
                return;
            }

            for (int i = userChoice + 1; i < userChoice + halfLength; i++)
            {
                var winVariant = i;

                if (winVariant > args.Length)
                {
                    winVariant -= args.Length;
                }

                if (winVariant == computerChoice)
                {
                    "You lose(((".Log();
                    key.Log();
                    return;
                }
            }

            "You win!!".Log();
            key.Log();
        }
    }
}