using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursoMVCApi.Models.WS
{
    public class AnimalViewModel : SecurityViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Patas { get; set; }
    }
}