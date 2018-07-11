using System.Collections.Generic;

namespace Game.Ranking.Services.Results
{
    public class ServiceResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
    }

    public class ServiceResult<T> : ServiceResult
    {
        T Data { get; set; }
    }
}
