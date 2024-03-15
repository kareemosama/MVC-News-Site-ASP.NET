
using System.ComponentModel;


namespace NewsApp.ViewModels
{
    public class NewsViewModel
    {
       
        public int Id { get; set; }

        
        public string Title { get; set; }

        
        public string Body { get; set; }

        [DisplayName("Date")]
        public DateTime NewsDate { get; set; } = DateTime.Now;

      
        public string Image { get; set; }


        [DisplayName("Category Name")]
        public String CategoryName { get; set; }



    }
}

