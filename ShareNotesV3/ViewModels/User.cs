using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareNotesV3.ViewModels
{
    public class User
    {
        public int Id { get; set; }
        public string Faculty { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
