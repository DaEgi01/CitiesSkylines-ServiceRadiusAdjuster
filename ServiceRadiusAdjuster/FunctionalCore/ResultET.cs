using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ServiceRadiusAdjuster.FunctionalCore
{
    [PublicAPI]
    [StructLayout(LayoutKind.Auto)]
    public readonly struct Result<TError, TOk>
    {
        private readonly TError? _errorResult;
        private readonly TOk? _okResult;

        private Result(TError? errorResult, TOk? okResult)
        {
            if (errorResult is not null && okResult is null)
                throw new ArgumentNullException(nameof(errorResult));

            _errorResult = errorResult;
            _okResult = okResult;
        }

        public static Result<TError, TOk> Ok(TOk okResult)
        {
            return new Result<TError, TOk>(default, okResult);
        }

        public static Result<TError, TOk> Error(TError errorResult)
        {
            return new Result<TError, TOk>(errorResult, default);
        }

        public Result<TError, TOkResult> Select<TOkResult>(Func<TOk, TOkResult> selector)
        {
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            return _errorResult is null
                ? Result<TError, TOkResult>.Ok(selector(_okResult))
                : Result<TError, TOkResult>.Error(_errorResult);
        }

        public Result<TError, TOkResult> SelectMany<TOkResult>(Func<TOk, Result<TError, TOkResult>> selector)
        {
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            return _errorResult is null
                ? selector(_okResult)
                : Result<TError, TOkResult>.Error(_errorResult);
        }

        public TResult Match<TResult>(Func<TError, TResult> error, Func<TOk, TResult> ok)
        {
            return _errorResult is null
                ? ok(_okResult)
                : error(_errorResult);
        }

        public void OnErrorAndSuccess(Action<TError> error, Action<TOk> ok)
        {
            if (_errorResult is null)
            {
                ok(_okResult);
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
