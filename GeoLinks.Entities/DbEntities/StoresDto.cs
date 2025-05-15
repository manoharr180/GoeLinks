using System.Collections.Generic;

public class StoresDto
{
    public List<StoreDto> storesDto { get; set; }
}
public class StoreDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreLogo { get; set; }
        public List<StoreItemDetails> storeItems { get; set; }
        
    }

public class StoreItemDetailsDto{
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