using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scores.Models
{
    // J'ai mis des propriétés avec l'attribut [Ignore] afin de ne pas les stocker dans la base de données (pour les calculer dynamiquement dans le code)
    public class Equipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        [Ignore]
        public int Points { get; set; }
        [Ignore]
        public int NombreMatchs { get; set; }
        [Ignore]
        public int ScoresMarques { get; set; }
        [Ignore]
        public int ScoresAdverses { get; set; }
        [Ignore]
        public int VifsDorAttrapes { get; set; }
    }
}
