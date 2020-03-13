using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testing.WebUI.Models
{
    public class PageInfo
    {
        //колчество товаров
        public int TotalItems { get; set; }
        //Количество товаров на одной странице
        public int ItemsPerPage { get; set; }
        //Номер текущей страницы
        public int CurrentPage { get; set; }
        //Общие количестов страниц
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); } }
    }
}