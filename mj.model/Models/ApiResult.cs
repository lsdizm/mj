namespace mj.model {
    public class ApiResult
    {
        public Response? response { get; set; } 
    }

    public class Response 
    {
        public Header? header { get; set; } 
        public Body? body { get; set; } 
    }

    public class Header
    {
        public string? resultCode { get; set; }
        public string? resultMsg { get; set; }
    }

    public class Body
    {
        public Items? items { get; set; }
        public int? numOfRows { get; set; }
        public int? pageNo { get; set; }
        public int? totalCount { get; set; }
    }

    public class Items
    {
        public IEnumerable<RaceResult>? item { get; set; }
    }

}

