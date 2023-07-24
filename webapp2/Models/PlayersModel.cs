using System.ComponentModel.DataAnnotations;

namespace webapp2.Models
{
   public class Player
    {
        public int Id { get; set; }
        [Display(Name ="Player Name")]
        [StringLength(35)]
        public string PlayerName { get; set; } = null!;
        public string Position { get; set; } = null!;
        [Display(Name ="Date Joined")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datejoined { get; set; }


        public int? ClubId { get; set; }
        public virtual Club Club { get; set; } =new();

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; } = new();



    }
   
   
    public class Country
    {
        public int Id { get; set; } 
        [Display(Name ="Country Name")]
        [StringLength(20)]

        public string CountryName { get; set; } = "";

    }
    public class Club
    {
        [Key] public int Id { get; set; } 
        [Display(Name ="Club Name")]
        [StringLength (20)]
        public string ClubName { get; set; }= "";


    }
}
