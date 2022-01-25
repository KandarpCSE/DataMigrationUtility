using Dmu_Console.Data;
using Dmu_Console.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Dmu_Console
{
    public  class Operation
    {
        
        // Initialize 1 Lakh Data to the source table
        public static void InitializeData()
        {
            using(var _db = new CommonContext())
            {
                Console.WriteLine(_db.Source.Count());
                if (_db.Source.Count() == 100000)
                {

                    string file = System.IO.File.ReadAllText(@"C:\Users\Kandarp Patel\Desktop\Dmu_App\Dmu_Console\generated1.json");
                    var data = JsonSerializer.Deserialize<List<SourceModel>>(file);
                    _db.AddRange(data);
                    _db.SaveChanges();
                }
            }
        }
    }
}
