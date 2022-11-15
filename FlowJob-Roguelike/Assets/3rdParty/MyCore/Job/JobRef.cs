using System;
using System.Threading.Tasks;

namespace Core.Tools
{
    // public class JobRef<TResult>
    // {
    //     private Type jobType;
    //
    //     private JobRef(Type jobType)
    //     {
    //         this.jobType = jobType;
    //     }
    //     
    //     public static JobRef<TResult> Of<TJob>() where TJob : Job<TResult>, new()
    //     {
    //         return new JobRef<TResult>(typeof(Job<>).MakeGenericType(typeof(TResult)));
    //     }
    //     
    //     public async Task<TResult> Run()
    //     {
    //         // TODO add pooling
    //         return await ((Job<TResult>) Activator.CreateInstance(jobType)).Run();
    //     }
    // }
}