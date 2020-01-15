using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace DAL.Model
{
    public partial class Player
    {
        [J("name")]  public string Name { get; set; }
        [J("captain")]          public bool? Captain { get; set; }
        [J("shirt_number")]     public long ShirtNumber { get; set; }
        [J("position")]         public Position Position { get; set; }

        public int Goals { get; set; }
        public int YellowCards { get; set; }
        public bool Favourite { get; set; } = false;

        public override string ToString() => $"{Name} ({ShirtNumber}), position:{Position}";
        public override bool Equals(object obj) => obj is Player ? (obj as Player).Name.Equals(this.Name) : false;
        public override int GetHashCode() => this.Name.GetHashCode();
    }
}
