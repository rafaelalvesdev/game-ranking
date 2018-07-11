using System.Collections.Generic;

namespace Game.Ranking.Services.Results
{
    public class ServiceResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }
    }
}
