using System;
using System.Collections.Generic;

public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreLogo { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public DateTime ModifieDatetime { get; set; }
        public bool IsActive { get; set; }
        public List<StoreItemDetails> StoreItemDetails { get; set; }
        
    }

public class StoreItemDetails{
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Material { get; set; }
        public string Quntity { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
}