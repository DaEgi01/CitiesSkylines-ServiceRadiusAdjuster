using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ServiceRadiusAdjuster.FunctionalCore
{
    [PublicAPI]
    [StructLayout(LayoutKind.Auto)]
    public readonly struct Result<TError>
    {
        private readonly TError? _errorResult;

        private Result(TError? errorResult)
        {
            _errorResult = errorResult;
        }

        public static Result<TError> Ok()
        {
            return new Result<TError>(default);
        }

        public static Result<TError> Error(TError errorResult)
        {
            return new Result<TError>(errorResult);
        }

        public Result<TError, TOk> Select<TOk>(Func<TOk> selector)
        {
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            return _errorResult is null
                ? Result<TError, TOk>.Ok(selector())
                : Result<TError, TOk>.Error(_errorResult);
        }

        public Result<TError, TOk> SelectMany<TOk>(Func<Result<TError, TOk>> selector)
        {
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            return _errorResult is null
                ? selector()
                : Result<TError, TOk>.Error(_errorResult);
        }

        public TResult Match<TResult>(Func<TError, TResult> error, Func<TResult> ok)
        {
            return _errorResult is null
                ? ok()
                : error(_errorResult);
        }

        public void OnErrorAndSuccess(Action<TError> error, Action ok)
        {
            if (_errorResult is null)
            {
                ok();
            }
            else
            {
                error(_errorResult);
            }
        }

        public void OnError(Action<TError> error)
        {
            if (_errorResult is not null)
            {
                error(_errorResult);
            }
        }
    }
}
