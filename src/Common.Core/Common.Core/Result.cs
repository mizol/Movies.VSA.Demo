﻿namespace Common.Core
{
    public class Result
    {
        protected const string WrongResultArguments = "Result is invalid. Check result parameters.";

        public bool IsSuccess { get; }
        public Error ErrorType { get; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None
                || !isSuccess && error == Error.None)
            {
                throw new ArgumentException(WrongResultArguments, nameof(error));
            }

            IsSuccess = isSuccess;
            ErrorType = error;
        }

        public static Result Success() => new Result(true, Error.None);

        public static Result Failure(Error error) => new Result(false, error);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected Result(bool isSuccess, T value, Error error)
            :base(isSuccess, error) 
        {
            if (isSuccess && error != Error.None
                || !isSuccess && error == Error.None)
            {
                throw new ArgumentException(Result.WrongResultArguments, nameof(error));
            }

            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, Error.None);
        public static Result<T> Failure(T value, Error error) => new Result<T>(false, default(T)!, error);
    }
}