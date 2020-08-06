using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectJavelin.Models
{
    public partial class Stats
    {
        public int StatsId { get; set; }
        public string Gamertag { get; set; }
        [Display(Name = "Game_Type")]
        public string GameVariant { get; set; }
        public int? MapId { get; set; }
        public bool? Won { get; set; }
        public int? Score { get; set; }
        public int? Kills { get; set; }
        public int? Deaths { get; set; }
        public int? Assists { get; set; }
        public int? Headshots { get; set; }
        public int? Medals { get; set; }
        public double? KillDeathRatio { get; set; }
        public double? HeadShotRate { get; set; }
        [Display(Name = "Date_Played")]
        [DataType(DataType.Date)]
        public DateTime? PlayedAt { get; set; }
    }
}
