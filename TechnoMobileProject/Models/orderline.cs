using System.ComponentModel.DataAnnotations;

namespace TechnoMobileProject.Models
{
    public class orderline
    {
        [Key]
        public int Id { get; set; }
        public string itemname  { get; set; }
        public int itemquant{ get; set; }
        public decimal itemprice { get; set; }
        public int orderid  { get; set; }
    }
}
