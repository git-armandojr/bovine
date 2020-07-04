using Bovine.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models
{
    [Table("cattle")]
    public class Cattle
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //public const string Type = "Animal";
        public string Identifier { get; set; }
        public Specie Specie { get; set; }
        public DateTime BirthDate { get; set; }
        public Sex Sex { get; set; }
        //public Nullable<float> Weight { get; set; }
        public float? Weight { get; set; }
        [ForeignKey(typeof(Pasture))]
        public int PastureID { get; set; }
        [ManyToOne]
        public Pasture Pasture { get; set; }

        //public Cattle(int iD, string identifier, Specie specie, DateTime birthDate, Sex sex, double weight)
        //{
        //    ID = iD;
        //    Identifier = identifier;
        //    Specie = specie;
        //    BirthDate = birthDate;
        //    Sex = sex;
        //    Weight = weight;
        //}

        public override string ToString()
        {
            return Identifier;
        }
    }    
}
