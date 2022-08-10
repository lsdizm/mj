namespace mj.model {
    public class ApiResult
    {
        public Response response { get; set; } = new Response();
    }

    public class Response 
    {
        public Header header { get; set; } = new Header();
        public Body body { get; set; } = new Body();
    }

    public class Header
    {
        public string? resultCode { get; set; }
        public string? resultMsg { get; set; }
    }

    public class Body
    {
        public dynamic? items { get; set; }
        public int? numOfRows { get; set; }
        public int? pageNo { get; set; }
        public int? totalCount { get; set; }
    }
}