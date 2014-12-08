using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Kontron;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Kontron
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> cards = Kontron_NET.GetAvailableCards();

            if (cards.Count == 0)
            {
                Console.WriteLine("No cards found in registry. Attempt to run PCIFIND.EXE? (y/n)");
                ConsoleKeyInfo reply = Console.ReadKey(true);
                if (reply.Key == ConsoleKey.Y)
                {
                    try
                    {
                        Process proc = Process.Start("PCIFIND.EXE");
                        return;
                    }
                    catch (System.ComponentModel.Win32Exception w32ex)
                    {
                        Console.WriteLine("Could not start PCIFIND:\n   {0}", w32ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Available Kontron Cards:\n");

                const string formatter = "{0,-48}{1,-13}{2,-8}";
                Console.WriteLine(formatter, "Name", "#PortGroups", "BaseAddr");
                for (int i = 0; i < 79; i++)
                    Console.Write('-');
                Console.WriteLine();

                foreach (Card card in cards)
                {
                    Console.WriteLine(formatter, card.Name, card.NumPortGroups, "0x" + Convert.ToString(card.BaseAddress, 16));
                    if (card.NumPortGroups > 1)
                    {
                        foreach (PortGroup pg in card.PortGroups)
                        {
                            Console.WriteLine("   {0}:     {1}", pg, "0x" + Convert.ToString(pg.BaseAddress, 16));
                        }
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Press ENTER to quit.");
            Console.Read();
        }
    }
}
