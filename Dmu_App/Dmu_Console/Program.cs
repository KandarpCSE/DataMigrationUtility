using ConsoleTables;
using Dmu_Console.Data;
using Dmu_Console.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dmu_Console
{
    public class Program
    {

        static async Task Main(string[] args)
        {
            Operation.InitializeData();
            ConsoleKeyInfo keychoosen;
            do
            {
                Console.WriteLine("\n");
                Console.WriteLine("\n");

                Console.WriteLine("Press M to Migrate Data from Source Table to Destination Table");
                Console.WriteLine("Press S to Show Status of Previous Migrated Data");
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("Press TAB To see status and press X to cancel Migration");
                Console.WriteLine("--------------------------------------------------------------");



                keychoosen = Console.ReadKey();
                Console.Write("\n");
                switch (keychoosen.KeyChar)
                {
                    case 'm':
                    case 'M':

                        CancellationTokenSource s_cts = new CancellationTokenSource();
                        var StartingRow = 0;
                        var EndingRow = 0;
                        bool firstTime = true;
                        while(!( (EndingRow > StartingRow) && (EndingRow > 0) && (StartingRow > 0) ))
                        {
                            if(firstTime)
                            {
                                firstTime = false;
                            }
                            else
                            {
                                Console.WriteLine("Please Enter Starting Row and Ending Row in Correct Manner");
                            }
                            TakeInput(out StartingRow, out EndingRow);
                        }

                        MigrationStatus s = new MigrationStatus();
                        Task T = MigrateData.BatchMigration(StartingRow,EndingRow,s_cts,s);
                        Task cancelTask = Task.Run(() =>
                        {
                            ConsoleKeyInfo cki;
                            while (true)
                            {
                                if(!T.IsCompleted)
                                {
                                    Console.Write("Press 'X' to quit, or ");
                                    Console.WriteLine("TAB to Show Status of the Current task:");

                                    cki = Console.ReadKey(true);

                                    if (cki.Key == ConsoleKey.Tab)
                                    {
                                        var table = new ConsoleTable("From", "To", "Status");
                                        table.AddRow($"{s.From}", $"{s.To}", $"{s.Status}");
                                        Console.WriteLine(table);
                                    }
                                    if (cki.Key == ConsoleKey.X)
                                    {
                                        s_cts.Cancel();
                                        break;
                                    }
                                }
                                
                            }
                        });
                        try
                        {
                            await Task.WhenAny(T, cancelTask);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;

                    case 's':
                    case 'S':
                        var data = new List<MigrationStatus>();
                        using(var context = new CommonContext())
                        {
                            data = context.MigrationStatuses.ToList();
                        }
                        var table = new ConsoleTable("Id", "From", "To", "Status", "ExecutionTime");
                        foreach (var status in data)
                        {
                            table.AddRow($"{status.Id}",$"{status.From}",$"{status.To}",$"{status.Status}",$"{status.ExecutionTime}");
                        }
                        Console.WriteLine(table);
                        break;




                    default:
                        Console.WriteLine("Wrong Choice please select again \n");
                        break;
                }

            }
            while (keychoosen.Key != ConsoleKey.Escape);
        }

        

        public static void TakeInput(out int StartingRow, out int EndingRow)
        {
            Console.WriteLine("Enter Start Row :-");
            var SR = Console.ReadLine();
            while (!(int.TryParse(SR, out StartingRow)))
            {
                Console.WriteLine("Please Enter Starting Row Number Again! ");
                SR = Console.ReadLine();
            }
            Console.WriteLine("Enter End Row :-");
            var ER = Console.ReadLine();
            while (!(int.TryParse(ER, out EndingRow)))
            {
                Console.WriteLine("Please Enter Ending Row Number Again! ");
                SR = Console.ReadLine();
            }
        }
    }
}
