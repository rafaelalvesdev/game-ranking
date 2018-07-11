using Game.Ranking.Services.Results;
using System.Collections.Generic;

namespace Game.Ranking.Services.Extensions
{
    public static class ServiceResultExtensions
    {
        public static ServiceResult<T> AsServiceResult<T>(this T data)
        {
            return new ServiceResult<T>()
            {
                Data = data
            };
        }

        public static ServiceResult Valid(this ServiceResult serviceResult)
        {
            serviceResult.IsValid = true;
            return serviceResult;
        }

        public static ServiceResult Invalid(this ServiceResult serviceResult)
        {
            serviceResult.IsValid = false;
            return serviceResult;
        }

        public static ServiceResult WithErrors(this ServiceResult serviceResult, List<string> ErrorMessages)
        {
            serviceResult.Errors.AddRange(ErrorMessages);
            return serviceResult;
        }
    }
}
