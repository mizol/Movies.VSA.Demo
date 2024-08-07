﻿namespace Common.Core
{
    public class Result
    {
        protected const string WrongResultArguments = "Result is invalid. Check result parameters.";

        public bool IsSuccess { get; }
        public List<Error> Errors { get; } = new List<Error>();

        public Result() { }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None
                || !isSuccess && error == Error.None)
            {
                throw new ArgumentException(WrongResultArguments, nameof(error));
            }

            IsSuccess = isSuccess;
            if (error != Error.None)
            {
                Errors.Add(error);
            }
        }

        public static Result Success() => new Result(true, Error.None);

        public static Result<T> Success<T>(T value) => new Result<T>(value, Error.None);

        public static Result Failure(Error error) => new Result(false, error);

        public static Result<T> Failure<T>(Error error) => new Result<T>(default, error);

        public void AddError(Error error) => Errors.Add(error);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        public Result(T value, Error error)
            : base(false, error)
        {
            Value = value;
        }

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

        public static Result<T> Success(T value) => new Result<T>(isSuccess: true, value, Error.None);
        public static Result<T> Failure(T value, Error error) => new Result<T>(isSuccess: false, default(T)!, error);
        
    }
}