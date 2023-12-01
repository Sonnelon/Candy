using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandyCat.Classes
{
   public class Sweets
    {

        private int id;

        private string name;

        private float price;

        private string recipe;

        private string type;

        private string photo;


        public int Id { get { return id; } set {  id = value; } }

        public string Name { get { return name; } set { name = value; } }

        public float Price { get { return price; } set { price = value; } }

        public string Recipe { get {  return recipe; } set {  recipe = value; } }

        public string Type { get { return type; } set { type = value; } }

        public string Photo { 
            get { return photo; } 
            set { if (value == "") { photo = @"C:\Users\tzeen\source\repos\Candy\plus.png"; } 
                  else { photo = value; } } 
        }

        public Sweets() { } 

        public Sweets(int id, string name, float price,string recipe, string type, string photo)
        {
            Id = id;
            Name = name;    
            Price = price;
            Recipe = recipe;
            Type = type;
            Photo = photo;
        }

    }
}
