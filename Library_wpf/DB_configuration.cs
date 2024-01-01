using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Npgsql;

namespace Library_wpf 
{
    public class DBConfiguration 
    {
        public string Server = "localhost";
        public string UserId = "postgres";
        public string Password = "123";
        public string Database = "library";
    }
}