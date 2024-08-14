namespace Infrastructure.RestClients.Models
{
    using System.Collections.Generic;

    public class BaseReponseModel <T>
    {
        public required List<T> Data { get; set; }
    }
}
