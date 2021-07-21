using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Examples
{
    public class AsyncMethodBuilderTests
    {
        [Fact]
        public async void CallAsync()
        {
            await VoidAsync();
            foreach (var call in System.Runtime.CompilerServices.AsyncSpyMethodBuilder.Spy)
                Console.WriteLine(call);

            async Task VoidAsync() { }
        }
    }
}

namespace System.Runtime.CompilerServices
{
    public class AsyncSpyMethodBuilder
    {
        private static IList<string> _spy = new List<string>();
        public static IList<string> Spy => _spy;

        public AsyncSpyMethodBuilder()
            => _spy.Add(".ctor");

        public static AsyncSpyMethodBuilder Create()
        {
            _spy.Add("Create()");
            return new AsyncSpyMethodBuilder();
        }

        public void SetResult() => _spy.Add("SetResult()");

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            _spy.Add($"Start({stateMachine})");
            stateMachine.MoveNext();
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
            => _spy.Add($"AwaitOnCompleted({awaiter}, {stateMachine})");

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
            => _spy.Add($"AwaitUnsafeOnCompleted({awaiter}, {stateMachine})");

        public void SetException(Exception exception)
            => _spy.Add($"SetException({exception})");

        public void SetStateMachine(IAsyncStateMachine stateMachine)
            => _spy.Add($"SetStateMachine({stateMachine})");
    }
}
