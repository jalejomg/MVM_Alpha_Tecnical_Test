namespace Alpha.Web.API.Domain.Models
{
    public class ResponseModel<Model> where Model : class
    {
        public int Count { get; set; }
        public Model Data { get; set; }
    }
}
