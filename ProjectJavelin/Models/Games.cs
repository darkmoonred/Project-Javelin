using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectJavelin.Models
{
    public class Games
    {
        public string gameVariant { get; set; }
        public int mapId { get; set; }
        public bool won { get; set; }
        public int score { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public int headshots { get; set; }
        public int medals { get; set; }
        public double killDeathRatio { get; set; }
        public double headshotRate { get; set; }
        [Display(Name = "Game_Date_Full")]
        public DateTime playedAt { get; set; }

    }
}
