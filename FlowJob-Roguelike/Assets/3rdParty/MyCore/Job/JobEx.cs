using System.Threading.Tasks;

namespace Core.Tools
{
    public static class JobEx
    {
        public static async Task Run<TJob>(this Job job) where TJob : Job, new()
        {
            await new TJob().Run(job.Lifetime);
        }
        
        public static async Task<TResult> Run<TJob, TResult>(this Job job) where TJob : Job<TResult>, new()
        {
            return await new TJob().Run(job.Lifetime);
        }
        
        public static async Task Run<TJob, TResult>(this Job<TResult> job) where TJob : Job, new()
        {
            await new TJob().Run(job.Lifetime);
        }
        
        public static async Task<TResult1> Run<TJob, TResult1, TResult2>(this Job<TResult2> job) where TJob : Job<TResult1>, new()
        {
            return await new TJob().Run(job.Lifetime);
        }
    }
}