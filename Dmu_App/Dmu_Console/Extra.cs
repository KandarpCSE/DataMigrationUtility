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
    public class Extra
    {
        public static async Task BatchMigration(int start, int End, CancellationTokenSource s_cts, MigrationStatus status)
        {
            Console.WriteLine("Application started.");
            Console.WriteLine($"Migrating From {start} To {End} Rows to Destination Table ........");
            var stopwatch = Stopwatch.StartNew();
            int tempstart, tempend;
            List<Task> tasks = new List<Task>();
            if (start > End)
            {
                Console.WriteLine("Please Enter Range in Correct Manner");
                return;
            }
            int totalDataRows = End - start;
            status.From = start;
            status.To = End;
            status.TimeStamp = DateTime.Now;
            tempstart = start;
            while (totalDataRows >= UniversalConstant.limitofTransfer)
            {
                tempend = tempstart + UniversalConstant.limitofTransfer;
                tasks.Add(ProcessUrlAsync(tempstart, tempend, s_cts.Token));
                tempstart = tempend;
                totalDataRows -= UniversalConstant.limitofTransfer;
            }
            if (totalDataRows > 0 && totalDataRows < UniversalConstant.limitofTransfer)
            {
                tempend = tempstart + totalDataRows;
                tasks.Add(ProcessUrlAsync(tempstart, tempend, s_cts.Token));
            }
            Task T = Task.WhenAll(tasks);
            status.Status = EnumValues.Ongoing;
            await T;
            if (T.IsCompleted)
            {
                if (s_cts.IsCancellationRequested)
                {
                    Console.WriteLine($"Migration From {start} To {End} is Cancelled");
                    status.Status = EnumValues.Canceled;
                }
                else
                {

                    Console.WriteLine($"Migration From {start} To {End} is Completed");
                    status.Status = EnumValues.Completed;
                }
            }
            stopwatch.Stop();
            status.ExecutionTime = stopwatch.Elapsed;
            Console.WriteLine($"Saving Data ... {start} to {End}");
            using (var StatusContext = new CommonContext())
            {
                StatusContext.MigrationStatuses.Add(status);
                StatusContext.SaveChanges();
            }
            Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
            Console.WriteLine("Application ending.");
        }




        public static async Task ProcessUrlAsync(int start, int end, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                //Console.WriteLine($"Migrating From {start} To {end} Rows to Destination Table");
                List<SourceModel> sourceDataBatch = new List<SourceModel>();
                if (!token.IsCancellationRequested)
                {
                    using (var context = new CommonContext())
                    {
                        sourceDataBatch = context.Source.Where(b => (b.Id >= start && b.Id < end)).ToList();
                    }
                }
                List<DestinationModel> newdata = new List<DestinationModel>();
                foreach (var temp in sourceDataBatch)
                {
                    if (token.IsCancellationRequested)
                    {
                        //Console.WriteLine($"Number of Rows Affected {newdata.Count} Before Cancellation");
                        //Console.WriteLine($"Data from {temp.Id} to {end} is Cancelled");
                        return;
                    }
                    int tempSum = await Sum(temp.FirstNumber, temp.LastNumber);
                    newdata.Add(new DestinationModel() { SourceModelId = temp.Id, Sum = tempSum });

                }
                using (var context = new CommonContext())
                {
                    //Console.WriteLine($"Saving ... From {start} to {end} in database");
                    await context.Destination.AddRangeAsync(newdata, token);
                    context.SaveChanges();
                }
                //Console.WriteLine($"Migration of {start} - {end} Completed");
                //Console.WriteLine($"Number of Rows Affected {newdata.Count} in  Batch");
            }

        }

        public static async Task<int> Sum(int FN, int LN)
        {
            await Task.Delay(50);
            return FN + LN;
        }
    }
}
