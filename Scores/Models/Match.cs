using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scores.Models
{
    public class Match
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int EquipeDomicileId { get; set; }
        public int EquipeExterieurId { get; set; }
        public DateTime DateMatch { get; set; }
        public int ScoreDomicile { get; set; }
        public int ScoreExterieur { get; set; }
        public int VifDorAttrapeParEquipeId { get; set; }

        [Ignore]
        public string NomEquipeAdverse { get; set; }

        [Ignore]
        public string NomEquipeSuivie { get; set; }
    }
}
