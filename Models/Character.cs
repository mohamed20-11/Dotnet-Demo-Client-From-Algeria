using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet_Project.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Ahbadj";
        public int HitPoints { get; set; } = 100; 
        public int Strength { get; set; } = 10 ;
        public int Defense { get; set; } =10;   
        public int Intellegence { get; set; } =10 ;
        public RpgClass Class { get; set; }=RpgClass.Knight;
    }
}