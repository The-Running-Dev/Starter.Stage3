using Starter.Framework.Clients;

namespace Starter.Data.Commands
{
    public class CatDeleteCommand : CatCommand
    {
        public CatDeleteCommand(IApiClient apiClient) : base(apiClient)
        {
        }

        //public override void Execute(object parameter)
        //{
        //    ApiClient.DeleteAsync(parameter);
        //}
    }
}