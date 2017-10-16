﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Standartstyle.Models
{
    public class GoodModel
    {
        public int GoodCode { get; set; }


        [Required(ErrorMessage = "Обязательно к заполнению")]
        [Display(Name = "Название")]
        public string Name { get; set;}
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [Display(Name = "Размеры (шир х выс х глб)")]
        public string Size { get; set; }

        public int MainImageIndex { get; set; }
        public IEnumerable<ImageModel> Images { get; set; }

        public int SelectedCategoryCode { get; set; }
        public IEnumerable<GoodsCategoryModel> Categories { get; set; }

        public IEnumerable<int> SelectedAttributeCodes { get; set; }
        public IEnumerable<AttributeModel> Attributes { get; set; }

        public IEnumerable<CBDManufacturerModel> Manufacturers { get; set; }
        public IEnumerable<CBDCollectionModel> Collections { get; set; }
        public IEnumerable<int> SelectedColorCodes { get; set; }
        public IEnumerable<CBDColorModel> CBDColors { get; set; }


        public GoodModel()
        {
            this.Images = new List<ImageModel>();
            this.Categories = new List<GoodsCategoryModel>();
            this.Attributes = new List<AttributeModel>();
            this.Manufacturers = new List<CBDManufacturerModel>();
            this.Collections = new List<CBDCollectionModel>();
            this.CBDColors = new List<CBDColorModel>();

            this.SelectedAttributeCodes = new List<int>();
            this.SelectedColorCodes = new List<int>();
        }
    }
}