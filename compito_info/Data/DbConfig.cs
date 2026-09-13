using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   
        public static class DbConfig
        {
            // LocalDB: viene installato insieme a Visual Studio, non serve un server vero

            public static string ConnectionString =>
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TorneoCalcioDB;Integrated Security=True;TrustServerCertificate=True";
        }


